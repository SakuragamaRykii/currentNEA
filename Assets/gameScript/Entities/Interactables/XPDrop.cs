using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPDrop : Entity, IRandomDrop
{
    public string Pick(string[] pool)
    {
        return "";
    }
    private void Start()
    {
        setupHitbox();
    }
    private void Update()
    {
        DynList<GameObject> cc = DynList<GameObject>.ToDList(CheckCollision());
        if (cc.HasTag("Player"))
        {
            PlayerStat.currentEXP += PlayerStat.expToNextLevel / 4;
            gameObject.SetActive(false);
        }
    }
}
