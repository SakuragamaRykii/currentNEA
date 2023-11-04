using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePickup : Entity, IRandomDrop
{
    string[] consPool;
    void Start()
    {
        consPool = new string[] {"Banana", "Spig Beed", "Temp Attack Boost"};
        setupHitbox();
    }

    void Update()
    {
        GameObject[] cc = CheckCollision();
        if (hasTag(cc, "Player"))
        {
            Inventory.insert(new Item(Pick(consPool)));
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }


    public string Pick(string[] pool)
    {
        int index = Random.Range(0, pool.Length);
        return pool[index];
    }
}
