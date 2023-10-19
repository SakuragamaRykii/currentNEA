using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerStat 
{
    public static Vector2 currentFieldPos = new Vector2(0, 0);
    public static float
        level = 1,
        attack = 5,
        defence = 2,
        speed = 10,
        currentHP = 20,
        maxHP = 20;

    public static void SetUp()
    {
        if (level > 1)
        {
            attack *= Mathf.Pow(1.05f, level);
            defence *= Mathf.Pow(1.05f, level);
            speed *= Mathf.Pow(1.05f, level);
            maxHP *= Mathf.Pow(1.05f, level);

        }
        

    }

    public static void LevelUp()
    {
        level++;
        attack *= 1.05f;
        defence *= 1.05f;
        speed *= 1.05f;
        maxHP *= 1.05f;

        currentHP = maxHP;
    }
    public static bool IsDead() { return currentHP <= 0; }
    public static void TakeDamage(float amount)
    {
        currentHP -= amount;
        Die();
    }

    public static void Die()
    {
        if (IsDead())
        {
            Debug.Log("you are dead");
            reset();
            SceneManager.LoadScene("StartMenu");
        }
    }
    public static void reset()
    {
       // FieldManager.hasReset = true;
        level = 1;
        attack = 5;
        defence = 2;
        speed = 10;
        maxHP = 20;
        currentHP = maxHP;
    }
}