using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
// Queue properties --------------------------

    public static DynList<Turn> turns;
    public static DynList<Turn> bench;

    private void Awake() 
    {
        turns = new DynList<Turn>();
        bench = new DynList<Turn>();
        Enq(GetComponentInParent<tbCombat>());
        spawnEnemies();
    }

    public static void Enq(Turn element)
    {
        turns.add(element);
    }

    public static void Deq()
    {
        turns.remove(turns.first.data);
    }



    public static Turn Peek() { return turns.first.data; }


    //----------------------------------------------------

    //quicksort algorithm (inspired by the implementation by geeksforgeeks.org visited 15th aug 2023)

    public void Swap(DynList<Turn> target, int a, int b)
    {
        ListNode<Turn> A = target.nodeAt(a), B = target.nodeAt(b);
        Turn temp = A.data;
        A.data = B.data;
        B.data = temp;
    }

    private int Partition(DynList<Turn> target, int left, int right)
    {

        int pivot = target.dataAt(right-1).counter; //counter of value at index right
        int result = left - 1;
        for(int i = left; i < right; i++)
        {
            if (target.dataAt(i).counter > pivot) Swap(target, ++result, i);
        }
        Swap(target, ++result, right-1);
        return result;

    }

    private void Sort(DynList<Turn> target,int left, int right)
    {
        if (left >= right) return;

        int pIndex = Partition(target, left, right);

        Sort(target, left, pIndex - 1);
        Sort(target, pIndex + 1, right);

    }


    private void Update()
    {
        if (turns.isEmpty() && !bench.isEmpty())
        {
            Debug.Log("reallocating turns");
            Sort(bench, 0, bench.size);

            Debug.Log("bench "+bench.ToString());
            turns.concat(bench);
            bench.clear();
            Debug.Log("turns "+ turns.ToString());
            Debug.Log("bench " + bench.ToString());

            foreach (Turn t in turns) t.moved = false;

        }

        if (!Peek().moved) Peek().ManageTurn();
        

    }

    [SerializeField] private GameObject enemy;
    private void spawnEnemies()
    {
        Instantiate<GameObject>(enemy, new Vector3(-8, 2, -5), transform.rotation);
    }




}
