using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Entity, IRandomDrop
{
    private string[] weaponPool;

    public string thisDrop;
    public string Pick(string[] pool)
    {
        int index = Random.Range(0, pool.Length);
        return pool[index];
    }
    private void Start()
    {
        weaponPool = new string[] { "Sword", "Hammer", "Drill" }; 
        setupHitbox();
        thisDrop = Pick(weaponPool);
        Debug.Log(thisDrop);
    }

    private void Update()
    {
        DynList<GameObject> cc = DynList<GameObject>.ToDList(CheckCollision());
            
        if (cc.HasTag("Player"))
        {
            Inventory.insert(new WeaponItem(thisDrop, Random.Range(60, 101)));
            gameObject.SetActive(false);
        }
    }
}
