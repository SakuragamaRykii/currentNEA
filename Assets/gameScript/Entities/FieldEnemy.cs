using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEnemy : Entity
{
    public GameObject targetObj;
    PathFinder pf;
    public Rigidbody2D rb;
    public GameObject within;
    float ms = 1; //movement speed

    // Start is called before the first frame update
    void Start()
    {
        setupHitbox();
        pf = GetComponent<PathFinder>();
        rb = GetComponent<Rigidbody2D>();
        pf.targetObj = GameObject.FindGameObjectWithTag("Player");
        //attacked = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        
    }

    public void Move()
    { //THE START POINT OF THE PATH IS IN AS THE LAST ELEMENT OF FINALPATH
        hitbox.position = new Vector2(rb.position.x - hitbox.width / 2, rb.position.y - hitbox.height / 2);
        if (pf.finalPath == null) return;
        if (pf.index < 0) return;
            Point moveTo = pf.finalPath[pf.index];
        if (!moveTo.centre.Equals(transform.position))
        {
            rb.position = Vector2.MoveTowards(rb.position, moveTo.centre, 0.1f);
        }
        else pf.index--;
    }
}
