using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rtEnemy : Entity
{
    public GameObject targetObj;
    PathFinder pf;
    public Rigidbody2D rb;
    private void Start()
    {
        setupHitbox();
        pf = GetComponent<PathFinder>();
        rb = GetComponent<Rigidbody2D>();
        targetObj = pf.targetObj;
    }
    int index = 0;
    private void Update()
    {

        if (pf.finalPath == null) return;
        Point p = pf.finalPath[index];
        if (!p.area.Contains(rb.position))
            rb.velocity = new Vector2(p.centre.x - rb.position.x, p.centre.y - transform.position.y);

        else index++;
        if (index >= pf.finalPath.Length) index = 0;

    }

    public void Move()
    {
        if (pf.finalPath == null) return;
        foreach(Point p in pf.finalPath)
        {
            while (!p.area.Contains(transform.position)){

            }
        }
    }

}
