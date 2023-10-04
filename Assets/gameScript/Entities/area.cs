using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class area : Entity
{
    [SerializeField] private GameObject enemy;
    Entity enemEn;
    private GameObject player;
    void Start()
    {
        setupHitbox();
        enemy = Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, -12), Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if(hitbox.Contains(player.transform.position)) enemy.GetComponent<FieldEnemy>().Move();


        if (!hitbox.Contains(enemy.transform.position))
        {
            enemy.transform.position = transform.position;
        }
    }
}
