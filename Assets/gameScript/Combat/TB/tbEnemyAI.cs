using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class tbEnemyAI : Turn
{
    EnemyStat es;
    public int number;
   UnityEngine.UI.Slider hpBar;
    private void Start()
    {
        es = GetComponent<EnemyStat>();
        hpBar = GetComponentInChildren<UnityEngine.UI.Slider>();
        moved = false;
        baseCounter = es.speed; //Random.Range(11, 20);
        counter = baseCounter;
        MoveManager.Enq(this);
        number = MoveManager.enemiesOnATM;
    }
    private void Update()
    {
//        Debug.Log(es.currentHP);
        if (es.IsDead())
        {
            MoveManager.root.Q<UnityEngine.UIElements.Button>("Target" + number).SetEnabled(false);
            es.Die();
        }
//        Debug.Log((float)(es.currentHP / es.maxHP));
        hpBar.value = (float)(es.currentHP / es.maxHP);
    }
    public override void ManageTurn()
    {
        if (es.IsDead())
        {
            MoveManager.turns.remove(this);
            MoveManager.bench.remove(this);

            MoveManager.enemiesOnATM--;
            return;
        }
        moved = true;
        
        Debug.Log("enemy has moved");
        float damage;
        if (tbCombat.defending)
            damage = es.attack - PlayerStat.defence;
        else
            damage = es.attack - PlayerStat.defence / 4;
        if (damage < 0) damage = 0;
        PlayerStat.TakeDamage(damage);

        // Debug.Log("enemy dealt " + es.attack + "damage");
        MoveManager.root.Q<UnityEngine.UIElements.TextElement>("log-text").text = ("Enemy dealt " + (int)es.attack + "damage");
        MoveManager.Deq();
    }
}
