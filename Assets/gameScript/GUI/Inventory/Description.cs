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
        Item selected = inv.selected;
        if(selected == null) desc.text = "Nothing in this slot.";
        else
        {
            if(selected.isConsumable && Inventory.currentlyEquipped == selected) desc.text = selected.description + " (Currently Equipped)";

            else desc.text = selected.description;
        }
    }
}
