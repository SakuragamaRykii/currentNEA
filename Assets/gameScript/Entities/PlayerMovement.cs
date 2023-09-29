
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
