using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class area : Entity
{
    [SerializeField] private GameObject enemy;
    Entity enemEn;
    private GameObject player;
    private float count;

    void Start()
    {
        setupHitbox();
        enemy = Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player");
        count = 0;
    }


    void Update()
    {
        if (hitbox.Contains(player.transform.position))
        {
            count -= Time.deltaTime;
            if(count <= 0) { enemy.GetComponent<PathFinder>().Find(); count = 0.5f; }
            enemy.GetComponent<FieldEnemy>().Move();
        }


        if (!hitbox.Contains(enemy.transform.position))
        {
            enemy.transform.position = transform.position;
        }
    }

}
