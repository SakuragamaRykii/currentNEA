using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public int baseCounter;
    public int counter { get; set; }

    private void Awake()
    {

    }
    private void Update()
    {
        if (MoveManager.Peek() == this & !moving) ManageTurn();
    }



    protected bool moving;
    public void ManageTurn()
    {
        moving = true;
        //body

        MoveManager.Deq();
        MoveManager.Enq(this, MoveManager.bench);
    }
}
