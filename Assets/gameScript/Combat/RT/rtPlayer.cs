using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class rtPlayer : Entity, IOverPrevention
{
    public static rtPlayer instance; //make this a singleton
    private Rigidbody2D rb;
    public GameObject weapon;
    public float weaponCd;

    void Start()
    {
        if (instance == null) instance = this; else Destroy(gameObject);
        rb = GetComponent<Rigidbody2D>();
        setupHitbox();
        Inventory.Fist();
        weapon = Inventory.currentlyEquipped.weapon;
        weaponCd = Inventory.currentlyEquipped.cd;
    }
    public void PreventHPOver()
    {
        if (PlayerStat.currentHP > PlayerStat.maxHP) PlayerStat.currentHP = PlayerStat.maxHP;

    }

    void Update()
    {
        Inventory.Fist();
        weapon = Inventory.currentlyEquipped.weapon;
        Move();
        PreventHPOver();
        ManageColEvent(CheckCollision());
        if (Inventory.currentlyEquipped.durability <= 0) Inventory.Deq(Inventory.currentlyEquipped);
        Attack();
        Debug.Log("currentcd " + weaponCd);
    }

    void Attack()//GameObject to
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && weaponCd <= 0)
        {
            Instantiate(weapon, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation);
            weaponCd = Inventory.currentlyEquipped.cd;
            Inventory.currentlyEquipped.durability--;
        }
        else weaponCd-=Time.deltaTime;
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
