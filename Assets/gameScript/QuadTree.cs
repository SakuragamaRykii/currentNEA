using System.Collections;
using System.Collections.Generic;
using UnityEngine;


            
public class QuadTree //: MonoBehaviour
{
    //
    int capacity = 5;
    public GameObject[] objsinBlock { get; private set; }
    public bool hasSplit { get; private set; }
    int desNumber; //the nth descendant of the first tree

    //Centre essentials -------------------------------
    public Block Grid { get; private set; }
    

    //possible descendants------------------------------
    public QuadTree TR, BR, TL, BL;//top right, bottom right, top left, bottom left



    public QuadTree(Block grid)
    {
        this.Grid = grid;
        objsinBlock = new GameObject[capacity];
        hasSplit = false;
    }
    public QuadTree(Block grid, int cap)
    {
        this.Grid = grid;
        objsinBlock = new GameObject[cap];
        hasSplit = false;
    }


    private int amount = 0;
    public void insert(GameObject obj)
    {
        if (!Grid.contains(obj)) return; 

        if (amount < objsinBlock.Length)
        {
            objsinBlock[amount] = obj;
            amount++;
        }
        else
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
        if (!Grid.contains(obj) || GameObject.Find(obj.ToString()) == null)
        {
            for (int i = 0; i < objsinBlock.Length; i++)
            {
                if (objsinBlock[i] == obj)
                {
                    objsinBlock[i] = null;
                    break;
                }

            }

        }

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

    public void query(Block Qarea, List<GameObject> Qobjs)
    {

        if (!Grid.intersects(Qarea)) return;
        foreach(GameObject o in this.objsinBlock)
        {
            if (Qarea.contains(o)) Qobjs.Add(o); 
        }
        if (hasSplit)
        {
            TR.query(Qarea, Qobjs);
            TL.query(Qarea, Qobjs);
            BR.query(Qarea, Qobjs);
            BL.query(Qarea, Qobjs);
        }

    }

    //---CURRENT SAVE IS NOT YET COMMITTED---
}
