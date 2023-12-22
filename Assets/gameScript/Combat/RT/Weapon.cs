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


    void FixedUpdate()
    {

        Move();
    }
    private void Update()
    {
        shootTime -= Time.deltaTime;

        Damage();

        if (shootTime <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    protected virtual void Move()
    {
        transform.localPosition = Vector2.MoveTowards(transform.position, mousePos, 0.5f);
    }


    protected virtual void Damage()
    {
        GameObject[] cc = CheckCollision();
        if (cc != null)
        {
             Debug.Log("hit");

            foreach (GameObject g in cc)
            {
                Debug.Log(g);
               // if (g.tag.Equals("Enemy"))
               if(g.GetComponent<rtEnemy>() != null)
                {
                    g.GetComponent<rtEnemy>().es.TakeDamage(damage);
                    shootTime = 0;

                }

            }
        }

    }


}



