
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
        
        ManageColEvent(CheckCollision());
        Move();


    }


    void ManageColEvent(GameObject type){
        //        Debug.Log(type);
        if (type == null) return;

        switch (type.tag)
        {
            case "Enemy":
                //SceneManager.LoadScene("CombatScene");
                Debug.Log("scary guy");
                break;
            case "Wall":
                Entity wall = type.GetComponent<Entity>();
                float left = type.transform.position.x - wall.hitbox.width / 2;
                float right = type.transform.position.x + wall.hitbox.width / 2;
                float down = type.transform.position.y - wall.hitbox.height / 2;
                float up = type.transform.position.y + wall.hitbox.height / 2;
                if (rb.position.x <= left && rb.velocity.x > 0) //from left to right
                {    Debug.Log("left to right");
                rb.velocity = new Vector2(0, rb.velocity.y);
                    Debug.Log(rb.velocity);
                }
                if (rb.position.x >= right && rb.velocity.x < 0) //right to left
                {
                    Debug.Log("right to left");
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    Debug.Log(rb.velocity);

                }
                if (rb.position.y <= down && rb.velocity.y > 0) //down to up
                {   Debug.Log("down to up");
                rb.velocity = new Vector2(rb.velocity.x, 0);
                    Debug.Log(rb.velocity);
                }
                if (rb.position.y >= up && rb.velocity.y < 0) //up to down
                { Debug.Log("up to down");
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    Debug.Log(rb.velocity);

                }


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
