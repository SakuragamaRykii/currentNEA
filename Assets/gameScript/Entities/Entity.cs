using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour, IDataPersistence //all entities in subject to the collision detection should inherit this 
{

    public Rect hitbox; //all hitboxes will be rectangular
    protected float x, y;

    public void setupHitbox()
    {
        x = transform.position.x - transform.localScale.x / 2;
        y = transform.position.y - transform.localScale.y / 2;
        hitbox = new Rect(x, y, transform.localScale.x, transform.localScale.y);

    }
    public virtual void LoadData(GameData data)
    {


    }
    public virtual void SaveData(ref GameData data)
    {

    }

    //public GameObject other;
    public virtual GameObject[] CheckCollision() {
        if (!FieldManager.qt.grid.Contains(gameObject.transform.position))
        {
            transform.position = new Vector3(0, 0, -5);
            return null;
        }
        DynList<GameObject> result = new DynList<GameObject>();
        hitbox.position = (Vector2)transform.position - hitbox.size / 2;
        GameObject[] others = FieldManager.qt.Query(gameObject);
      //  Debug.Log(DynList<GameObject>.ToDList(others));
        foreach (GameObject other in others)
        {
            
             if(other != gameObject && other.gameObject != null && other.GetComponent<Entity>() != null && other.GetComponent<Entity>().hitbox.Overlaps(hitbox))
             {
                result.Add(other);
             }
        }
        return result.toArr();

        //if (hitbox.Intersects(other.GetComponent<Entity>().hitbox)) Debug.Log("HIT");
    }

    public bool HasTag(GameObject[] target ,string tag)
    {
        if (target == null) return false;
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
