using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class area : Entity
{
    [SerializeField] private GameObject enemy;
    void Start()
    {
        setupHitbox();
        Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, -12), Quaternion.identity);
    }


    void Update()
    {
        if (!hitbox.Contains(enemy.transform.position))
        {
            enemy.transform.position = transform.position;
        }
    }
}
