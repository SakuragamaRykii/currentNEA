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
        setupHitbox();
        pf = GetComponent<PathFinder>();
        rb = GetComponent<Rigidbody2D>();
        pf.targetObj = GameObject.FindGameObjectWithTag("Player");
        attacked = false;

    }

    int index = 0;
    float counter = 0;
    private void Update()
    {
        Debug.Log(DynList<GameObject>.ToDList(CheckCollision()));
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            pf.Find();
            counter = 0.3f;
        }
    }
    private void FixedUpdate()
    {

        if (isPlayerIn() && !attacked)
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
        attacked = false;
    }

    public void Move()
    {
        if (pf.finalPath == null) return;
        Point p = pf.finalPath[index];
        if (!p.centre.Equals(rb.position)) {
            Quaternion to = Quaternion.LookRotation(Vector3.forward,  p.centre - (Vector2)transform.position );
            rb.velocity = new Vector2(p.centre.x - rb.position.x, p.centre.y - transform.position.y);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, to, 30);
        }
        else index++;
        if (index >= pf.finalPath.Length) index = 0;
    }

}
