
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
public class RTManager : MonoBehaviour
{
    public GameObject enemy, wall, player;
    public static int amount, wallAmount; //amount of enemies on the scene
    void Awake()
    {
        amount = Random.Range(1, 6);
        wallAmount = Random.Range(10, 16);
        for (int i = 0; i < amount; i++)
        {
            float x = Random.Range(-18.4f, 18.5f);
            float y = Random.Range(-18.4f, 18.5f);
            while (new Vector2(x, y) == (Vector2)player.transform.position)
            {
                x = Random.Range(-18.4f, 18.5f);
                y = Random.Range(-18.4f, 18.5f);
            }
            Instantiate(enemy, new Vector3(x, y,  - 5), Quaternion.identity);
        }
        for(int i = 0; i < wallAmount; i++)
        {
            float x = Random.Range(-18.4f, 18.5f);
            float y = Random.Range(-18.4f, 18.5f);
            while(new Vector2(x, y) == (Vector2)player.transform.position)
            {
                 x = Random.Range(-18.4f, 18.5f);
                 y = Random.Range(-18.4f, 18.5f);
            }
            Instantiate(wall, new Vector3(x, y, -5), Quaternion.identity);
            
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) SceneManager.LoadScene("FieldScene");
        if (amount <= 0)
        {
           // EnemyStat.ended = true;
            SceneManager.LoadScene("FieldScene");
        }

    }
}