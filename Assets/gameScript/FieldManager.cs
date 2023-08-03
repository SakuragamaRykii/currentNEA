using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static QuadTree qt;
    public GameObject field;
    void Awake()
    {
        Instantiate(field);
        qt = new QuadTree(new Block(transform.position, field.transform.localScale.x, field.transform.localScale.y));
                 
    }

    // Update is called once per frame
    void Update()
    {     
        
    }
}
                                   