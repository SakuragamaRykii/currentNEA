using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponItem : Item
{
    public int durability { get; private set; }
    public GameObject weapon { get; private set; }
    public float damagemult { get; private set; }
    public WeaponItem(string name, int durability) : base(name)
    {
        hasDurability = true;
        this.durability = durability;
        weapon = (GameObject)Resources.Load("prefabs/RT/" + name);
        switch (name)
        {
            case "Sword":
                damagemult = 0.5f;
                break;
            case "Hammer":
                damagemult = 1;
                break;
            case "Drill":
                damagemult = 2.2f;
                break;
        }
    }

}
