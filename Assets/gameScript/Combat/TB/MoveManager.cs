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
    }

    public static void Enq(Turn element, DynList<Turn> target = null)
    {
        if (target == null) turns.add(element);
        else target.add(element);
    }

    public static void Deq(DynList<Turn> target = null)
    {
        if (target == null) turns.remove(turns.first.data);
        else target.remove(target.first.data);
    }



    public static Turn Peek() { return turns.first.data; }


//----------------------------------------------------

//quicksort algorithm (inspired by the implementation by geeksforgeeks.org visited 15th aug 2023)

    private int Partition(DynList<Turn> target, int left, int right)
    {

        int pivot = target.dataAt(target.size-1).counter; //counter value of the last element in the list
        int result = left - 1;
        for(int i = left; i < right; i++)
        {
            if (target.dataAt(i).counter < pivot) target.Swap(++result, i);
        }
        target.Swap(++result, right);
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
            Sort(bench, 0, bench.size);
            turns.concat(bench);
            bench.clear();
        }
        


    }




}
