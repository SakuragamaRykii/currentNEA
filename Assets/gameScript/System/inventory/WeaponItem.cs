using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponItem : Item
{
    public int durability;
    public GameObject weapon { get; private set; }
    public float damagemult { get; private set; }

    public float cd { get; private set; }
    public WeaponItem(string name, int durability) : base(name)
    {
        hasDurability = true;
        this.durability = durability;
        weapon = Resources.Load("prefabs/RT/Weapons/" + name).GameObject();
        switch (name)
        {
            case "Sword":
                description = "A normal sword.";
                damagemult = 1;
                cd = 0.3f;
                break;
            case "Hammer":
                description = "A little bit heavier than a sword but deals more damage.";
                damagemult = 1.5f;
                cd = 0.5f;
                break;
            case "Drill":
                description = "Only shoots in a straight line but does big damage. Very heavy.";
                damagemult = 3f;
                cd = 1f;

                break;
            case "Fist":
                description = "Your own trusty fists.";
                damagemult = 0.5f;
                cd = 0.2f;

                break;
        }
       // Debug.Log(weapon.name);
    }


}
