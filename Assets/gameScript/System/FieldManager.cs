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
        objInField = new DynList<GameObject>();
        foreach(Entity e in GameObject.FindObjectsOfType<Entity>())
        {
            objInField.add(e.gameObject);
        }
        float x = field.transform.position.x - field.transform.localScale.x / 2,
            y = field.transform.position.y - field.transform.localScale.y / 2;
        qt = new QuadTree(new Rect(x, y, field.transform.localScale.x, field.transform.localScale.y));

        //Instantiate(Resources.Load("prefabs/Player") );
       

        
    }


}
                                   