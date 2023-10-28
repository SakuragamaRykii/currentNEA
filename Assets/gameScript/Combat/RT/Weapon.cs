using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Entity
{
    public float shootTime;
    protected Vector2 mousePos;
    public float damage;

    void Start()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - (Vector2)transform.position);
        damage = PlayerStat.attack * Inventory.currentlyEquipped.damagemult;
        
        setupHitbox();
    }


    void Update()
    {
        Move();
    }

    protected void Move()
    {
        shootTime -= Time.deltaTime;
        // mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(transform.rotation);
        Damage();
        transform.localPosition = Vector3.MoveTowards(transform.position, mousePos, 0.1f);

        if (shootTime <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);

        }
    }

    protected virtual void Damage()
    {
        GameObject[] cc = CheckCollision();
        if (cc != null)
        {
            Debug.Log(cc.ToString());

            foreach (GameObject g in cc)
            {
                Debug.Log(g);
                if (g.tag.Equals("Enemy"))
                {
                    g.GetComponent<rtEnemy>().es.TakeDamage(damage);
                    // Debug.Log(g.GetComponent<rtEnemy>().es.currentHP);
                    shootTime = 0;

                }

            }
        }
    }
}
