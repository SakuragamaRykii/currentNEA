using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
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
