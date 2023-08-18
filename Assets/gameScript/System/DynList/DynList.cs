using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynList<T> : IEnumerable
{
    public ListNode<T> first { get;  set; }
    public ListNode<T> last { get;  set; }

    public int size {get; private set;}


    public DynList()
    {
        first = null;
        last = null;
        size = 0;
    }


    public void add(T data) {

        ListNode<T> newNode = new ListNode<T>(data);


        if (first == null)
        {
            first = newNode;
            last = first;
            size++;
            //Debug.Log(data + " in " + this + "size = " + size);
            return;
        }


        last.next = newNode;
        newNode.previous = last;
        last = newNode;
        size++;
        //Debug.Log(data + " in " + this + "size = " + size);

    }

    public void concat(DynList<T> other)
    {
        if (first == null)
        {
            first = other.first; last = other.last; size = other.size;
        }
        else
        {
            last.next = other.first;
            last = other.last;
            size += other.size;
        }


    }
    /*public void add(T data, int atIndex)
    {

    }*/

    public void remove(T queryData)
    {
        ListNode<T> current = first;
        for(int index = 0; index < size; index++)
        {
            if (current.data.ToString().Equals(queryData.ToString()))
            {

                if (current == last)
                {
                    last = current.previous;
                    current = null;

                }
                else if (current == first)
                {
                    first = current.next;
                    current = null;
                }
                else
                {
                    ListNode<T> right = current.next;
                    ListNode<T> left = current.previous;
                    current = null;
                    left.next = right;
                }
                size--;
                break;
            }
            else current = current.next;
        }
    }

    public void clear() { first = null; last = null; size = 0; }

    public T dataAt(int qIndex) //returns the data at a specified index
    {
        if (qIndex == 0) return first.data;
        if (qIndex == size - 1) return last.data;

        ListNode<T> result = first;
        if (qIndex >= size) {
            Debug.Log("invalid index");
            return default(T);
        }
        for(int i = 0; i < size; i++)
        {
            //Debug.Log(i);
            //Debug.Log(result.data);
            if (i == qIndex)
            {
                return result.data;
            }
            result = result.next;
        }
        return default(T);
    }
    public ListNode<T> nodeAt(int qIndex) //dataAt, but the return type is the node itself.
    {
        if (qIndex == 0) return first;
        if (qIndex == size - 1) return last;

        ListNode<T> result = first;
        if (qIndex >= size)
        {
            Debug.Log("invalid index");
            return null;
        }
        for (int i = 0; i < size; i++)
        {
            //Debug.Log(i);
            //Debug.Log(result.data);
            if (i == qIndex)
            {
                return result;
            }
            result = result.next;
        }
        return null;
    }

    public T[] toArr()
    {
        T[] result = new T[size];
        ListNode<T> current = first;
        for (int i = 0; i < size; i++)
        {
            result[i] = current.data;
            current = current.next;
        }
        return result;
    }

    public static DynList<T> ToDList(T[] arr)
    {
        DynList<T> result = new DynList<T>();
        foreach (T t in arr) result.add(t);
        return result;

    }

    public bool isEmpty() { return (size == 0); }

    public void Swap(int swap, int swapTo)
    {
        ListNode<T> sNode = nodeAt(swap),
            stNode = nodeAt(swapTo),  //swapNode and swapWithNode
            temPre = sNode.previous, temNext = sNode.next;

        sNode.next = stNode.next;
        sNode.previous = stNode.previous;
        stNode.next = temNext;
        stNode.previous = temPre;
    }



    private class MyEnumerator : IEnumerator
    {
        // allow this class to be subject to a foreach loop (from the official web guide of c# by microsoft)
        public DynList<T> list;
        private int pos = -1;

        public MyEnumerator(DynList<T> myList)
        {
            list = myList;
        }

        public bool MoveNext()
        {
            pos++;
            return (pos < list.size);

        }

        public void Reset() { pos = -1; }

        public object Current
        {
            get { return list.dataAt(pos); }
        }
    }

    public IEnumerator GetEnumerator()
    {
        return new MyEnumerator(this);
    }
}
