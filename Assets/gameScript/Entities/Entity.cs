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
    public virtual GameObject[] CheckCollision() {
        if (!FieldManager.qt.grid.Contains(gameObject.transform.position))
        {
            transform.position = new Vector3(0, 0, -5);
            return null;
        }
        DynList<GameObject> result = new DynList<GameObject>();
        x = transform.position.x - transform.localScale.x / 2;
        y = transform.position.y - transform.localScale.y / 2;
        hitbox.position = new Vector2(x, y);
        GameObject[] others = FieldManager.qt.Query(gameObject);
      //  Debug.Log(DynList<GameObject>.ToDList(others));
        foreach (GameObject other in others)
        {
            
             if(other != gameObject && other.gameObject != null && other.GetComponent<Entity>() != null && other.GetComponent<Entity>().hitbox.Overlaps(hitbox))
             {
                result.add(other);
             }
        }
        return result.toArr();

        //if (hitbox.Intersects(other.GetComponent<Entity>().hitbox)) Debug.Log("HIT");
    }

    public bool hasTag(GameObject[] target ,string tag)
    {
        foreach(GameObject g in target)
        {
            if (g.tag.Equals(tag)) return true;
        }
        return false;
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
