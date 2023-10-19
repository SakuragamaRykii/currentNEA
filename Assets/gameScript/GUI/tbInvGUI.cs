using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class tbInvGUI : MonoBehaviour
{
    public GameObject[] slots; //list of slots in the inventory
    public GameObject[] amounts; //the list of ui objects that store the amount of each item the player has
    [SerializeField] private tbCombat tbObj; 
    private void Start()
    {
        if (Inventory.itemsIn[0] == null) Inventory.ResetInventory(); //if the first list object is null, it probably means that the other objects are also null
        slots = GameObject.FindGameObjectsWithTag("Slot");
        amounts = GameObject.FindGameObjectsWithTag("amount");
        for (int i = 0; i < slots.Length; i++)
        {
            Button select = slots[i].GetComponentInChildren<Button>();
            select.onClick.AddListener(() => refer(select, i));
        }
        gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        DisplayGUI();
    }

    private void DisplayGUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            GameObject amount = amounts[i], slot = slots[i];
            Item inSlot;
            Debug.Log(Inventory.itemsIn[i]);

            try { inSlot = Inventory.peek(i); }
            catch (NullReferenceException e)
            {
                slot.GetComponentInChildren<TextMeshProUGUI>().text = "Empty Slot";
                amount.GetComponent<TextMeshProUGUI>().text = "0";
                continue;
            }



            if (inSlot.hasDurability)
            {
                if (((WeaponItem)inSlot).durability <= 0) Inventory.Deq(i);
                else amount.GetComponent<TextMeshProUGUI>().text = Inventory.itemsIn[i].size.ToString();
            }
            else
            {
                if (inSlot.amount <= 0) Inventory.Deq(i);
                else amount.GetComponent<TextMeshProUGUI>().text = inSlot.amount.ToString();
            }

            slot.GetComponentInChildren<TextMeshProUGUI>().text = inSlot.name;

        }
    }


    public Item selected;


    public void refer(Button b, int index)
    {
        string selectedIName = b.GetComponentInChildren<TextMeshProUGUI>().text;
        if (selectedIName.Contains("Empty Slot")) return;
        int hIndex = Inventory.HashFunc(selectedIName);
        Item current = Inventory.peek(hIndex);

        if (selected == null) selected = current;
        else if (selected.name.Equals(selectedIName))
        {
            if (current.hasDurability)
            {
                Inventory.currentlyEquipped = (WeaponItem)current;
                Debug.Log(Inventory.currentlyEquipped.name);
            }

            else
            {
                Inventory.Use(current.name);
                MoveManager.Deq();
                gameObject.SetActive(false);
            }
        }
    }

}
