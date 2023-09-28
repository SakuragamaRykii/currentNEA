using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PathFinder : MonoBehaviour
{

    [SerializeField] private GameObject targetObj;
    private Point startPoint, targetPoint;
    // Update is called once per frame
    DynList<Point> open, closed;  //closed and open list       
    private Point centrePoint;

    private void Start()
    {
        /*  open = new DynList<Point>();
          closed = new DynList<Point>();
          startPoint = new Point(transform.position, targetObj);
          targetPoint = new Point(targetObj.transform.position);
          centrePoint = startPoint;
          open.add(startPoint);*/


        Thread pFinderThread = new Thread(RepeatFind); //TRANSFORM IS NOT ALLOWED INSIDE THREADS.
        pFinderThread.Start();
    }
    int ite = 0;
    void Update()
    {
        KeyCode key = KeyCode.Space;
        if(Input.GetKeyDown(key))
        {
            ite++;
            FindPath();
        }
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            DeleteLowest();
        }
    }

    private void RepeatFind()
    {
        while (true)
        {
            open = new DynList<Point>();
            closed = new DynList<Point>();
            startPoint = new Point(transform.position, targetObj);
            targetPoint = new Point(targetObj.transform.position);
            centrePoint = startPoint;
            open.add(startPoint);
            //yield return new WaitForSeconds(0.5f);
            FindPath();
        }

    }

    [SerializeField] private GameObject point;
    [SerializeField] private GameObject op;
    [SerializeField] private GameObject path;
    public void FindPath()
    {
        Point lowest = startPoint;
        while (!targetObj.GetComponent<Entity>().hitbox.Overlaps(lowest.area))
        {
          //  Debug.Log(ite);
            lowest = null;
            foreach (Point p in open)
            {
                if (lowest == null) lowest = p;
                else if (p.f < lowest.f) lowest = p;

                
            }

            lowest.InitSurr();
            open.remove(lowest);
            if (point != null) { Instantiate(point, new Vector3(lowest.centre.x, lowest.centre.y, -10), Quaternion.identity); }
            foreach (Point p in lowest.surrPoints)
            {
                //Debug.Log("already: " + p + ": " + p.f);
                if (AlreadyHas(p, closed) || AlreadyHas(p, open)) {  continue; }
                GameObject sc = p.scan();
                if (sc == null)
                {
                    Instantiate(op, new Vector3(p.centre.x, p.centre.y, -10), Quaternion.identity);
                    open.add(p);
                }
                else if (sc.tag.Equals("Wall")) continue;
                else if (sc.tag.Equals(targetObj.tag)) { open.add(p); break; }
            }

            closed.add(lowest);
           // Debug.Log("closed = " + closed);
           // Debug.Log(open);
          //  Debug.Log(open.size);
            
         }
        FinalizePath(lowest);
    }

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
        result.add(from);
        Instantiate(path, new Vector3(from.centre.x, from.centre.y, -12), Quaternion.identity);
        Point prevs = from;
        while(prevs.previous != null)
        {
            prevs = prevs.previous;
            result.add(prevs);
            Debug.Log(prevs);
            Instantiate(path, new Vector3(prevs.centre.x, prevs.centre.y, -12), Quaternion.identity);
        }
        
        return result.toArr();
    }
    public void DeleteLowest()
    {
        Point lowest = null;
        foreach(Point p in open)
        {
            if (lowest == null) lowest = p;
            else if (p.f < lowest.f) lowest = p;

        }
        Debug.Log("Lowest = " + lowest);
        open.remove(lowest);
        Debug.Log(open);
    }
    

}










