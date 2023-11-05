using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IKillable
{
    public int level;
    public float
        maxHP,
        currentHP,
        attack,
        defence,
        speed;
    private void Awake()
    {
        level = Random.Range(PlayerStat.level, PlayerStat.level + 5);

        maxHP = (int)(Random.Range(5, 11) * Mathf.Pow(1.05f, level-1));
        //maxHP = 100;

        currentHP = maxHP;

        attack = (int)(Random.Range(1, 5) * Mathf.Pow(1.05f, level-1));

        defence = (int)(Random.Range(1, 6) * Mathf.Pow(1.05f, level-1));

        speed = (int)(Random.Range(1, 6) * Mathf.Pow(1.05f, level-1));

    }

    public void Die()
    {
        PlayerStat.score += 10 * 2 * level;
        gameObject.SetActive(false);
        Destroy(gameObject);

    }
    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        Debug.Log("Current hp : " + currentHP);
    }
    public bool IsDead() { return currentHP <= 0; }
 

}
