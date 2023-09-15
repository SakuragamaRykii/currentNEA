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

    public override colWith CheckCollision()
    {
        GameObject[] others = FieldManager.qt.Query(gameObject);
        foreach (GameObject other in others)
        {
            if (other != gameObject
                && other.GetComponent<Rigidbody2D>() != null
                && other.GetComponent<Entity>().hitbox.Overlaps(hitbox))
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                Block(rb);
            }
        }
        return colWith.NONE;
    }

    private void Block(Rigidbody2D rb)
    {
        if(rb.position.x < transform.position.x && rb.velocity.x > 0) //from left to right
            rb.velocity = new Vector2(0, rb.velocity.y);
        else if (rb.position.x > transform.position.x && rb.velocity.x < 0) //right to 
            rb.velocity = new Vector2(0, rb.velocity.y);
        else if (rb.position.x < transform.position.x && rb.velocity.x > 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else if (rb.position.x < transform.position.x && rb.velocity.x > 0)
            rb.velocity = new Vector2(0, rb.velocity.y);


    }
}
