using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEnemy : Entity
{
    public GameObject targetObj;
    PathFinder pf;
    public Rigidbody2D rb;
    int index = 0;

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
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (pf.finalPath == null) return;
        Point p = pf.finalPath[index];
        if (!p.area.Contains(rb.position))
            rb.velocity = new Vector2(p.centre.x - rb.position.x, p.centre.y - transform.position.y);

        else index++;
        if (index >= pf.finalPath.Length) index = 0;
    }
}
