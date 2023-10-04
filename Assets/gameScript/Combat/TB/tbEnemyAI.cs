using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class tbEnemyAI : Turn
{
    //WARNING: IN THE CURRENT SYSTEM THE FIGHT ENDS WHEN ONLY ONE OF THE ENEMIES DIE, NOT ALL
    EnemyStat es;
    PlayerStat ps;
    public int number;
   UnityEngine.UI.Slider hpBar;
    private void Start()
    {
        es = GetComponent<EnemyStat>();
        hpBar = GetComponentInChildren<UnityEngine.UI.Slider>();
        moved = false;
        baseCounter = es.speed; //Random.Range(11, 20);
        counter = baseCounter;
        ps = GameObject.FindObjectOfType<PlayerStat>(); //note that there should only be one player obj
        MoveManager.Enq(this);
        number = MoveManager.enemiesOnATM;
    }
    private void Update()
    {
        if (es.IsDead())
        {
            MoveManager.root.Q<UnityEngine.UIElements.Button>("Target" + number).SetEnabled(false);
        }

        hpBar.value = es.currentHP / es.maxHP;
    }
    public override void ManageTurn()
    {
        if (es.IsDead())
        { 
            MoveManager.Deq();
            MoveManager.enemiesOnATM--;
            return;
        }
        moved = true;
        
        Debug.Log("enemy has moved");
        ps.TakeDamage(es.attack);
        Debug.Log("enemy dealt " + es.attack + "damage");
        MoveManager.Deq();
    }
}
