using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float width { get; private set; }
    public float height { get; private set; }

    public Vector2 centrePos; //{ get; private set; }

    public Block(Vector2 pos, float w, float h)
    {
        centrePos = pos;
        width = w;
        height = h;

    }

    public bool contains(GameObject obj)
    {
        float x = obj.transform.position.x;
        float y = obj.transform.position.y;

        if (x > centrePos.x - width / 2 &&
            y > centrePos.y - height / 2 &&
            x < centrePos.x + width / 2 &&
            y < centrePos.y + height / 2)
        {
            Debug.Log("true");
            return true;
        }


        else { Debug.Log("false"); return false; }

        /*return (
            x > centrePos.x - width / 2 &&
            y > centrePos.y - height / 2 &&
            x < centrePos.x + width / 2 &&
            y < centrePos.y + height / 2

            );*/
    }

    public bool intersects(Block other)
    {
        float ax = this.centrePos.x;
        float ay = this.centrePos.y;
        float bx = other.centrePos.x;
        float by = other.centrePos.y;

        return !(bx - other.width > ax + this.width ||
                 bx + other.width < ax - this.width ||
                 by - other.height > ay + this.height ||
                 by + other.height < ay - this.height);
    }






}
