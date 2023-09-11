using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class otherQT// : MonoBehaviour
{
    public Rect grid;
    private int capcity;
    public otherQT tr, tl, br, bl;
    private bool splitted;
    public DynList<GameObject> inGrid;


    public otherQT(Rect g)
    {
        grid = g;
        capcity = 1;
        splitted = false;
        inGrid = new DynList<GameObject>();

    }

    public void insert(GameObject obj)
    {
        if (!grid.Contains(obj.transform.position)) return;
        Debug.Log(obj.name + ":" + grid + ":" + obj.transform.position); 
        if (splitted)
        {
            tl.insert(obj);
            tr.insert(obj);
            bl.insert(obj);
            br.insert(obj);
        }


    }

    public void split()
    {
        float halfX = grid.width / 2;
        float halfY = grid.height / 2;
        tl = new otherQT(new Rect(grid.x, grid.y+halfY, halfX, halfY));
        tr = new otherQT(new Rect(grid.x+halfX, grid.y + halfY, halfX, halfY));
        bl = new otherQT(new Rect(grid.x, grid.y, halfX, halfY));
        br = new otherQT(new Rect(grid.x+halfX, grid.y, halfX, halfY));
        splitted = true;
    }


}
