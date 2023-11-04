using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rtEnemy : Entity
{
    public GameObject targetObj;
    PathFinder pf;
    public Rigidbody2D rb;
    public EnemyStat es;
    Rect atkArea;
    private void Start()
    {
        atkArea = new Rect(transform.position.x - 7, transform.position.y - 7, 7, 7);
        es = GetComponent<EnemyStat>();
        Debug.Log(es.attack);
        setupHitbox();
        pf = GetComponent<PathFinder>();
        rb = GetComponent<Rigidbody2D>();
        pf.targetObj = GameObject.FindGameObjectWithTag("Player");
        attacked = false;

    }
    int index = 0;
    private void Update()
    {
        CheckCollision();
        if(isPlayerIn() && !attacked)
        {
            StartCoroutine(Attack());
        }
        else Move();

        if (es.IsDead())
        {
            RTManager.amount--;
            es.Die();
        }
    }

    private bool isPlayerIn()
    {
        atkArea.position = new Vector2(transform.position.x - 5, transform.position.y - 5);
        return atkArea.Contains(GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    public GameObject weapon;
    bool attacked;
    public IEnumerator Attack()
    {
        attacked = true;
        yield return new WaitForSeconds(1);
        EnemyWeapon ew =  Instantiate(weapon, transform.position, transform.rotation).GetComponent<EnemyWeapon>();
        ew.damage = es.attack;
        Debug.Log(ew.damage);
        attacked = false;
    }

    public void Move()
    {
        if (pf.finalPath == null) return;
        Point p = pf.finalPath[index];
        if (!p.area.Contains(rb.position)) { 
            rb.velocity = new Vector2(p.centre.x - rb.position.x, p.centre.y - transform.position.y);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, p.centre);
        }
        else index++;
        if (index >= pf.finalPath.Length) index = 0;
    }

}
