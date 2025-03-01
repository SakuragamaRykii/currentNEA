using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBuff : Entity, IRandomDrop
{
    private string[] buffs;
    public string thisDrop;
    public string Pick(string[] pool)
    {
        int index = Random.Range(0, pool.Length);
        return pool[index];
    }
    void Start()
    {
        buffs = new string[] { "ATK", "DEF", "HP" };
        setupHitbox();
        thisDrop = Pick(buffs);
    }

    // Update is called once per frame
    void Update()
    {
        DynList<GameObject> cc = DynList<GameObject>.ToDList(CheckCollision());
        if (cc.HasTag("Player"))
        {
            switch (thisDrop)
            {
                case "ATK":
                    PlayerStat.attack *= 1.2f;
                    break;
                case "DEF":
                    PlayerStat.defence *= 1.2f;
                    break;
                case "HP":
                    PlayerStat.maxHP *= 1.1f;
                    break;

            }
            gameObject.SetActive(false);
        }
    }
}
