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
    void FixedUpdate()
    {
        GameObject[] cc = CheckCollision();
       // if(cc != null) Debug.Log(cc.Length);
        foreach(GameObject g in cc)
        {
            if (g.GetComponent<Rigidbody2D>() != null) Block(g.GetComponent<Rigidbody2D>());
        }
    }


    private void Block(Rigidbody2D rb)
    {
        Debug.Log("colliding");

        if(rb.velocity.x < 0)
        {
            if (rb.position.x >= transform.position.x + hitbox.width / 2) rb.velocity = new Vector2(0, rb.velocity.y);
            Debug.Log(rb.velocity);
        }
        else if(rb.velocity.x > 0) 
        {
            if (rb.position.x <= transform.position.x - hitbox.width / 2) rb.velocity = new Vector2(0, rb.velocity.y);
            Debug.Log(rb.velocity);

        }

        if (rb.velocity.y < 0)
        {
            if (rb.position.y >= transform.position.y + hitbox.height / 2) rb.velocity = new Vector2(rb.velocity.x, 0);
            Debug.Log(rb.velocity);

        }
        else if(rb.velocity.y > 0) 
        {
            if (rb.position.y <= transform.position.y - hitbox.height / 2) rb.velocity = new Vector2(rb.velocity.x, 0);
            Debug.Log(rb.velocity);

        }


    }


}
