using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static DynList<Item>[] itemsIn;
    public static GameObject[] slots;
    public GameObject invObj;
    public static GameObject[] amounts;
    

    private void Start()
    {
        invObj = gameObject;
        itemsIn = new DynList<Item>[15];
        for(int i = 0; i < itemsIn.Length; i++)
        {
            itemsIn[i] = new DynList<Item>();
        }
        slots = GameObject.FindGameObjectsWithTag("Slot");
        amounts = GameObject.FindGameObjectsWithTag("amount");
        foreach (GameObject g in slots)
        {
            
            Button select = g.GetComponentInChildren<Button>();
            select.onClick.AddListener( () => refer(select) );
            

        }
        
    }

    public int index;
    public Item selected;
    public void refer(Button b)
    {
        string name = b.GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log(name);
        WeaponItem weapon = new WeaponItem("WEAPON", 100);
        int hIndex = HashFunc(weapon.name);
        

        if (name.Contains("Empty Slot"))
        {
            Debug.Log("empty");
            insert(weapon);
            
        }
        index = hIndex;
        selected = itemsIn[index].DataAt(0);


    }


    //HashTable funcitons --------------------------------------------------
    public static int HashFunc(string toHash)
    {
        int mid = toHash.Length / 2;

        Debug.Log((int)toHash[mid] % 15);

        return (int)toHash[mid] % 15;//casting to int converts to the hash value
    }

    public static void insert(Item i)
    {
        int slot = HashFunc(i.name);
        GameObject selected = slots[slot];
        Debug.Log(amounts.Length);
        GameObject amount = amounts[slot];
        if (itemsIn[slot].size > 0)
        {
            
            if (!i.hasDurability)
            {
                itemsIn[slot].DataAt(0).amount++;
                amount.GetComponent<TextMeshProUGUI>().text = itemsIn[slot].DataAt(0).amount.ToString();
            }
            else 
            {
                itemsIn[slot].add(i);
                amount.GetComponent<TextMeshProUGUI>().text = itemsIn[slot].size.ToString();
            }
            selected.GetComponentInChildren<TextMeshProUGUI>().text = i.name;
            
            
        }
        else
        {
            itemsIn[slot].add(i);
            selected.GetComponentInChildren<TextMeshProUGUI>().text = i.name;
            Debug.Log(i.name);
            amount.GetComponent<TextMeshProUGUI>().text = "1";
        }


    }


    public static void Deq(int slot)
    {
        itemsIn[slot].remove(itemsIn[slot].DataAt(0));
    }
    public static void Deq(string slot)
    {
        int hIndex = HashFunc(slot);
        itemsIn[hIndex].remove(itemsIn[hIndex].DataAt(0));
    }



}
