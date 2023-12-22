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
        if(HasTag(CheckCollision(), "Player"))
        {
            PlayerStat.currentEXP += PlayerStat.expToNextLevel / 4;
            gameObject.SetActive(false);
        }
    }
}
