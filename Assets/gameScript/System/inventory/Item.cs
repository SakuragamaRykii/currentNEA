using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public string name;
    public bool hasDurability;
    public bool isConsumable;
    public string description;
    public int amount;

    public Item(string name)
    {
        this.name = name;
        amount = 1;
        switch (name)
        {
            case "Banana":
                description = "Yummy banana. Restores you HP.";
                break;
            case "Spig Beed":
                description = "Permenant increase to speed.";
                break;
        }

    }
    public virtual void Use()
    {
        switch (name)
        {
            case "Banana":
                PlayerStat.currentHP += PlayerStat.maxHP * 0.4f;
                break;
            case "Spig Beed":
                PlayerStat.speed++;
                break;
        }
        amount--;
    }


}
