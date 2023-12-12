using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynlistTest : MonoBehaviour
{
    // Start is called before the first frame update
    DynList<GameObject> list;
    public GameObject dummy1, dummy2;

    void Start()
    {
        list = new DynList<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(list.size < 5)
        {
            list.Add(dummy1);
        }
        if(list.size == 5) list.Add(dummy2);
        if(list.size>5) list.Remove(dummy1);
        Debug.Log(list);

    }
}
