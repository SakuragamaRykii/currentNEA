using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerStat 
{
    public static Vector2 currentFieldPos = new Vector2(0, 0);
    public static int level = 1, score = 0;

    public static float
        attack = 5,
        defence = 2,
        speed = 10,
        currentHP = 20,
        maxHP = 20;
    public static int currentEXP = 0, expToNextLevel = 25;
    public static Thread levelThread = new Thread(LevelUp);

    public static void LevelUp()
    {
        while (true){
            if (currentEXP >= expToNextLevel)
            {
                level++;
                attack *= 1.05f;
                defence *= 1.05f;
                speed *= 1.05f;
                maxHP *= 1.05f;

                currentHP = maxHP;

                currentEXP = 0;
                expToNextLevel += 25;
            }
        }

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
        score = 0;
        currentEXP = 0;
        expToNextLevel = 25;

        WorldLevelSpawner.hasReset = true;

    }
}