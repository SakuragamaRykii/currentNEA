using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public Rect area { get; private set; } //area used to query the objects within it
    public Vector2 centre { get; private set; } //centre position of the point
    public float h { get; private set; } //current square to target
    public float g { get; private set; } //start to the current square
    public float f { get; private set; } //total
    public Point previous;
    public Point[] surrPoints { get; private set; } //surrounding points in four direcitons
    public GameObject target;

    public Point(Vector2 c)
    {
        centre = c;
        h = -1; g = -1; f = -1;
        area = new Rect(c.x - 0.5f, c.y - 0.5f, 1, 1);
    }

    public Point(Vector2 c, GameObject t)
    {
        centre = c;
        target = t;
        g = 1; 
        h = Vector2.Distance(centre, target.transform.position);
        f = h + g;
        area = new Rect(c.x - 0.5f, c.y - 0.5f, 1, 1);
    }

    public Point(Vector2 c, GameObject t, Point prev)
    {
        centre = c;
        previous = prev;
        target = t;
        g = previous.g + 1;
        h = Vector2.Distance(centre, target.transform.position);
        f = h + g;
        area = new Rect(c.x - 0.5f, c.y - 0.5f, 1, 1);
    }

    public GameObject scan()//GameObject target = null
    {
        foreach (Entity e in GameObject.FindObjectsOfType<Entity>())
        {
            if (area.Overlaps(e.hitbox))
            {
               // Debug.Log(e.gameObject);
                return (e.gameObject);
            }

        }
        return null;
    }

    public void InitSurr()
    {
        surrPoints = new Point[4];
        surrPoints[0] = new Point(new Vector2(centre.x, centre.y + 1), target, this);//up
        surrPoints[1] = new Point(new Vector2(centre.x + 1, centre.y), target, this);//right
        surrPoints[2] = new Point(new Vector2(centre.x, centre.y - 1), target, this);//down
        surrPoints[3] = new Point(new Vector2(centre.x - 1, centre.y), target, this);//left
    }




    public override string ToString()
    {
        return (centre.ToString() + ", f: " + f);
    }
}
