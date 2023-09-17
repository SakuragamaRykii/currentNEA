using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rtManager : MonoBehaviour
{
    public static QuadTree qt;
    public GameObject rtField;
    public static rtManager only;
    //[SerializeField] private GameObject player, enemyTemp, wallTemp; //, utilTemp
    public int minEnemies = 3, maxEnemies = 7,
        minInte = 3, maxInte = 10,
        minWall = 3, maxWall = 12;
    private float x, y;
    // Start is called before the first frame update
    void Awake()
    {
        if (only == null) only = this; else Destroy(gameObject);
        Instantiate(rtField);
        x = rtField.transform.position.x - rtField.transform.localScale.x / 2;
        y = rtField.transform.position.y - rtField.transform.localScale.y / 2;
        qt = new QuadTree(new Rect(x, y, rtField.transform.localScale.x, rtField.transform.localScale.y));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        qt = new QuadTree(new Rect(x, y, rtField.transform.localScale.x, rtField.transform.localScale.y));

        foreach (Entity e in GameObject.FindObjectsOfType<Entity>())
        {
            qt.Insert(e.gameObject);
        }

    }
}
