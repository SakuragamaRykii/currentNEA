using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    private Block area;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        area = new Block(player.transform.position, 19.2f, 12);

    }

    // Update is called once per frame
    void Update()
    {
        if (!area.contains(player))
        {
            area.centrePos = player.transform.position;
            transform.position = player.transform.position;
        }
    }
}
