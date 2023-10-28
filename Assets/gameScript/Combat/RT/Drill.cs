using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : Weapon
{
    void Start()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        damage = PlayerStat.attack * Inventory.currentlyEquipped.damagemult;
        float currentRot = Quaternion.LookRotation(Vector3.forward, mousePos - (Vector2)transform.position).z;
        if (currentRot > -45 && currentRot < 45) transform.rotation = Quaternion.identity;
        else if (currentRot > -135 && currentRot < -45) transform.rotation = Quaternion.Euler(0, 0, -90);
        else if(currentRot < 135 && currentRot > 45) transform.rotation = Quaternion.Euler(0, 0, 90);
        else transform.rotation = Quaternion.Euler(0, 0, 180);
        setupHitbox();

    }



    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
