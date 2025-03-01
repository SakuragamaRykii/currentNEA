using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePickup : Entity, IRandomDrop
{
    string[] consPool;
    void Start()
    {
        consPool = new string[] { "Banana" , "Spig Beed", "Temp Attack Boost"};
        setupHitbox();
    }

    void Update()
    {
        DynList<GameObject> cc = DynList<GameObject>.ToDList(CheckCollision());
        if (cc.HasTag("Player"))
        {
            Inventory.insert(new Item(Pick(consPool)));
            gameObject.SetActive(false);
        }
    }


    public string Pick(string[] pool)
    {
        int index = Random.Range(0, pool.Length);
        return pool[index];
    }
}
