using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.ExceptionServices;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

[System.Serializable]
public class DynList<T> : IEnumerable where T : class
{
    public ListNode<T> first { get;  private set; }
    public ListNode<T> last { get;  private set; }

    public int size {get; private set;}


    public DynList()
    {
        first = null;
        last = null;
        size = 0;
    }


    public void Add(T data) {
        if (data == null) return;
        ListNode<T> newNode = new ListNode<T>(data);


        if (first == null)
        {
            first = newNode;
            last = first;
            size++;
            return;
        }


        last.next = newNode;
        newNode.previous = last;
        last = newNode;
        size++;

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

        foreach(T elem in other)
        {
            if (isEmpty())
            {
                first = new ListNode<T>(elem);
                last = first;
                size++;
            }
            else
            {
                ListNode<T> temp = last;
                last.next = new ListNode<T>(elem);
                last = last.next;
                last.previous = temp;
                size++;
            }


        }
        //Debug.Log(this);
    }
    /*public void add(T data, int atIndex)
    {

    }*/

    public void Remove(T queryData)
    {
        if (queryData == null) return;
        if (!Contains(queryData)) return;
        if (first.data == queryData)
        {
           
            first = first.next;
            size--;
            return;
        }
        else if(last.data == queryData) 
        {
            last = last.previous;
            size--;
            return;
        }
        ListNode<T> current = first.next;
        ListNode<T> prev = current.previous;
        for (int i = 1; i < size-1; i++)
        {
            if(current.data ==  queryData)
            {
                current = current.next;
                prev.next = current;
                current.previous = prev;
                size--;
                return;

            }
            current = current.next;
            prev = prev.next;
        }
    }

    public bool Contains(T subject)
    {
        if (subject == null) return false;
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
            return null;
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
        return null;
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
        foreach (T t in arr) result.Add(t);
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
        private ListNode<T> curr;

        public MyEnumerator(DynList<T> l)
        {
            list = l;
            curr = l.first;
        }

        public bool MoveNext()
        {
            pos++;
            if(pos > 0 ) curr = curr.next;
            return (pos < list.size);

        }

        public void Reset() { pos = -1; curr = list.first; }

        public object Current
        {
            get { return curr.data; }
        }
    }

    public IEnumerator GetEnumerator()
    {
        return new MyEnumerator(this);
    }
}
