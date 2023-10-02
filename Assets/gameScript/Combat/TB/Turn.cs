using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turn : MonoBehaviour
{
    public int baseCounter;
    public int counter;
    public bool moved;

    public abstract void ManageTurn();
    
}
