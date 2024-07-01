using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PathFinder : MonoBehaviour
{

    [SerializeField] public GameObject targetObj;
    private Point startPoint;
    DynList<Point> open, closed;  //closed and open list       
    public Point[] finalPath;
    public int index;



    public void Find()
    {
        if (targetObj == null) targetObj = GameObject.FindGameObjectWithTag("Player");
        open = new DynList<Point>();
        closed = new DynList<Point>();
        startPoint = new Point(transform.position, targetObj);

        open.Add(startPoint);
        FindPath();
        index = finalPath.Length - 1;

    }

    
    private void FindPath()
    {
        Point lowest = startPoint;
        while (!targetObj.GetComponent<Entity>().hitbox.Overlaps(lowest.area))
        {

            //  Debug.Log(ite);
            closed.Add(lowest);
            lowest = null;
            foreach (Point p in open)
            {
                if (lowest == null) lowest = p;
                else if (p.f < lowest.f) lowest = p;

                
            }

            lowest.InitSurr();
            open.Remove(lowest);
            foreach (Point p in lowest.surrPoints)
            {
                //Debug.Log("already: " + p + ": " + p.f);
                if (AlreadyHas(p, closed) || AlreadyHas(p, open)) {  continue; }
                //DynList<GameObject> sc = p.scan();
                GameObject sc = p.scan();
                if (sc == null)
                {
                    open.Add(p);
                }
                else if (sc.tag.Contains(targetObj.tag)) { open.Add(p); break; }
                else if (sc.tag.Contains("Wall")) continue;
                else open.Add(p);
            }

            

            
         }
        finalPath = FinalizePath(lowest);
    }

    public GameObject marker;
    private bool AlreadyHas(Point p, DynList<Point> list)
    {
        foreach(Point other in list)
        {
            if (p.centre.Equals(other.centre)) return true;
        }
        return false;
    }
    private Point[] FinalizePath(Point from)
    {
        DynList<Point> result = new DynList<Point>();
        result.Add(from);
       // if(marker != null) Instantiate(marker, new Vector3(from.centre.x, from.centre.y, -1), Quaternion.identity);
        Point prevs = from;
        while(prevs.previous != null)
        {
            prevs = prevs.previous;
            result.Add(prevs);
           // if (marker != null) Instantiate(marker, new Vector3(prevs.centre.x, prevs.centre.y, -1), Quaternion.identity);
        }
        
        return result.toArr();
    }

    

}










