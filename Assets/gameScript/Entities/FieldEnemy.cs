using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldEnemy : Entity
{
    public GameObject targetObj;
    PathFinder pf;
    public Rigidbody2D rb;
    public GameObject within; //is initialized by the area. refers to the area which spawned this entity.

    // Start is called before the first frame update
    void Awake()
    {
        setupHitbox();
       /* if (transform.position.Equals(WorldLevelSpawner.enemyMarked) && EnemyStat.ended)
        {
            Debug.Log("se");
            within.SetActive(false);
            gameObject.SetActive(false);
            return;
        }*/
        pf = GetComponent<PathFinder>();
        rb = GetComponent<Rigidbody2D>();
        pf.targetObj = GameObject.FindGameObjectWithTag("Player");

    }

    //move is called by the area script

    public void Move()
    { //THE START POINT OF THE PATH IS IN AS THE LAST ELEMENT OF FINALPATH
        
        hitbox.position = rb.position - hitbox.size/2;
        if (pf.finalPath == null) return;
        if (pf.index < 0) return;
            Point moveTo = pf.finalPath[pf.index];
        if (!moveTo.centre.Equals(transform.position))
        {
            rb.position = Vector2.MoveTowards(rb.position, moveTo.centre, 0.1f);
        }
        else pf.index--;
    }

    private void FixedUpdate()
    {
        if(HasTag(CheckCollision(), "Player"))
        {
            DataPersistenceManager dpm = GameObject.FindGameObjectWithTag("GameController").GetComponent<DataPersistenceManager>();
            within.SetActive(false);
            dpm.SaveGame();
            SceneManager.LoadScene("TBAndSelectionScene");
        }
    }
}
