
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerMovement : Entity
{

    public static PlayerMovement instance; //make this a singleton
    private Rigidbody2D rb;
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();

        setupHitbox();

    }


    void Update()
    {
        Move();
        
        ManageColEvent(CheckCollision());


    }

    void ManageColEvent(colWith type){
//        Debug.Log(type);
        switch (type)
        {
            case colWith.ENEMY:
                //SceneManager.LoadScene("CombatScene");
                Debug.Log("scary guy");
                break;
            case colWith.WALL:
                if (rb.velocity.x < 0)
                    rb.velocity = new Vector2(0, rb.velocity.y);
                if (rb.velocity.x > 0)
                    rb.velocity = new Vector2(-1, rb.velocity.y);
                if (rb.velocity.y < 0)
                    rb.velocity = new Vector2(rb.velocity.x,0);
                if (rb.velocity.y > 0)
                    rb.velocity = new Vector2(rb.velocity.x, -1);


                break;


        }
      
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
