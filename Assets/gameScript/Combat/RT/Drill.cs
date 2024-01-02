using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : Weapon
{
    float currentRot;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        damage = PlayerStat.attack * Inventory.currentlyEquipped.damagemult;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - (Vector2)transform.position);
        currentRot = transform.localEulerAngles.z;
        if ((currentRot < 45 && currentRot >= 0) ||  (currentRot > 315 && currentRot <= 360)) transform.rotation = Quaternion.identity;
        else if (currentRot <= 315 && currentRot > 225) transform.rotation = Quaternion.Euler(0, 0, -90);
        else if(currentRot < 135 && currentRot >= 45) transform.rotation = Quaternion.Euler(0, 0, 90);
        else transform.rotation = Quaternion.Euler(0, 0, 180);
        setupHitbox();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {

        shootTime -= Time.deltaTime;
        if (shootTime <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);

        }
        Damage();
    }
    protected override void Move()
    {
        
        if ((currentRot < 45 && currentRot >= 0) || (currentRot > 315 && currentRot <= 360)) rb.velocity = new Vector2(0, 10);
        else if (currentRot <= 315 && currentRot > 225) rb.velocity = new Vector2(10, 0);
        else if (currentRot < 135 && currentRot >= 45) rb.velocity = new Vector2(-10, 0);
        else rb.velocity = new Vector2(0, -10);
        

    }

    

}
