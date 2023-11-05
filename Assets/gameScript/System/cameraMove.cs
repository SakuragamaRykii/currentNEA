using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    private Block area;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    public void SetUp()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        area = new Block(player.transform.position, 19.2f, 12);
        area.position = (Vector2)player.transform.position;
        transform.position = (Vector2)player.transform.position;
        StartCoroutine(checkLocation());
    }

    // Update is called once per frame
    private IEnumerator checkLocation()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!area.Contains(player))
            {
                area.position = (Vector2)player.transform.position;
                transform.position = (Vector2)player.transform.position;
            }
        }
    }
}
