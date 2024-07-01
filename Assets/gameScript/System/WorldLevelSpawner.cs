using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class WorldLevelSpawner : MonoBehaviour, IDataPersistence
{

    public static int worldLevel = 1, oldLevel = 0;
    public bool enableRandomSpawn;
    // below are to be serialized on editor
    public GameObject wallObj, enemyObj;
    public GameObject playerObj;
    public GameObject[] inteObjs;

    //public static string enemyMarked;

    public Vector3 playerPos;
    public GameObject playerCam;

    private void Start()
    {
        if(enableRandomSpawn) StartCoroutine(RandomEnemySpawn());
    }

    private IEnumerator RandomEnemySpawn()
    {
        int min = 0, max = 101;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(player != null && player.GetComponent<Rigidbody2D>().velocity.magnitude != 0) 
            {
                int rng = Random.Range(min, max);
                Debug.Log(max);
                if (rng < 5)
                { //therefore the initial chance of encounter is 5%
                    GetComponent<DataPersistenceManager>().SaveGame();
                    SceneManager.LoadScene("TBAndSelectionScene");

                }
                else max -= 2; //increases chance of encounter each time
            }

            
        }
    }
    public void LoadData(GameData data)
    {

        worldLevel = data.worldLevel;
        oldLevel = data.oldLevel;
        if(worldLevel > oldLevel)
        {
            Debug.Log("levelling up world level");
            NewRandomField();
            oldLevel = worldLevel;
            return;
        }


        //spawn enemies
        int amountDown = 0;
        foreach (KeyValuePair<Vector2, bool> pair in data.enemyFieldData)
        {
            if (pair.Value) Spawn(pair.Key, enemyObj);
            else amountDown++;
        }
        if(amountDown >= data.enemyFieldData.Count)
        {
            worldLevel++;
            Debug.Log("new level");
            NewRandomField();
            return;
        }

        //spawn player 
        playerPos = data.fieldPlayerPosition;
        Instantiate(playerObj, playerPos, Quaternion.identity);
        playerCam.GetComponent<cameraMove>().SetUp();

        //spawn walls
        foreach (KeyValuePair<Vector2, bool> pair in data.wallFieldData)
        {
            if(pair.Value) Spawn(pair.Key, wallObj);
        }

        // spawn interactables
        if(data.specificInteractables != null)
        {
            for(int i = 0; i < data.specificInteractables.Count; i++)
            {
                  if(data.interactablesFieldData.ElementAt(i).Value)
                  {
                      if (data.specificInteractables.ElementAt(i).Contains("xpDummy")) Spawn(data.interactablesFieldData.ElementAt(i).Key, inteObjs[0]);
                      if (data.specificInteractables.ElementAt(i).Contains("WeaponDummy")) Spawn(data.interactablesFieldData.ElementAt(i).Key, inteObjs[1]);
                      if (data.specificInteractables.ElementAt(i).Contains("ConsumableDummy")) Spawn(data.interactablesFieldData.ElementAt(i).Key, inteObjs[2]);
                      if (data.specificInteractables.ElementAt(i).Contains("BuffDummy")) Spawn(data.interactablesFieldData.ElementAt(i).Key, inteObjs[3]);
                      if (data.specificInteractables.ElementAt(i).Contains("BadDummy")) Spawn(data.interactablesFieldData.ElementAt(i).Key, inteObjs[4]);

                  }
                //if (data.interactablesFieldData.ElementAt(i).Value) Spawn(data.interactablesFieldData.ElementAt(i).Key, inteObjs[0]);
            }
        }

    }
    public void SaveData(ref GameData data)
    {
        if (data != null) data = new GameData();
        IEnumerable enemies = FindObjectsByType<area>(FindObjectsInactive.Include, FindObjectsSortMode.None); 
        foreach (area a in enemies) data.enemyFieldData.Add(a.transform.position, a.gameObject.activeSelf);

        IEnumerable interactables = FindObjectsByType<Entity>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IRandomDrop>();
        foreach (Entity a in interactables)
        {
            try
            {
                data.interactablesFieldData.Add(a.transform.position, a.gameObject.activeSelf);
            }
            catch (System.ArgumentException e)
            {
                continue;
            }
            data.specificInteractables.Add(a.gameObject.name);
        }
        IEnumerable walls = FindObjectsByType<Wall>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Wall a in walls) data.wallFieldData.Add(a.transform.position, a.gameObject.activeSelf);

        data.fieldPlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        data.worldLevel = worldLevel;
        data.oldLevel = oldLevel;
    }

    public void NewRandomField()
    {
        playerPos = new Vector3(0, 0, -5);
        Instantiate(playerObj, playerPos, Quaternion.identity);
        playerCam.GetComponent<cameraMove>().SetUp();
        //initialize minimum and maximum relative to world level;
        int minEnemies = 2 + worldLevel,
            maxEnemies = 5 + worldLevel,
            minInte = 2 + worldLevel,
            maxInte = 6 + worldLevel,
            minWall = 12 + worldLevel,
            maxWall = 15 + worldLevel;
        //later randomly get the amount to spawn for this level
        int ens = Random.Range(minEnemies, maxEnemies + worldLevel),
        inters = Random.Range(minInte, maxInte + worldLevel),
        walls = Random.Range(minWall, maxWall + worldLevel);
        //
        Rect[] alreadyVisited = new Rect[ens + inters + walls];

        Rect preventEnemy = new Rect(Vector2.zero - Vector2.one * 5, Vector2.one * 10); //10*10 square to prevent enemies from spawning near and immediately chase the player.
        Rect preventOther = new Rect(Vector2.zero - Vector2.one * 1.5f, Vector2.one * 3); // prevents other objects to overlap the player.

        //values to prevent the entities from spwaning outside the field
        float min = -18, max = 18.1f;

        //spawn enemies
        for(int i = 0; i < ens; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(min, max), Random.Range(min, max));
            while(CheckAlready(randomPos, alreadyVisited) || preventEnemy.Contains(randomPos)) randomPos = new Vector2(Random.Range(min, max), Random.Range(min, max));

            Spawn(randomPos, enemyObj);
            alreadyVisited[i] = new Rect(randomPos - Vector2.one / 2, Vector2.one);
            
        }
        //spawn interactables
        for(int j = ens; j < ens + inters; j++)
        {
            Vector2 randomPos = new Vector2(Random.Range(min, max), Random.Range(min, max));
            while (CheckAlready(randomPos, alreadyVisited) || preventOther.Contains(randomPos)) randomPos = new Vector2(Random.Range(min, max), Random.Range(min, max));
            GameObject randomObj = inteObjs[Random.Range(0, inteObjs.Length)];
            Spawn(randomPos, randomObj);
            alreadyVisited[j] = new Rect(randomPos - Vector2.one / 2, Vector2.one);

        }
        //spawn walls
        for (int k = ens + inters; k < ens + inters + walls; k++)
        {
            Vector2 randomPos = new Vector2(Random.Range(min, max), Random.Range(min, max));
            while (CheckAlready(randomPos, alreadyVisited)) randomPos = new Vector2(Random.Range(min, max), Random.Range(min, max));

            Spawn(randomPos, wallObj);
            alreadyVisited[k] = new Rect(randomPos - Vector2.one / 2, Vector2.one);

        }





    }

    private bool CheckAlready(Vector2 pos, Rect[] list)
    {
        foreach (Rect r in list)
        {
            Rect checkArea = new Rect(pos - Vector2.one / 2, Vector2.one); //rect area to check
            if (r.Overlaps(checkArea)) return true;
        }

        return false;
    }

    public void Spawn(Vector2 position, GameObject entity)
    {
        Instantiate(entity, (Vector3)position - Vector3.forward, Quaternion.identity);
    }

}
