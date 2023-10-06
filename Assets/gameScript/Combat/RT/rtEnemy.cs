using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rtEnemy : RTEntity
{
    public GameObject targetObj;
    PathFinder pf;
    public Rigidbody2D rb;
    private void Start()
    {
        setupHitbox();
        pf = GetComponent<PathFinder>();
        rb = GetComponent<Rigidbody2D>();
        pf.targetObj = GameObject.FindGameObjectWithTag("Player");
        attacked = false;
    }
    int index = 0;
    private void Update()
    {
        GameObject cc = CheckCollision();
        if(cc != null && cc.tag.Equals("Player"))
        {
            StartCoroutine(Attack());
        }
        else Move();



    }

    public GameObject weapon;
    bool attacked;
    public IEnumerator Attack()
    {
        attacked = true;
        yield return new WaitForSeconds(1);
        Debug.Log("ATAC");
        attacked = false;
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
