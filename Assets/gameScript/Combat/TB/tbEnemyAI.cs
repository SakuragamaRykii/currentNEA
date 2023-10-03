using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tbEnemyAI : Turn
{
    //WARNING: IN THE CURRENT SYSTEM THE FIGHT ENDS WHEN ONLY ONE OF THE ENEMIES DIE, NOT ALL
    EnemyStat es;
    PlayerStat ps;
    private void Start()
    {
        es = GetComponent<EnemyStat>();
        moved = false;
        baseCounter = es.speed; //Random.Range(11, 20);
        counter = baseCounter;
        ps = GameObject.FindObjectOfType<PlayerStat>(); //note that there should only be one player obj
        MoveManager.Enq(this);
    }
    private void Update()
    {

    }
    public override void ManageTurn()
    {
        moved = true;
        
        Debug.Log("enemy has moved");
        ps.TakeDamage(es.attack);
        Debug.Log("enemy dealt " + es.attack + "damage");
        MoveManager.Deq();
    }
}
