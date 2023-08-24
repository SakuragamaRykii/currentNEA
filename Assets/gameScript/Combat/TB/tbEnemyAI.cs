using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tbEnemyAI : Turn
{
    //WARNING: IN THE CURRENT SYSTEM THE FIGHT ENDS WHEN ONLY ONE OF THE ENEMIES DIE, NOT ALL
    EnemyStat es;
    private void Start()
    {
        es = GetComponent<EnemyStat>();
        moved = false;
        baseCounter = es.speed; //Random.Range(11, 20);
        counter = baseCounter;

        MoveManager.Enq(this);
    }
    private void Update()
    {
        if (es.isDead)
        {
            MoveManager.finish = true;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    public override void ManageTurn()
    {
        moved = true;
        
        Debug.Log("enemy has moved");
        PlayerStat.currentHP -= GetComponent<EnemyStat>().attack;
        Debug.Log("enemy dealt " + GetComponent<EnemyStat>().attack + "damage");
        MoveManager.Deq();
        MoveManager.bench.add(this);
    }
}
