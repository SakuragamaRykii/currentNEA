using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IKillable
{
    public int level;
    //public static bool ended = false;

    public float
        maxHP,
        currentHP,
        attack,
        defence,
        speed;
    public int xpWorth;
    private void Awake()
    {
       // ended = false;
        level = Random.Range(PlayerStat.level, PlayerStat.level + 5);
        maxHP = (Random.Range(5, 11) * Mathf.Pow(1.05f, level-1));
        //maxHP = 100;

        currentHP = maxHP;

        attack = (Random.Range(1, 5) * Mathf.Pow(1.05f, level-1));

        defence = (Random.Range(1, 6) * Mathf.Pow(1.05f, level-1));

        speed = (Random.Range(10, 16) * Mathf.Pow(1.05f, level-1));

        xpWorth = 5 * level;
    }

    public void Die()
    {
        PlayerStat.score += 10 * 2 * level;
        PlayerStat.currentEXP += xpWorth;
        gameObject.SetActive(false);
        Destroy(gameObject);

    }
    public void TakeDamage(float amount)
    {
        currentHP -= amount;
    }
    public bool IsDead() { return currentHP <= 0; }
 

}
