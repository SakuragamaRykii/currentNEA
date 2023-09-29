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


    private void Block(Rigidbody2D rb)
    {
        Debug.Log("helo");
        //rb.position = Vector2.zero;
        if (rb.position.x <= transform.position.x-hitbox.width/2 && rb.velocity.x > 0) //from left to right
            rb.velocity = new Vector2(0, rb.velocity.y);
        if (rb.position.x >= transform.position.x + hitbox.width / 2 && rb.velocity.x < 0) //right to left 
            rb.velocity = new Vector2(0, rb.velocity.y);
        if (rb.position.y <= transform.position.y - hitbox.height / 2 && rb.velocity.y > 0) //down to up
            rb.velocity = new Vector2(rb.velocity.x, 0);
           // Debug.Log("does this work");
        if (rb.position.y >= transform.position.y+hitbox.height/2 && rb.velocity.y < 0) //up to down
            rb.velocity = new Vector2(rb.velocity.x, 0);

    }


}
