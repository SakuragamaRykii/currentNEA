using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static QuadTree qt;
    public GameObject field;
    public static FieldManager only;
    [SerializeField] private GameObject player, enemyTemp, wallTemp;
    [SerializeField] private GameObject utilTemp;


    private float x, y;

    void Awake()
    {
        if (only == null) only = this; else Destroy(gameObject);
       // if (!hasReset) return;

        Instantiate(field);

        x = field.transform.position.x - field.transform.localScale.x / 2;
        y = field.transform.position.y - field.transform.localScale.y / 2;
        qt = new QuadTree(new Rect(x, y, field.transform.localScale.x, field.transform.localScale.y));


        
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
       // Debug.Log(qt.objsInGrid);
    }




}
                                   