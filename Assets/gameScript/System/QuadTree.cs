using System.Collections;
using System.Collections.Generic;
using UnityEngine;


            
public class QuadTree //: MonoBehaviour
{
    //
    int capacity = 3;
    public DynList<GameObject> objsinBlock;
    public bool hasSplit { get; private set; }
    int desNumber; //the nth descendant of the first tree

    //Centre essentials -------------------------------
    public Block Grid { get; private set; }
    

    //possible descendants------------------------------
    public QuadTree TR, BR, TL, BL;//top right, bottom right, top left, bottom left



    public QuadTree(Block grid)
    {
        this.Grid = grid;
        objsinBlock = new DynList<GameObject>();
        hasSplit = false;
    }
    public QuadTree(Block grid, int cap)
    {
        this.Grid = grid;
        capacity = cap;
        objsinBlock = new DynList<GameObject>();
        hasSplit = false;
    }


    
    public void insert(GameObject obj)
    {
        if (!Grid.contains(obj)) return; 

        objsinBlock.add(obj);

        if(objsinBlock.size > capacity)
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
        if (Grid.contains(obj)) return;
        else objsinBlock.remove(obj);
    }

    public void split()
    { 
        float x = this.Grid.centrePos.x;
        float y = this.Grid.centrePos.y;
        float w = this.Grid.width;
        float h = this.Grid.height;

        Block blTR = new Block(new Vector2(x + w / 4, y + h / 4), w / 4, h / 4);
        TR = new QuadTree(blTR);
        Block blBR = new Block(new Vector2(x + w / 4, y - h / 4), w / 4, h / 4);
        BR = new QuadTree(blBR);
        Block blTL = new Block(new Vector2(x - w / 4, y + h / 4), w / 4, h / 4);
        TL = new QuadTree(blTL);
        Block blBL = new Block(new Vector2(x - w / 4, y - h / 4), w / 4, h / 4);
        BL = new QuadTree(blBL);

        hasSplit = true;
    }

    public GameObject[] query(Block Qarea, DynList<GameObject> Qobjs = null)
    {
        if (Qobjs == null) Qobjs = new DynList<GameObject>();


        if (!Grid.intersects(Qarea)) return null;
        foreach(GameObject o in this.objsinBlock)
        {
            if (Qarea.contains(o)) Qobjs.add(o); 
        }
        if (hasSplit)
        {
            TR.query(Qarea, Qobjs);
            TL.query(Qarea, Qobjs);
            BR.query(Qarea, Qobjs);
            BL.query(Qarea, Qobjs);
        }

        return Qobjs.toArr();

    }

    //---CURRENT SAVE IS NOT YET COMMITTED---
}
