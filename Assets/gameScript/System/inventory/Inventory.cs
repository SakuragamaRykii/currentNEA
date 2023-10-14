using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static DynList<Item>[] itemsIn;
    public static GameObject[] slots;
    public GameObject invObj;
    

    private void Start()
    {
        invObj = gameObject;
        itemsIn = new DynList<Item>[15];
        for(int i = 0; i < itemsIn.Length; i++)
        {
            itemsIn[i] = new DynList<Item>();
        }
        slots = GameObject.FindGameObjectsWithTag("Slot");
        foreach(GameObject g in slots)
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
        int hIndex = HashFunc(i.name);
        Add(i, hIndex);
    }

    private static void Add(Item i, int slot)
    {
        if (itemsIn[slot].size <= 0)
        {
            if (!i.hasDurability) itemsIn[slot].DataAt(0).amount++;
            else itemsIn[slot].add(i);
        }
        else
        {
            itemsIn[slot].add(i);
            slots[slot].GetComponentInChildren<TextMeshProUGUI>().text = i.name;
            Debug.Log(i.name);
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
