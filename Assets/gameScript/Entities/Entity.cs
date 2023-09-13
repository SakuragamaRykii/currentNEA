using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour //all entities should inherit this 
{
    //public bool collided { get; private set; }

    public enum colWith
    {
        NONE, ENEMY, WALL, INTERACTABLE, PLAYER, WEAPON
    }

    public Rect hitbox; //all hitboxes will be rectangular
    float x, y;

    public void setupHitbox()
    {
        // float width = transform.localScale.x, height = transform.localScale.y;
        // hitbox = new Block(transform.position, width, height);
        x = transform.position.x - transform.localScale.x / 2;
        y = transform.position.y - transform.localScale.y / 2;
        hitbox = new Rect(x, y, transform.localScale.x, transform.localScale.y);
        FieldManager.qt.Insert(gameObject);

        StartCoroutine(ManageIns());

    }

    public IEnumerator ManageIns()
    {

        while (true)
        {
            Vector2 oldPos = transform.position;
            yield return new WaitForSeconds(0.2f);
            if (transform.position.x != oldPos.x || transform.position.y != oldPos.y)
            {
                FieldManager.qt.Insert(gameObject);
            }
        }

    }

    //public GameObject other;
    public colWith checkCollision() {
        x = transform.position.x - transform.localScale.x / 2;
        y = transform.position.y - transform.localScale.y / 2;
        hitbox.position = new Vector2(x, y);
        //hitbox.position = transform.position;
        GameObject[] others = FieldManager.qt.Query(gameObject);
        foreach(GameObject other in others)
         {
            
             if(other != gameObject && other.GetComponent<Entity>() != null && other.GetComponent<Entity>().hitbox.Overlaps(hitbox))
             {
                 Debug.Log("HIT");
                //collided = true;
                switch (other.tag)
                {
                    case "Enemy": { Debug.Log("enemy"); return colWith.ENEMY; }

                    case "Utility": { Debug.Log("inte"); return colWith.INTERACTABLE; }

                    case "Wall": {Debug.Log("wall"); return colWith.WALL; }

                    case "Weapon": { Debug.Log("weapon"); return colWith.WEAPON; }

                    case "Player": {Debug.Log("player"); return colWith.PLAYER; }

                    default: { Debug.Log("none"); return colWith.NONE; }
                }
                 
             }
         }
        return colWith.NONE;

        //if (hitbox.Intersects(other.GetComponent<Entity>().hitbox)) Debug.Log("HIT");
    }

    void Start()
    {
        setupHitbox();
        

    }
    void Update()
    {
        checkCollision();

    }





}
