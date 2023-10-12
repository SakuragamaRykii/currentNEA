using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Entity
{
    float shootTime;
    Vector2 mousePos;
    public float damage;

    void Start()
    {
        shootTime = 0.15f;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - (Vector2)transform.position);
        damage = PlayerStat.attack/2;
        setupHitbox();
    }


    void Update()
    {
        shootTime -= Time.deltaTime;
        // mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(transform.rotation);
        Damage();
        transform.localPosition = Vector2.MoveTowards(transform.position, mousePos, 0.1f);

        if (shootTime <= 0 )
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            
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
            Debug.Log(cc);

            foreach (GameObject g in cc)
            {
                if (g.tag.Equals("Enemy"))
                {
                    g.GetComponent<rtEnemy>().es.TakeDamage(damage);
                    Debug.Log(g.GetComponent<rtEnemy>().es.currentHP);
                    weaponCoolDown = 0.3f;
                    shootTime = 0;
            
                }

            }
            }
        }
        else weaponCoolDown -= Time.deltaTime;
            
     
    }
}
