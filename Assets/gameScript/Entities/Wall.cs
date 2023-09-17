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
        GameObject hit = CheckCollision();
        if(hit != null && hit.GetComponent<Rigidbody2D>() != null)
        {
            Block(hit.GetComponent<Rigidbody2D>());
        }
    }

    public Rigidbody2D rb;

    //public override GameObject CheckCollision()
    //{
    //    GameObject[] others = FieldManager.qt.Query(gameObject);
    //   // Debug.Log(DynList<GameObject>.ToDList(others));
    //    foreach (GameObject other in others)
    //    {
    //        if (other != gameObject
    //            && other.GetComponent<Rigidbody2D>() != null
    //            && other.GetComponent<Entity>().hitbox.Overlaps(hitbox))
    //        {
    //            rb = other.GetComponent<Rigidbody2D>();
    //           // rb.velocity = new Vector2(-100, 0);
    //            Block(rb);
    //        }

                
    //    }
    //    return null;
    //}

    private void Block(Rigidbody2D rb)
    {
        //rb.position = Vector2.zero;
        if (rb.position.x < transform.position.x-hitbox.width/2 && rb.velocity.x > 0) //from left to right
            rb.velocity = new Vector2(0, rb.velocity.y);
        if (rb.position.x > transform.position.x + hitbox.width / 2 && rb.velocity.x < 0) //right to left 
            rb.velocity = new Vector2(0, rb.velocity.y);
        if (rb.position.y < transform.position.y - hitbox.height / 2 && rb.velocity.y > 0) //down to up
            rb.velocity = new Vector2(rb.velocity.x, 0);
        if (rb.position.y > transform.position.y+hitbox.height/2 && rb.velocity.y < 0) //up to down
            rb.velocity = new Vector2(rb.velocity.x, 0);


    }
}
