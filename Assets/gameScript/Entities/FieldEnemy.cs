using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEnemy : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        setupHitbox();

    }

    // Update is called once per frame
    void Update()
    {
        checkCollision();
    }
}
