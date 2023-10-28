using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class RTManager : MonoBehaviour
{
    public GameObject enemy;
    public static int amount;
    private void Start()
    {
        amount = Random.Range(1, 6);
        for(int i = 0; i < amount; i++)
        {
            float x = Random.Range(-18.4f, 18.5f);
            float y = Random.Range(-18.4f, 18.5f);

           Instantiate(enemy, new Vector3(x, y, i-5), Quaternion.identity);
        }
    }
    
    private void FixedUpdate()
    {
        if(amount <= 0) SceneManager.LoadScene("FieldScene");

    }
}
