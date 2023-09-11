using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynList<T> : IEnumerable where T : class
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

    public void Concat(DynList<T> other)
    {
        if (other == null || other.isEmpty()) return;
        if (isEmpty())
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
    public void Concat(T[] other)
    {
        if (other == null || other.Length == 0) return;

        ListNode<T> current;
        int i = 0;
        if (isEmpty())
        {
            first = new ListNode<T>(other[0]); i = 1;
            current = first;
        }
        else current = last;
        while(i < other.Length) 
        {
            ListNode<T> temp = current;
            current.next = new ListNode<T>(other[i]);
            last = current.next;
            current = current.next;
            size++;
            i++;
        }
        Debug.Log(this);
    }
    /*public void add(T data, int atIndex)
    {

    }*/

    public void remove(T queryData)
    {
        if (queryData == first.data)
        {
            ListNode<T> temp = first.next;
            first.remove();
            first = temp;
            size--; return;
        }

        if (queryData == last.data)
        {
            ListNode<T> temp = last.previous;
            last.remove();
            last = temp;
            size--; return;
        }

        ListNode<T> current = first.next;
        for(int index = 0; index < size-1; index++)
        {
            if (queryData == current.data)
            {
                ListNode<T> right = current.next;
                ListNode<T> left = current.previous;
                left.next = right;
                current.remove();
                size--; return;

            }
            else current = current.next;
        }
    }

    public bool Contains(T subject)
    {
        foreach(T t in this)
        {
            if (t == subject) return true;
        }
        return false;
    }

    public void Clear() { first = null; last = null; size = 0; }

    public T DataAt(int qIndex) //returns the data at a specified index
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


    public override string ToString()
    {
        string result=  "list:";
        foreach(T t in this)
        {
            result += t + ", ";
        }
        return result;
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
            get { return list.DataAt(pos); }
        }
    }

    public IEnumerator GetEnumerator()
    {
        return new MyEnumerator(this);
    }
}
