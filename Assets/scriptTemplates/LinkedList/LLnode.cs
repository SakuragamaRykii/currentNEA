using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LLnode
{
    public LLnode next{get; set;}   

//    ---MEMBER DATA TO STORE---
    public int data { get; set; }

    //example
//    --------------------------

    public LLnode(int add)
    {
        data = add;
    }
    public LLnode()
    {
        data = 0;
    }
}
