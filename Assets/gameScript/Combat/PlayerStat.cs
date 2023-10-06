using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour, IKillable
{
    
    public static float
        level = 1,
        attack = 5,
        defence = 2,
        speed = 10,
        currentHP,
        maxHP = 20;

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
    public bool IsDead() { return currentHP <= 0; }
    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        Die();
    }

    public void Die()
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
        FieldManager.hasReset = true;
        level = 1;
        attack = 5;
        defence = 2;
        speed = 10;
        maxHP = 20;
        currentHP = maxHP;
    }
}