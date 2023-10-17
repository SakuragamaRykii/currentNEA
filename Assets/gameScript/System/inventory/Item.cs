using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public string name;
    public bool hasDurability;
    public bool isConsumable;
    public int amount;

    public Item(string name)
    {
        this.name = name;
        amount = 1;

    }


}
