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
    bool found;
    private Point centrePoint;

    private void Start()
    {
        open = new DynList<Point>();
        closed = new DynList<Point>();
        startPoint = new Point(transform.position, targetObj);
        targetPoint = new Point(targetObj.transform.position);
        found = false;
        centrePoint = startPoint;
        open.add(startPoint);

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

    [SerializeField] private GameObject point;
    [SerializeField] private GameObject op;
    public void FindPath()
    {
        Debug.Log(ite);
        Point lowest = null;
        foreach(Point p in open)
        {
            if (lowest == null) lowest = p;
            else if (p.f < lowest.f) lowest = p;
        }

        lowest.InitSurr();
        open.remove(lowest);
        if(point != null) { Instantiate(point, new Vector3(lowest.centre.x, lowest.centre.y, -10), Quaternion.identity); }
        foreach(Point p in lowest.surrPoints)
        {
            if (AlreadyHas(p, closed) || AlreadyHas(p, open)) { Debug.Log("already: " + p + ": " + p.f); continue; }
            GameObject sc = p.scan();
            if (sc == null)
            {
                Instantiate(op, new Vector3(p.centre.x, p.centre.y, -10), Quaternion.identity); 
                open.add(p);
            }
            else if (sc.tag.Equals("Wall")) continue;
        }

        closed.add(lowest);
        Debug.Log("closed = " + closed);
        Debug.Log(open);
        Debug.Log(open.size);


    }

    private bool AlreadyHas(Point p, DynList<Point> list)
    {
        foreach(Point other in list)
        {
            if (p.centre.Equals(other.centre)) return true;
        }
        return false;
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










