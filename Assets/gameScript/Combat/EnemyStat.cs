using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IKillable
{
    public int
        maxHP,
        currentHP,
        attack,
        defence,
        speed,
        level;
    private void Awake()
    {
        level = Random.Range(PlayerStat.level, PlayerStat.level + 5);

        maxHP = (int)(Random.Range(5, 11) * Mathf.Pow(1.05f, level-1));
        currentHP = maxHP;

        attack = (int)(Random.Range(3, 8) * Mathf.Pow(1.05f, level-1));

        defence = (int)(Random.Range(1, 6) * Mathf.Pow(1.05f, level-1));

        speed = (int)(Random.Range(1, 6) * Mathf.Pow(1.05f, level-1));

    }

    public void Die()
    {
        if (IsDead())
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
    }
    public bool IsDead() { return currentHP <= 0; }
 

}
