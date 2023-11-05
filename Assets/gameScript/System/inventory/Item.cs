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
        if (isConsumable)
        {
            switch (name)
            {
                case "Banana":
                    description = "Looks like a banana, though it feels like jelly when you eat it. *Restores hp when consumed*.";
                    break;
                case "Spig Beed":
                    description = "What other bead is spelt like this? *Permenantly increases speed by 1*.";
                    break;
            }
        }
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
