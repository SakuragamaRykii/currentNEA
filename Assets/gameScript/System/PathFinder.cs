using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // Start is called before the first frame update
   // float x, y;
    [SerializeField] private Entity targetObj;//probably the player
    private point startPoint, targetPoint;
    // Update is called once per frame
    DynList<point> visited, toVisit;  //closed and open list       


    void Awake()
    {
    }


    void FixedUpdate()
    {
        visited = new DynList<point>();
        toVisit = new DynList<point>();
        startPoint = new point(GetComponent<Entity>().hitbox);
        targetPoint = new point(targetObj.hitbox);
        FindPath();
    }

    [SerializeField] private GameObject point;
    public void FindPath()
    {
        // point targetpoint = new point(targetObj.hitbox);
        point centrePoint = startPoint; //
        int ite = 0;
        while (!targetObj.hitbox.Contains(centrePoint.c))
        {
            visited.add(centrePoint);

            Up(centrePoint);
            Right(centrePoint);
            Down(centrePoint);
            Left(centrePoint);

            point lowest = null;
           // Debug.Log("visited = " + visited);
           // Debug.Log("tovisit = " + toVisit);
            foreach (point p in toVisit)
            {
                if(p == null) continue;
                if (lowest == null) lowest = p;
                else if (p.f < lowest.f) lowest = p; //P HAS A RANDOM CHANCE OF BEING NULL FOR SOME REASON.

            }
            centrePoint = lowest;
            Instantiate(point, new Vector3(centrePoint.c.x, centrePoint.c.y, - 10), Quaternion.identity);



            visited.add(lowest);// toVisit.remove(lowest);
            Debug.Log(++ite);//debug for the amount of iterations taken to find the path.
        }



    }

    private bool AlVisited(point p) //check if already visited
    {
        foreach(point vis in visited)
        {
            Vector2 centre = vis.c;
            Vector2 checkP = p.c;
            if (centre.Equals(checkP))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsAtTarget(point p)
    {
        // return (p.c.Equals(targetPoint.c));
        if (p.c.Equals(targetPoint.c))
        {
            Debug.Log("FOUND");
            return true;
        }
        else return false;
    }
    
    private void Up(point centredAt)
    {
        //Debug.Log(startPoint.c);
        point up = new point(new Rect(centredAt.c.x - centredAt.area.width/2,
                                      centredAt.c.y + centredAt.area.height/2,
                                      centredAt.area.width, 
                                      centredAt.area.height),
                                      startPoint, targetPoint);

        if (AlVisited(up))
        {
           // Debug.Log("up : already visited");
            return;
        }
 
        GameObject sc = up.scan(); //targetObj.gameObject <- target
        if (sc != null)
        {
            if (sc.tag == "Wall")
            {
              //  Debug.Log("up : Wall");
            }
            //else if (sc.tag == targetObj.tag)
            //{
            //    Debug.Log("left : Target");
            //    found = true;

            //}
        }
        else
        {
          //  Debug.Log("up: none");
            toVisit.add(up);
        }
    }
    private void Right(point centredAt)
    {
        point right = new point(new Rect(centredAt.c.x + centredAt.area.width / 2,
                                         centredAt.c.y - centredAt.area.height / 2,
                                         centredAt.area.width,
                                         centredAt.area.height),
                                         startPoint, targetPoint);
        if (AlVisited(right))
        {
            Debug.Log("right : already visited");
            return;
        }

        GameObject sc = right.scan();
        if (sc != null)
        {
            if (sc.tag == "Wall")
            {
               // Debug.Log("right : Wall");
            }
            //else if (sc.tag == targetObj.tag)
            //{
            //    Debug.Log("left : Target");
            //    found = true;

            //}
        }
        else
        {
           // Debug.Log("right : none");
            toVisit.add(right);
        }
    }
    private void Down(point centredAt)
    {

        point down = new point(new Rect(centredAt.c.x - centredAt.area.width / 2,
                                        centredAt.c.y - centredAt.area.height,
                                        centredAt.area.width,
                                        centredAt.area.height),
                                        startPoint, targetPoint);
        if (AlVisited(down))
        {
            Debug.Log("down : already visited");
            return;
        }
        GameObject sc = down.scan();
        if (sc != null)
        {
            if (sc.tag == "Wall")
            {
              //  Debug.Log("down : Wall");
            }
            //else if (sc.tag == targetObj.tag)
            //{
            //    Debug.Log("left : Target");
            //    found = true;

            //}
        }
        else
        {
           // Debug.Log("down: none");
            toVisit.add(down);
        }
    }
    private void Left(point centredAt)
    {

        point left = new point(new Rect(centredAt.c.x - centredAt.area.width,
                                        centredAt.c.y - centredAt.area.height / 2,
                                        centredAt.area.width,
                                        centredAt.area.height),
                                        startPoint, targetPoint);

        if (AlVisited(left))
        {
          //  Debug.Log("left : already visited");
            return;
        }

        GameObject sc = left.scan();
        if (sc != null)
        {
            if (sc.tag == "Wall")
            {
              //  Debug.Log("left : Wall");
            }
            //else if (sc.tag == targetObj.tag)
            //{
            //    Debug.Log("left : Target");
            //    found = true;

            //}
        }
        else
        {
           // Debug.Log("left: none");
            toVisit.add(left);
        }
    }

 


        

}
class point
{
    public Rect area { get; private set; }
    public Vector2 c {  get ; private set; } //centre position of the point

    public float g, h, f;
    public point(Rect r)
    {
        area = r;
        c = new Vector2 (r.x+r.width/2, r.y+r.height/2);
        g = -1; h = -1;
        f = -1;
        //Debug.Log(c);
        
    }
    public point(Rect r, point start, point target)
    {
        area = r;
        c = new Vector2(r.x + r.width / 2, r.y + r.height / 2);
        g = Vector2.Distance(start.c, c);
        h = Vector2.Distance(target.c, c);
        f = g + h;

    }

    public GameObject scan()//GameObject target = null
    {
        foreach(Entity e in GameObject.FindObjectsOfType<Entity>())
        {
            if (area.Overlaps(e.hitbox))
            {
              //  Debug.Log("has" + e.gameObject.name);
                if (e.gameObject.tag.Equals("Wall"))
                {
                    return e.gameObject;
                }
                //if (target != null && e.gameObject.tag.Equals(target.tag))
                //{
                //    return e.gameObject;
                //}

            }

        }
        return null;
    }
}


