using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // Start is called before the first frame update
   // float x, y;
    [SerializeField] private Entity targetObj;//probably the player
    private Point startPoint, targetPoint;
    // Update is called once per frame
    DynList<Point> open, closed;  //closed and open list       
    bool found = false;



    void Start()
    {
        open = new DynList<Point>();
        closed = new DynList<Point>();
        startPoint = new Point(GetComponent<Entity>().hitbox);
        Debug.Log(startPoint.c);
        targetPoint = new Point(targetObj.hitbox);
        found = false;
        FindPath();
        //StartCoroutine(search());
    }
    public IEnumerator search()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            open = new DynList<Point>();
            closed = new DynList<Point>();
            startPoint = new Point(GetComponent<Entity>().hitbox);
            Debug.Log(startPoint.c);
            targetPoint = new Point(targetObj.hitbox);
            FindPath();
        }

    }




    [SerializeField] private GameObject point;

    public void FindPath()
    {
        open.add(startPoint);
        Debug.Log(open);
      //  while (!open.isEmpty() && !found)
      //  {
      for(int i = 0; i < 10; i++) { 
            Point q = null;
            foreach (Point p in open)
            {
                if (q == null) q = p;
                else if(p.f < q.f) q = p;

            }
            open.remove(q);
            Successor(q);
      
            Debug.Log(open);
            if (point != null) Instantiate(point, new Vector3(q.c.x, q.c.y, - 10), Quaternion.identity);
            closed.add(q);

        }

    }


    

    private bool AlreadyIn(Point p, DynList<Point> list) //check if already visited
    {
        foreach(Point check in list)
        {

            if (check.c.Equals(p.c) && check.f <= p.f) { Debug.Log("true"); return true; }
        }
        Debug.Log("false");
        return false;
    }

    private void Successor(Point of)
    {
        Vector2 c = of.c;
        of.up = new Point(new Vector2(c.x, c.y + 1f), startPoint.c, targetPoint.c);
        of.right = new Point(new Vector2(c.x + 1f, c.y), startPoint.c, targetPoint.c);
        of.down = new Point(new Vector2(c.x, c.y - 1f), startPoint.c, targetPoint.c);
        of.left = new Point(new Vector2(c.x - 1f, c.y), startPoint.c, targetPoint.c);
        ManagePoint(new Point[] {of.up, of.right, of.down, of.left});
    }
    private void ManagePoint(Point[] points)
    {
        foreach(Point p in points)
        {
            if(AlreadyIn(p, open) || AlreadyIn(p, closed)) continue;
            GameObject sc = p.scan();
            if (sc == null) open.add(p);
            else if (sc.tag == "Wall") continue;
            else if(sc.tag == targetObj.tag) { found = true; return; }
        }
    }


 


        

}



