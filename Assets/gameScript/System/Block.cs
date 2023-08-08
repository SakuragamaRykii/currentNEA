using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block //: MonoBehaviour
{
    public float scaleX { get; private set; }
    public float scaleY { get; private set; }

    public Vector2 position;

    //private float left, right, top, bot;


    public Block(Vector2 pos, float w, float h)
    {
        position = pos; scaleX = w; scaleY = h;

        /*GameObject b = (GameObject)Resources.Load("prefabs/BLOCK");
        b.transform.localScale = new Vector2(w, h);
        b.transform.position = pos;
        Instantiate(b);*/

            
        
    }

    public bool Contains(GameObject obj)
    {
        float x = obj.transform.position.x;
        float y = obj.transform.position.y;
       // Debug.Log(x); Debug.Log(y);

        return (
            x >= position.x - scaleX / 2&&
            y >= position.y - scaleY / 2 &&
            x <= position.x + scaleX / 2 &&
            y <= position.y + scaleY / 2

            );
    }

    public bool Intersects(Block other)
    {
        float ax = this.position.x;
        float ay = this.position.y;
        float bx = other.position.x;
        float by = other.position.y;

        return !(bx - other.scaleX > ax + this.scaleX /2||
                  bx + other.scaleX < ax - this.scaleX /2||
                  by - other.scaleY > ay + this.scaleY /2||
                  by + other.scaleY < ay - this.scaleY /2);    
    }






}
