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
    private float x, y;

    void Awake()
    {
        if (only == null) only = this; else Destroy(gameObject);

        Instantiate(field);

        x = field.transform.position.x - field.transform.localScale.x / 2;
        y = field.transform.position.y - field.transform.localScale.y / 2;
        qt = new QuadTree(new Rect(x, y, field.transform.localScale.x, field.transform.localScale.y));

        //Instantiate(Resources.Load("prefabs/Player") );

       

        
    }
    private void FixedUpdate()
    {

        qt = new QuadTree(new Rect(x, y, field.transform.localScale.x, field.transform.localScale.y));
        //Debug.Log(qt.objsInGrid);
       // Debug.Log(DynList<Entity>.ToDList(GameObject.FindObjectsOfType<Entity>()));

        foreach(Entity e in GameObject.FindObjectsOfType<Entity>())
        {
            qt.Insert(e.gameObject);
            //qt.find(e.gameObject);
        }
        Debug.Log(qt.objsInGrid);
    }


}
                                   