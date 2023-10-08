using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rtPlayer : Entity
{
    public static rtPlayer instance; //make this a singleton
    private Rigidbody2D rb;
    public  GameObject weapon;
    void Start()
    {
        if (instance == null) instance = this; else Destroy(gameObject);
        rb = GetComponent<Rigidbody2D>();

        setupHitbox();
       
    }


    void Update()
    {
        Move();
        
        ManageColEvent(CheckCollision());
        Attack();

    }

    void Attack()//GameObject to
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
           Instantiate(weapon, transform.position, transform.rotation);

        }
    }
    void ManageColEvent(GameObject[] type)
    {
        if (type == null) return;

        if (hasTag(type, "Wall")) rb.velocity = -rb.velocity;

    }


    private void Move()
    {
        float hor = Input.GetAxis("Horizontal") * 15;
        float ver = Input.GetAxis("Vertical") * 15;
        Vector2 move = new Vector2(hor, ver);
        rb.velocity = move;
        Quaternion to = Quaternion.LookRotation(Vector3.forward, move);
        if (hor != 0 || ver != 0)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, to, 2);
    }
}
