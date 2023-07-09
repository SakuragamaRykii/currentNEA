using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class invStaticHash : MonoBehaviour
{

    int size = 12;
    Items[] table;


    public invStaticHash()
    {
        table = new Items[size];
    }

    public int hFunc(int input)
    {
        return input % 10;
    }
    
    public void add(Items item)
    {
        int ID = item.itemID;
        if (table[hFunc(ID)] == null) {
            table[hFunc(ID)] = item;
        }
        else
        {
            if(ID != table[hFunc(ID)].itemID) { //if the (item you want to add)'s ID is different from the item ID of the item that is in the slot
                //BUFF FUNCTION (WILL ADD LATER)
            }
            else
            {

            }

        }
    }

}
