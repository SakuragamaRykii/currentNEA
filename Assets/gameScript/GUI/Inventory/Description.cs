using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Description : MonoBehaviour
{
    TextMeshProUGUI desc;
    public InvGUI inv;
    void Start()
    {
        desc = GetComponentInChildren<TextMeshProUGUI>();
        desc.text = "Nothing in this slot.";
    }

    // Update is called once per frame
    void Update()
    {
        if(inv.selected == null) desc.text = "Nothing in this slot.";
        else
        {
            if(!inv.selected.isConsumable && Inventory.currentlyEquipped == inv.selected) desc.text = inv.selected.description + " (Currently Equipped)";
            else desc.text = inv.selected.description;
        }
    }
}
