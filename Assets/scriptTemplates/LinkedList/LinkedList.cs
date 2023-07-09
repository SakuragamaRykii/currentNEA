using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class LinkedList 
{
    private LLnode start;
    private int size;

    public LinkedList(int size)
    {
        this.size = size;
        LLnode temp = start;
        for(int i = 1; i<size; i++)
        {
            temp.next = new LLnode();
            temp = temp.next;
        }
    }

    public LLnode index(int index)
    {
        LLnode temp = start;
        for(int i = 1; i<=index; i++)
        {
            if (temp.next != null)
                temp = temp.next;
        }

        return temp;
    }

    public void delete(int index)
    {
        LLnode previous = start;

    }
     

}

