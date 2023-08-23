using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turn : MonoBehaviour
{
    public int baseCounter;
    public int counter;
    public bool moved;

    /*  private void Awake()
      {
          counter = baseCounter;
      }
      private void Update()
      {
          if (MoveManager.Peek() == this && !moved) ManageTurn();
      }
    */

    public abstract void ManageTurn();



    
}
