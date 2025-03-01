using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class otherQT// : MonoBehaviour
{
    public Rect grid;
    private int capacity;
    public otherQT tr, tl, br, bl;
    private bool splitted;
    public DynList<GameObject> inGrid;


    public otherQT(Rect g)
    {
        grid = g;
        capacity = 1;
        splitted = false;
        inGrid = new DynList<GameObject>();

    }

    public void insert(GameObject obj)
    {
        if (!grid.Contains(obj.transform.position)) 
        {
            if (inGrid.Contains(obj)) inGrid.Remove(obj); //for objects that can move out of the grid 
            return;
        }

        if(!inGrid.Contains(obj)) inGrid.Add(obj);

        //Debug.Log(obj.name + ":" + grid + ":" + obj.transform.position);
        Debug.Log(inGrid);

        if (inGrid.size > capacity)
        {
           if(!splitted) Split();
            tl.insert(obj);
            tr.insert(obj);
            bl.insert(obj);
            br.insert(obj);

        }


    }

    public void Split()
    {
        float halfX = grid.width / 2;
        float halfY = grid.height / 2;
        tl = new otherQT(new Rect(grid.x, grid.y+halfY, halfX, halfY));
        tr = new otherQT(new Rect(grid.x+halfX, grid.y + halfY, halfX, halfY));
        bl = new otherQT(new Rect(grid.x, grid.y, halfX, halfY));
        br = new otherQT(new Rect(grid.x+halfX, grid.y, halfX, halfY));
        splitted = true;
    }

    public DynList<GameObject> Query(GameObject from, DynList<GameObject> objs = null)
    {
        if (!grid.Contains(from.transform.position)) return null;
        if (objs == null) objs = new DynList<GameObject>();
        if (!splitted) return inGrid;

        objs.Concat(tl.Query(from));
        objs.Concat(tr.Query(from));
        objs.Concat(bl.Query(from));
        objs.Concat(br.Query(from));
        return objs;


    }


}
