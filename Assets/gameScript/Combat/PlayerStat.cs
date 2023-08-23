using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private int level = 1;
    public static int attack = 5,
        defence = 2,
        speed = 10,
        currentHP, maxHP = 20;
    void death()
    {
        if (currentHP <= 0) gameObject.SetActive(false);

    }
    private void Awake()
    {
        if (level > 1)
        {
            attack *= (int)Mathf.Pow(1.05f, level);
            defence *= (int)Mathf.Pow(1.05f, level);
            speed *= (int)Mathf.Pow(1.05f, level);
            maxHP *= (int)Mathf.Pow(1.05f, level);

        }
        currentHP = maxHP;

    }

    public void LevelUp()
    {
        level++;
        attack = (int)(attack * 1.05f);
        defence = (int)(attack * 1.05f);
        speed = (int)(attack * 1.05f);
        maxHP = (int)(attack * 1.05f);

        currentHP = maxHP;
    }
}