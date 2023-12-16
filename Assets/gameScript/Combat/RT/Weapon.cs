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
        Debug.Log(transform.localEulerAngles.z);

        Move();
    }

    protected virtual void Move()
    {
        shootTime -= Time.deltaTime;
        Damage();
        transform.localPosition = Vector2.MoveTowards(transform.position, mousePos, 0.1f);

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
            // Debug.Log(cc.ToString());

            foreach (GameObject g in cc)
            {
                if (g.tag.Equals("Enemy"))
                {
                    g.GetComponent<rtEnemy>().es.TakeDamage(damage);
                    Debug.Log(g.GetComponent<rtEnemy>().es.currentHP);
                    shootTime = 0;

                }

            }
        }

    }


}



