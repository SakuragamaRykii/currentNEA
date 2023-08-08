using System.Collections;
using System.Collections.Generic;
using UnityEngine;


            
public class QuadTree //: MonoBehaviour
{
    //
    int capacity = 3;
    public DynList<GameObject> objsInGrid;
    public bool hasSplit { get; private set; }
    int desNumber; //the nth descendant of the first tree

    //Centre essentials -------------------------------
    public Block Grid { get; private set; }
    

    //possible descendants------------------------------
    public QuadTree TR, BR, TL, BL;//top right, bottom right, top left, bottom left



    public QuadTree(Block grid)
    {
        Grid = grid;
        objsInGrid = new DynList<GameObject>();
        hasSplit = false;
    }
    public QuadTree(Block grid, int cap)
    {
        Grid = grid;
        capacity = cap;
        objsInGrid = new DynList<GameObject>();
        hasSplit = false;
    }


    
    public void insert(GameObject obj)
    {
        if (!Grid.Contains(obj)) return;

        objsInGrid.add(obj);

        if(objsInGrid.size > capacity)
        {
            if (!hasSplit) split();
            TR.insert(obj);
            BR.insert(obj);
            TL.insert(obj);
            BL.insert(obj);
        }
    }
    public void remove(GameObject obj)
    {
        if (Grid.Contains(obj)) return;
        else objsInGrid.remove(obj);
    }

    public void split()
    { 
        float x = this.Grid.position.x;
        float y = this.Grid.position.y;
        float w = this.Grid.scaleX;
        float h = this.Grid.scaleY;

        Block blTR = new Block(new Vector2(x + w / 4, y + h / 4), w / 2, h / 2);
        TR = new QuadTree(blTR);
        Block blBR = new Block(new Vector2(x + w / 4, y - h / 4), w / 2, h / 2);
        BR = new QuadTree(blBR);
        Block blTL = new Block(new Vector2(x - w / 4, y + h / 4), w / 2, h / 2);
        TL = new QuadTree(blTL);
        Block blBL = new Block(new Vector2(x - w / 4, y - h / 4), w / 2, h / 2);
        BL = new QuadTree(blBL);

        hasSplit = true;
    }

    public DynList<GameObject> query(GameObject qObj, DynList<GameObject> result = null)
    {
        if (result == null) result = new DynList<GameObject>();
        if (!Grid.Contains(qObj)) return null;
        if (!hasSplit)
        {
            return this.objsInGrid;
        }
        else
        {
            result.concat(TR.query(qObj));
            result.concat(TL.query(qObj));
            result.concat(BR.query(qObj));
            result.concat(BL.query(qObj));
        }
        return result;



    }

    //---CURRENT SAVE IS NOT YET COMMITTED---
}
