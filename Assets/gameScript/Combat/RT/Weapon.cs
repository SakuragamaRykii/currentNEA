using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Entity
{
    float shootTime;
    bool hitting;
    Vector2 start;
    Vector2 mousePos;
    Rigidbody2D rb;
    public float damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootTime = 0.2f;
        hitting = false;
        start = transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - (Vector2)transform.position);
        damage = PlayerStat.attack;
    }


    void Update()
    {
        if (shootTime <= 0 )
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            
        }
        else
        {
            shootTime -= Time.deltaTime;
            // mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(transform.rotation);
            Damage();
            transform.localPosition = Vector2.MoveTowards(transform.position, mousePos, 0.1f);
        }


    }

    public static float weaponCoolDown;
    public virtual void Damage()
    {
        if(weaponCoolDown <= 0 )
        {
            GameObject[] cc = CheckCollision();
            if(cc != null)
            {
                foreach(GameObject g in cc)
                {
                    if (g.tag.Equals("Enemy")) g.GetComponent<rtEnemy>().es.TakeDamage(damage);
                    
                }
            }
            weaponCoolDown = 0.5f;
        }
        else weaponCoolDown -= Time.deltaTime;
            
     
    }
}
