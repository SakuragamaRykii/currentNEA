using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static QuadTree qt;
    public GameObject field;
    public static FieldManager only;

    public int minEnemies = 3, maxEnemies = 7,
        minInte = 3, maxInte = 10,
        minWall = 3, maxWall = 12;
    public static DynList<GameObject> objInField;


    void Awake()
    {
        if (only == null) only = this; else Destroy(gameObject);

        Instantiate(field);
        qt = new QuadTree(new Block(transform.position, field.transform.localScale.x, field.transform.localScale.y));
        objInField = new DynList<GameObject>();

        //Instantiate(Resources.Load("prefabs/Player") );
       

        
    }


}
                                   