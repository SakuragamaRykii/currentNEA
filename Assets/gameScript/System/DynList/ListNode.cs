using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListNode<U>
{
    public ListNode<U> next { get; set; }
    public ListNode<U> previous { get; set; }

    public U data;

    public ListNode(U data)
    {
        next = null; 
        this.data = data;
        previous = null;
    }


   
}
