using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour //all entities should inherit this 
{
    public bool collided { get; private set; }

    public Block hitbox; //all hitboxes will be rectangular

    public void addToFM()
    {
        FieldManager.qt.insert(gameObject);
        FieldManager.objInField.add(gameObject) ;
    }

    public void setupHitbox()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.drawMode = SpriteDrawMode.Sliced;
        float width = sr.size.x, height = sr.size.y;
        hitbox = new Block(transform.position, width, height);
    }

    public void checkCollision() {
        GameObject[] others = FieldManager.qt.query(hitbox);
        foreach(GameObject other in others)
        {
            if(this.gameObject != other && hitbox.intersects(other.GetComponent<Entity>().hitbox))
            {
                Debug.Log("HIT");
                collided = true;
                break;
            }
        }

    }

    void Awake()
    {
        addToFM();
        setupHitbox();

    }
    void Update()
    {
        checkCollision();
        hitbox.centrePos = transform.position;
    }





}
