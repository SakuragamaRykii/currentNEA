using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableField : Entity
{

    Item[] options;
    private void Start()
    {
        setupHitbox();
    }

    private void Update()
    {
        GameObject[] cc = CheckCollision();
        if(hasTag(cc, "Player"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
