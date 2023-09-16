using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        setupHitbox();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
    }

    //public Rigidbody2D rb;

    public override colWith CheckCollision()
    {
        GameObject[] others = FieldManager.qt.Query(gameObject);
        Debug.Log(DynList<GameObject>.ToDList(others));
        foreach (GameObject other in others)
        {
            if (other != gameObject
                && other.GetComponent<Rigidbody2D>() != null
                && other.GetComponent<Entity>().hitbox.Overlaps(hitbox))
            {
                Debug.Log("hitt");
               // rb = other.GetComponent<Rigidbody2D>();
               // Block(rb);
            }
        }
        return colWith.NONE;
    }   

    private void Block(Rigidbody2D rb)
    {
     /*   if (rb.position.x < transform.position.x && rb.velocity.x > 0)
        { //from left to right
            Debug.Log("LEFT TO RIGHT");
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (rb.position.x > transform.position.x && rb.velocity.x < 0) //right to left 
            rb.velocity = new Vector2(0, rb.velocity.y);
        else if (rb.position.y < transform.position.y && rb.velocity.y > 0) //down to up
            rb.velocity = new Vector2(rb.velocity.x, 0);
        else if (rb.position.y > transform.position.y && rb.velocity.y < 0) //up to down
            rb.velocity = new Vector2(rb.velocity.x, 0);

        */
    }
}
