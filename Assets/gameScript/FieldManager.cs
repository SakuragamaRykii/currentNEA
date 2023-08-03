using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static QuadTree qt;
    public GameObject field;

    public int minEnemies = 3, maxEnemies = 7,
        minInte = 3, maxInte = 10,
        minWall = 3, maxWall = 12;
    public static List<GameObject> objInField;


    void Awake()
    {
        Instantiate(field);
        qt = new QuadTree(new Block(transform.position, field.transform.localScale.x, field.transform.localScale.y));
        objInField = new List<GameObject>();
                 
    }

    // Update is called once per frame
    void Update()
    {     
        
    }
}
                                   