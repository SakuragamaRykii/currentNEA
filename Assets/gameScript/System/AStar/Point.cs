using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public Rect area { get; private set; }
    public Vector2 c { get; private set; } //centre position of the point

    public float g, h, f;
    public Point(Rect r)
    {
        area = r;
        c = new Vector2(r.x + r.width / 2, r.y + r.height / 2);
        g = -1; h = -1;
        f = -1;

    }
    public Point(Vector2 centre)
    {
        area = new Rect(centre.x - 0.5f, centre.y - 0.5f, 1, 1);
        c = centre;
        g = -1; h = -1; f = -1;
    }
    public Point(Vector2 centre, Vector2 start, Vector2 target)
    {
        area = new Rect(centre.x - 0.5f, centre.y - 0.5f, 1, 1);
        c = centre;
        g = Vector2.Distance(start, c);
        h = Vector2.Distance(target, c);//unity's Distance method uses euclidian distance.
        f = g + h;
    }

    public Point(Rect r, Vector2 start, Vector2 target)
    {
        area = r;
        c = new Vector2(r.x + r.width / 2, r.y + r.height / 2);
        g = Vector2.Distance(start, c);
        h = Vector2.Distance(target, c);//unity's Distance method uses euclidian distance.
        f = g + h;
       // Debug.Log(c + " point = " + f);


    }

    public Point up, right, down, left;

    public GameObject scan()//GameObject target = null
    {
        foreach (Entity e in GameObject.FindObjectsOfType<Entity>())
        {
            if (area.Overlaps(e.hitbox))
            {
                return (e.gameObject);
            }

        }
        return null;
    }

    public override string ToString()
    {
        return c.ToString();
    }
}
