using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rtPlayer : Entity, IOverPrevention
{
    public static rtPlayer instance; //make this a singleton
    private Rigidbody2D rb;
    public GameObject weapon;
    public float currentCd;
    void Start()
    {
        if (instance == null) instance = this; else Destroy(gameObject);
        rb = GetComponent<Rigidbody2D>();
       // weapon = Inventory.currentlyEquipped.weapon;
        setupHitbox();
       
    }
    public void PreventHPOver()
    {
        if (PlayerStat.currentHP > PlayerStat.maxHP) PlayerStat.currentHP = PlayerStat.maxHP;

    }

    void Update()
    {
        Inventory.Fist();
        weapon = Inventory.currentlyEquipped.weapon;
        Debug.Log(Inventory.currentlyEquipped.name);
        Move();
        PreventHPOver();
        ManageColEvent(CheckCollision());
        Attack();
       // Debug.Log("currentcd " + weaponCd);
    }

    void Attack()//GameObject to
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentCd <= 0)
        {
            Instantiate(weapon, transform.position, transform.rotation);
            currentCd = Inventory.currentlyEquipped.cd;
        }
        else currentCd -= Time.deltaTime;
    }
    void ManageColEvent(GameObject[] type)
    {
        if (type == null) return;

        //if (hasTag(type, "Wall")) rb.velocity = -rb.velocity;

    }


    private void Move()
    {
        float hor = Input.GetAxis("Horizontal") * 15;
        float ver = Input.GetAxis("Vertical") * 15;
        Vector2 move = new Vector2(hor, ver);
        rb.velocity = move;
        Quaternion to = Quaternion.LookRotation(Vector3.forward, move);
        if (hor != 0 || ver != 0)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, to, 2);
    }
}
