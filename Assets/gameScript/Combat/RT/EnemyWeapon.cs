using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Entity
{
    Vector2 target;
    public float speed;
    public float shootTime;
    public static float weaponCooldown;
    public float damage;

    void Start() {
        setupHitbox();
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        speed = 0.15f;
        shootTime = 0.3f;
        weaponCooldown = 0f ;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
   
        shootTime -= Time.deltaTime;
        Attack();
        if(shootTime<= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }

    private void Attack()
    {
        GameObject[] cc = CheckCollision();
        if (cc == null) return;
        foreach (GameObject g in cc)
        {
            Debug.Log(g);
            if (g.tag.Equals("Player"))
            {
                Debug.Log("HELLO");
                PlayerStat.TakeDamage(damage);
                shootTime = 0;


            }
        }


    }
}


    


