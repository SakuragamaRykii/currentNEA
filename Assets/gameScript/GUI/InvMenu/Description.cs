using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    private TextMeshProUGUI desc;
    public InvGUI inventory;
    void Start()
    {
        desc = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (inventory.selected != null)
        {
            if (inventory.selected.hasDurability)
            {
                if (Inventory.currentlyEquipped.name.Equals(inventory.selected.name)) desc.text = inventory.selected.description + "\n Current Durability: " +
                        ((WeaponItem)inventory.selected).durability + "\n (EQUIPPED)";

                else desc.text = inventory.selected.description + "\n" + ((WeaponItem)inventory.selected).durability;
            }
            else desc.text = inventory.selected.description;
        }
        else desc.text = "Nothing.";
        
    }
}
