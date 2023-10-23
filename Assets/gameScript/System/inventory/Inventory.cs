using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public static class Inventory
{
    public static DynList<Item>[] itemsIn = new DynList<Item>[15];
    public static int index;
    public static WeaponItem currentlyEquipped;




    public static Item peek(int index) { return itemsIn[index].first.data; }
    
    public static int HashFunc(string toHash)
    {
        int mid = toHash.Length / 2;

        Debug.Log((int)toHash[mid] % 15);

        return (int)toHash[mid] % 15;//casting to int converts to the hash value
    }

    public static void insert(Item i)
    {
        int slot = HashFunc(i.name);
        if (itemsIn[slot] == null) return;

            if (itemsIn[slot].size > 0)
        {
            
            if (i.hasDurability)  itemsIn[slot].add(i);
            else itemsIn[slot].DataAt(0).amount++;

        }
        else itemsIn[slot].add(i);

    }

    public static void ResetInventory()
    {
        for (int i = 0; i < itemsIn.Length; i++)
        {
            itemsIn[i] = new DynList<Item>();
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

    public static void Use(string slot)
    {
        int hIndex = HashFunc(slot);
        Debug.Log("used");
        peek(hIndex).Use();
  

    }
    public static void Use(int slot)
    {
       
    }
    public static void Fist()
    {
        if (currentlyEquipped == null) currentlyEquipped = new WeaponItem("Fist", 100000);
    }

}
