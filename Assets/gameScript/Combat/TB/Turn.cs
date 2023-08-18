using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public int baseCounter;
    public int counter { get; set; }

    private void Awake()
    {
        insert();

    }
    private void Update()
    {
        if (MoveManager.Peek() == this & !moving) ManageTurn();
    }
    public void insert()
    {
        MoveManager.Enq(this);
    }


    protected bool moving;
    public void ManageTurn()
    {
        moving = true;
        //body

        MoveManager.Deq();
    }
}
