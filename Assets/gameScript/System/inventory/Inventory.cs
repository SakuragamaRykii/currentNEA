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
    public static Item peek(string h) { return itemsIn[HashFunc(h)].first.data; }


    public static int HashFunc(string toHash)
    {
        int mid = toHash.Length / 2;

        Debug.Log(toHash[mid] % 15);

        return toHash[mid] % 15;//casting to int converts to the hash value
    }

    public static void insert(Item i)
    {
        int slot = HashFunc(i.name);
        if (itemsIn[slot] == null) return;

            if (itemsIn[slot].size > 0)
        {
            
            if (i.hasDurability)  itemsIn[slot].Add(i);
            else itemsIn[slot].DataAt(0).amount++;

        }
        else itemsIn[slot].Add(i);

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
        if (peek(slot) == currentlyEquipped) currentlyEquipped = null;
        itemsIn[slot].Remove(itemsIn[slot].DataAt(0));

    }
    public static void Deq(string slot)
    {
        int hIndex = HashFunc(slot);
        if (peek(hIndex) == currentlyEquipped) currentlyEquipped = null;
        itemsIn[hIndex].Remove(itemsIn[hIndex].DataAt(0));

    }
    public static void Deq(Item slot)
    {
        int hIndex = HashFunc(slot.name);
        if (peek(hIndex) == currentlyEquipped) currentlyEquipped = null;
        itemsIn[hIndex].Remove(itemsIn[hIndex].DataAt(0));
    }

    public static void Use(string slot)
    {
        int hIndex = HashFunc(slot);
        Debug.Log("used");
        peek(hIndex).Use();
  

    }

    public static void Fist()
    {
        if (currentlyEquipped == null) currentlyEquipped = new WeaponItem("Fist", 100000);
        else if (currentlyEquipped.durability <= 0)
        {
            string temp = currentlyEquipped.name;
            Deq(currentlyEquipped);
            if (peek(temp) != null ) currentlyEquipped = (WeaponItem)peek(temp);
            else currentlyEquipped = new WeaponItem("Fist", 100000);
        }
    }

}
