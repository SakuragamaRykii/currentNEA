using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour //all entities should inherit this 
{
    //public bool collided { get; private set; }


    public Rect hitbox; //all hitboxes will be rectangular
    protected float x, y;

    public void setupHitbox()
    {
        x = transform.position.x - transform.localScale.x / 2;
        y = transform.position.y - transform.localScale.y / 2;
        hitbox = new Rect(x, y, transform.localScale.x, transform.localScale.y);
       // FieldManager.qt.Insert(gameObject);

      //  StartCoroutine(ManageIns());

    }


    //public GameObject other;
    public virtual GameObject CheckCollision() {
        x = transform.position.x - transform.localScale.x / 2;
        y = transform.position.y - transform.localScale.y / 2;
        hitbox.position = new Vector2(x, y);
        GameObject[] others = FieldManager.qt.Query(gameObject);
      //  Debug.Log(DynList<GameObject>.ToDList(others));
        foreach (GameObject other in others)
        {
            
             if(other != gameObject && other.GetComponent<Entity>() != null && other.GetComponent<Entity>().hitbox.Overlaps(hitbox))
             {
               //  Debug.Log("HIT");
                return other;
             }
         }
       // Debug.Log("none");
        return null;

        //if (hitbox.Intersects(other.GetComponent<Entity>().hitbox)) Debug.Log("HIT");
    }

    void Start()
    {
        setupHitbox();
        

    }
    void Update()
    {
        CheckCollision();

    }





}
