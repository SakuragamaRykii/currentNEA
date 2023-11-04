using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLevelSpawner : MonoBehaviour
{
    public int worldLevel, oldLevel;
    public int minEnemies = 3, maxEnemies = 7,
        minInte = 3, maxInte = 10,
        minWall = 3, maxWall = 12;
    public static bool hasReset = true;

    // Start is called before the first frame update
    void Start()
    {
        worldLevel = PlayerStat.level / 10 + 1;

        if (hasReset)
        {
            worldLevel = 1;
            minEnemies = 3; maxEnemies = 7;
            minInte = 3; maxInte = 10;
            minWall = 12; maxWall = 12;
        }

        hasReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
