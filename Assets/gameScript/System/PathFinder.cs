using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // Start is called before the first frame update
    float x, y;
    [SerializeField] private GameObject startObj;
    [SerializeField] private GameObject targetObj;//probably the player
    public Vector2 visted, toVisit; //known as open and closed lists

    void Awake()
    {
        x = startObj.transform.position.x - startObj.transform.localScale.x/2;
        y = startObj.transform.position.y - startObj.transform.localScale.y / 2;
        Rect startPoint = new Rect(x, y, startObj.transform.localScale.x, startObj.transform.localScale.y);




    }

    // Update is called once per frame
    void Update()
    {
        FindPath();
    }

    public void FindPath()
    {
        Vector2 north, south, east, west, northEast, northWest, southEast, southWest;

    }

}
