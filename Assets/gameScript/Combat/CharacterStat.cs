using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [SerializeField] private int level = 1;
    public int attack, defence, baseSpeed, currentSpeed, currentHP, maxHP;
    void death()
    {
        gameObject.SetActive(false);

    }
    private void Awake()
    {
        if(level > 1)
        {
            attack *= (int)Mathf.Pow(1.05f, level);
            defence *= (int)Mathf.Pow(1.05f, level);
            baseSpeed *= (int)Mathf.Pow(1.05f, level);
            maxHP *= (int)Mathf.Pow(1.05f, level);

            currentSpeed = baseSpeed;
            currentHP = maxHP;
        }
       
    }

    public void LevelUp()
    {
        level++;
        attack = (int)(attack*1.05f);
        defence = (int)(attack * 1.05f);
        baseSpeed = (int)(attack * 1.05f);
        maxHP = (int)(attack * 1.05f);

        currentHP = maxHP;
    }

}
