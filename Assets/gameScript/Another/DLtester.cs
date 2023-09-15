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
            if(list.size < 5) list.add(obj);
            Debug.Log(list);
            if(list.size == 5) { list.add(obj2); list.add(obj2); }
        }
    }


    // Update is called once per frame
    void Update()
    {
        KeyCode key = KeyCode.Space;
        if (list.Contains(obj2) && Input.GetKeyDown(key)) { Debug.Log(list.Contains(obj2)); list.remove(obj2); }
    }
}
