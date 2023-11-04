using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public string name;
    public bool hasDurability;
    public bool isConsumable;
    public int amount;
    public string description;

    public Item(string name)
    {
        this.name = name;
        amount = 1;

    }
    public void Use()
    {
        switch (name)
        {
            case "Banana":
                PlayerStat.currentHP += (PlayerStat.maxHP * 0.4f);
                Debug.Log(PlayerStat.currentHP);
                amount--;
                break;
            case "Spig Beed":
                PlayerStat.speed++;
                amount--;
                break;
        }
    }


}
