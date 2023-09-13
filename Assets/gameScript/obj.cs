using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj : MonoBehaviour
{
    public Rect hitbox;
    float x, y;
    private void Start()
    {

        x = transform.position.x - transform.localScale.x / 2;
        y = transform.position.y - transform.localScale.y / 2;
        hitbox = new Rect(x, y, transform.localScale.x, transform.localScale.y);
        Man.qt.insert(gameObject);
        
        StartCoroutine(ManageIns());
        
    }

    public IEnumerator ManageIns()
    {
        
        while(true)
        {
            Vector2 oldPos = transform.position;
            yield return new WaitForSeconds(1);
            Man.qt.insert(gameObject);
            if (transform.position.x != oldPos.x || transform.position.y != oldPos.y)
            {
                //Man.qt.insert(gameObject);
            }
        }

    }

    public void Update() 
    {
        x = transform.position.x - transform.localScale.x / 2;
        y = transform.position.y - transform.localScale.y / 2;
        hitbox.position = new Vector2(x, y);
        DynList<GameObject> others = Man.qt.Query(gameObject);
        Debug.Log(others);

        foreach(GameObject other in others)
        {
            if(other != gameObject && other.GetComponent<obj>() != null && other.GetComponent<obj>().hitbox.Overlaps(hitbox))
            {
                Debug.Log(gameObject + "HIT" + other.gameObject);
            }
        }
    }
}
