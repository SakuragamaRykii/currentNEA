using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tbEnemyAI : Turn
{
    private void Awake()
    {
        moved = false;
        baseCounter = 1; //Random.Range(11, 20);
        counter = baseCounter;

        MoveManager.Enq(this);
    }
    private void Update()
    {

        //if (MoveManager.Peek() == this && !moved) ManageTurn();

    }
    public override void ManageTurn()
    {
        moved = true;
        
        Debug.Log("enemy has moved");

        MoveManager.Deq();
        MoveManager.bench.add(this);
    }
}
