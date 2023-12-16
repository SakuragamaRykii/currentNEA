using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLtester : MonoBehaviour
{
    DynList<GameObject> list;
    public GameObject obj, obj2;
    void Start()
    {
        list = new DynList<GameObject>();
        StartCoroutine(add());
    }
    public IEnumerator add()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if(list.size < 5) list.add(new GameObject());
            else
            {
                foreach (GameObject go in list)
                {
                    Debug.Log(go);
                }
            }


        }
    }


    // Update is called once per frame
    void Update()
    {


    }
}
