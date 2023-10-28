using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IKillable
{
    public float
        maxHP,
        currentHP,
        attack,
        speed,
        level;

    public float xpWorth;
    private void Awake()
    {
        level = Random.Range(PlayerStat.level, PlayerStat.level + 5);

        maxHP = (Random.Range(5, 11) * Mathf.Pow(1.05f, level));
        //maxHP = 100;

        currentHP = maxHP;

        attack = (Random.Range(1, 5) * Mathf.Pow(1.05f, level-1));

        speed = (Random.Range(1, 6) * Mathf.Pow(1.05f, level -1));

        xpWorth = Random.Range(5, 10) * level; 

    }

    public void Die()
    {
        PlayerStat.currentEXP += xpWorth;
        PlayerStat.score += 10 * (int)Mathf.Pow(1.12f, level);
        gameObject.SetActive(false);
        Destroy(gameObject);
        
    }
    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        //  Debug.Log("Current hp : " + currentHP);
    }
    public bool IsDead() { return currentHP <= 0; }
 

}
