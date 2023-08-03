using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour //all entities should inherit this 
{
    public bool collided { get; private set; }

    public Block hitbox; //all hitboxes will be rectangular

    public void add()
    {
        FieldManager.qt.insert(gameObject);
        FieldManager.objInField.Add(gameObject);
    }

    public void setupHitbox()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.drawMode = SpriteDrawMode.Sliced;
        float width = sr.size.x, height = sr.size.y;
        hitbox = new Block(transform.position, width, height);
    }




}
