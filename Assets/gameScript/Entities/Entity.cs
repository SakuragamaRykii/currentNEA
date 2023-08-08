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

    public Block hitbox; //all hitboxes will be rectangular

    public void addToFM()
    {
        FieldManager.qt.insert(gameObject);
        FieldManager.objInField.add(gameObject) ;
    }

    public void setupHitbox()
    {
        float width = transform.localScale.x, height = transform.localScale.y;
        hitbox = new Block(transform.position, width, height);
        
    }

    //public GameObject other;
    public colWith checkCollision() {
        hitbox.position = transform.position;
        DynList<GameObject> others = FieldManager.qt.query(gameObject);
        foreach(GameObject other in others)
         {
            
             if(other != gameObject && hitbox.Intersects(other.GetComponent<Entity>().hitbox))
             {
                 Debug.Log("HIT");
                 //collided = true;
                switch (other.tag)
                {
                    case "Enemy": return colWith.ENEMY;

                    case "Utility": return colWith.INTERACTABLE;

                    case "Wall": return colWith.WALL;

                    case "Weapon": return colWith.WEAPON;

                    case "Player": return colWith.PLAYER;

                    default: return colWith.NONE;
                }
                 
             }
         }
        return colWith.NONE;

        //if (hitbox.Intersects(other.GetComponent<Entity>().hitbox)) Debug.Log("HIT");
    }

    void Start()
    {
        addToFM();
        setupHitbox();
        

    }
    void Update()
    {
        checkCollision();
        //foreach(GameObject g in FieldManager.objInField)
        //{
            //Debug.Log(g.name);
        //}
        //Debug.Log(FieldManager.objInField.dataAt(1).name + " is at index 1 ");
        //Debug.Log(FieldManager.objInField.dataAt(0).name + " is at index 0   ");
    }





}
