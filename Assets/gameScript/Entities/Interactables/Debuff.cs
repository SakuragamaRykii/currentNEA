using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : Entity, IRandomDrop
{
    void Start()
    {
        setupHitbox();
        
    }
    public string Pick(string[] pool)
    {
        return "";
    }

    // Update is called once per frame
    void Update()
    {
        DynList<GameObject> cc = DynList<GameObject>.ToDList(CheckCollision());
        if (cc.HasTag("Player"))
        {
            PlayerStat.TakeDamage(Random.Range(PlayerStat.maxHP / 10, PlayerStat.maxHP / 3));
            gameObject.SetActive(false);
        }
    }
}
