using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turn : MonoBehaviour
{
    public float baseCounter;
    public float counter;
    public bool moved;

    public abstract void ManageTurn();
    
}
