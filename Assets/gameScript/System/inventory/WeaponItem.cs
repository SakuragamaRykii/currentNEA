using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponItem : Item
{
    public int durability { get; private set; }
    public WeaponItem(string name, int durability) : base(name)
    {
        hasDurability = true;
        this.durability = durability;
    }

}
