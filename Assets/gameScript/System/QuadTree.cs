using System.Collections;
using System.Collections.Generic;
using UnityEngine;


            
public class QuadTree 
{
    public Rect grid;
    private int capacity;
    public QuadTree tr, tl, br, bl;
    private bool splitted;
    public DynList<GameObject> objsInGrid;


    public QuadTree(Rect g)
    {
        grid = g;
        capacity = 2;
        splitted = false;
        objsInGrid = new DynList<GameObject>();

    }

    public void Insert(GameObject obj)
    {
        if (!grid.Contains(obj.transform.position)) return;

        if (!objsInGrid.Contains(obj)) objsInGrid.Add(obj);

        if (objsInGrid.size > capacity)
        {
            if (!splitted) { Split(); foreach(GameObject o in objsInGrid) Insert(o); }
            tl.Insert(obj);
            tr.Insert(obj);
            bl.Insert(obj);
            br.Insert(obj);

        }


    }

    public void Split()
    {
        float halfX = grid.width / 2;
        float halfY = grid.height / 2;
        tl = new QuadTree(new Rect(grid.x, grid.y + halfY, halfX, halfY));
        tr = new QuadTree(new Rect(grid.x + halfX, grid.y + halfY, halfX, halfY));
        bl = new QuadTree(new Rect(grid.x, grid.y, halfX, halfY));
        br = new QuadTree(new Rect(grid.x + halfX, grid.y, halfX, halfY));
        splitted = true;
    }

    public GameObject[] Query(GameObject from, DynList<GameObject> objs = null)
    {
        if (!grid.Contains(from.transform.position)) return null;
        if (objs == null) objs = new DynList<GameObject>();
        if (!splitted) { //Debug.Log(objsInGrid);
                         return objsInGrid.toArr(); }

        objs.Concat(tl.Query(from));
        objs.Concat(tr.Query(from));
        objs.Concat(bl.Query(from));
        objs.Concat(br.Query(from));
       // Debug.Log(objs);
        return objs.toArr();


    }


}
