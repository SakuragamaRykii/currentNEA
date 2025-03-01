using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponItem : Item
{
    public int durability { get; private set; }
    public GameObject weapon { get; private set; }
    public float damagemult { get; private set; }
    public float cd { get; private set; }
    public WeaponItem(string name, int durability) : base(name)
    {
        hasDurability = true;
        this.durability = durability;
        weapon = (GameObject)Resources.Load("prefabs/RT/Weapons/" + name);
        switch (name)
        {
            case "Sword":
                description = "Oooh shiny sword" + "\n Durability: " + durability;
                damagemult = 1;
                cd = 0.3f;
                break;
            case "Hammer":
                description = "A little heavier than a sword but deals more damage" + "\n Durability: " + durability;
                damagemult = 1.5f;
                cd = 0.5f;
                break;
            case "Drill":
                description = "Only shoots in 4 directions but deals a lot of damage. Very heavy." + "\n Durability: " + durability;
                damagemult = 3f;
                cd = 1.2f;
                break;
            case "Fist":
                description = "The power of the punch must not be underestimated.";
                damagemult = 0.5f;
                cd = 0.2f;
                break;
        }
    }

}
