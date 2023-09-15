using System.Collections;
using System.Collections.Generic;
using UnityEngine;


            
public class QuadTree //: MonoBehaviour
{
    //
    /* int capacity = 3;
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
             Debug.Log(objsInGrid);
             return this.objsInGrid;
         }
         else
         {
             result.Concat(TR.query(qObj));
             result.Concat(TL.query(qObj));
             result.Concat(BR.query(qObj));
             result.Concat(BL.query(qObj));
         }
         return result;



     }

     //---CURRENT SAVE IS NOT YET COMMITTED---  */

    public Rect grid;
    private int capacity;
    public QuadTree tr, tl, br, bl;
    private bool splitted;
    public DynList<GameObject> objsInGrid;


    public QuadTree(Rect g)
    {
        grid = g;
        capacity = 1;
        splitted = false;
        objsInGrid = new DynList<GameObject>();

    }

    public void Insert(GameObject obj)
    {
        if (!grid.Contains(obj.transform.position)) return;

        if (!objsInGrid.Contains(obj)) objsInGrid.add(obj);

       // Debug.Log(obj.name + ":" + grid + ":" + obj.transform.position);

        if (objsInGrid.size > capacity)
        {
            if (!splitted) { Split(); Insert(objsInGrid.DataAt(0)); }//the first element doesnt get added to the children when it splits
                                                                  //without this.
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
            if (!splitted) { Debug.Log(objsInGrid); return objsInGrid.toArr(); }

        objs.Concat(tl.Query(from));
        objs.Concat(tr.Query(from));
        objs.Concat(bl.Query(from));
        objs.Concat(br.Query(from));
        Debug.Log(objs);
        return objs.toArr();


    }

    public void find(GameObject g)
    {
        if (!grid.Contains(g.transform.position)) return;
        if (splitted)
        {
            tl.find(g);
            tr.find(g);
            br.find(g);
            bl.find(g);
        }
        Debug.Log(g);
    }
}
