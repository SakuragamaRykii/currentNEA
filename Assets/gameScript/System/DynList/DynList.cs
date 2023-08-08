using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynList<T> : IEnumerable
{
    public ListNode<T> first { get; private set; }
    public ListNode<T> last { get; private set; }

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

    public T dataAt(int qIndex)
    {
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



    private class MyEnumerator : IEnumerator
    {
        // allow this class to be subject to a foreach loop
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
