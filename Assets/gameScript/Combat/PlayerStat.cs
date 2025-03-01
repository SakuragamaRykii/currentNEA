using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerStat 
{
    public static Vector2 currentFieldPos = new Vector2(0, 0);
    public static int level = 1, score = 0;
    public static string fileToDelete;

    public static float
        attack = 5,
        defence = 2,
        speed = 10,
        currentHP = 20,
        maxHP = 20;
    public static float currentEXP = 0, expToNextLevel = 25;

    public static Thread levelThread = new Thread(LevelUp);

    public static void LevelUp()
    {
        while (true){
            if (currentEXP >= expToNextLevel)
            {
                level++;
                currentEXP = 0;
                currentHP = maxHP;
            }
            attack = 5 * Mathf.Pow(1.05f, level);
            defence = 2 * Mathf.Pow(1.05f, level);
            speed = 10 * Mathf.Pow(1.05f, level);
            maxHP = 20 * Mathf.Pow(1.05f, level);
            expToNextLevel = 25 * level;
            if (currentHP >= maxHP) currentHP = maxHP;
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
            reset();
            SceneManager.LoadScene("StartMenu");
        }
    }
    public static void reset()
    {
       // FieldManager.hasReset = true;
        level = 1;
        currentEXP = 0;
        expToNextLevel = 25;
        currentHP = maxHP;
        File.Delete(fileToDelete);
    }
}