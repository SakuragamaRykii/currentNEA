using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class WorldLevelSpawner : MonoBehaviour
{
    public int worldLevel, oldLevel;
    public int minEnemies = 3, maxEnemies = 7,
        minInte = 3, maxInte = 10,
        minWall = 3, maxWall = 12;
    public GameObject[] total;
    Rect[] alreadyVisited;
    public GameObject[] enemies;
    public static bool hasReset = true;

    // below are to be serialized on editor
    public GameObject wallObj, enemyObj;
    public GameObject playerObj;
    public GameObject[] inteObjs;

    private Rect nonoArea;//prevents enemies from spawning really close to the player
    public static Vector3 playerPos;
    public GameObject playerCam;
    private bool CheckAlready(Vector2 pos)
    {
        foreach(Rect r in alreadyVisited)
        {
            Rect checkArea = new Rect(pos - Vector2.one/2, Vector2.one);
            if (r.Overlaps(checkArea)) return true;
        }
        
        return false;
    }
    void Start()
    {
        if (hasReset)
        {
            playerPos = Vector3.zero - Vector3.forward;

            worldLevel = 1;
            minEnemies = 2; maxEnemies = 5;
            minInte = 2; maxInte = 6;
            minWall = 12; maxWall = 15;
            hasReset = false;
        }
        

        nonoArea = new Rect((Vector2)playerPos - Vector2.one*5, Vector2.one*10); 

        Instantiate(playerObj, playerPos, Quaternion.identity);
        playerCam.GetComponent<cameraMove>().SetUp();

        Spawn();
    }

    public void Spawn()
    {
        int ens = Random.Range(minEnemies, maxEnemies + worldLevel),
            inters = Random.Range(minEnemies, maxEnemies + worldLevel),
            walls = Random.Range(minEnemies, maxEnemies + worldLevel);

        int totalAmount = ens + inters + walls;
        total = new GameObject[totalAmount];
        alreadyVisited = new Rect[totalAmount];
        enemies = new GameObject[ens];

        float min = -18, max = 18.1f;
        for (int i = 0; i < ens; i++) //spawn enemies ------------------------------------------------------------------------------------------------------
        {
            Vector2 pos = new Vector2(Random.Range(min, max), Random.Range(min, max));
            while (CheckAlready(pos) || nonoArea.Contains(pos))
            {
                pos.x = Random.Range(min, max);
                pos.y = Random.Range(min, max);
            }
            
            Instantiate(enemyObj, (Vector3)pos - Vector3.forward, Quaternion.identity);
            alreadyVisited[i] = new Rect(pos - Vector2.one/2, Vector2.one);
        }
        Rect notOnPlayer = new Rect((Vector2)playerPos - Vector2.one * 1.5f, Vector2.one * 3); //to prevent other objects from going on the player or very near it.
        for (int j = ens; j < ens+inters; j++) //spawn interactables ------------------------------------------------------------------------------------
        {
            GameObject rand = inteObjs[Random.Range(0, inteObjs.Length)]; //picks a random interactable
            Vector2 pos = new Vector2(Random.Range(min, max), Random.Range(min, max));
            while (CheckAlready(pos) || notOnPlayer.Contains(pos))
            {
                pos.x = Random.Range(min, max);
                pos.y = Random.Range(min, max);
            }

            Instantiate(rand, (Vector3)pos - Vector3.forward, Quaternion.identity);
            alreadyVisited[j] = new Rect(pos - Vector2.one / 2, Vector2.one);
        }
        for(int k = ens + inters; k < totalAmount; k++) // spawn walls ------------------------------------------------------------------------------
        {
            Vector2 pos = new Vector2(Random.Range(min, max), Random.Range(min, max)); 
            while (CheckAlready(pos) || notOnPlayer.Contains(pos))
            {
                pos.x = Random.Range(min, max);
                pos.y = Random.Range(min, max);
            }

            Instantiate(wallObj, (Vector3)pos - Vector3.forward, Quaternion.identity);
            alreadyVisited[k] = new Rect(pos - Vector2.one / 2, Vector2.one);
        }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
