using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynList<T> : IEnumerable, IEnumerator
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
            return;
        }


        last.next = newNode;
        newNode.previous = last;
        last = newNode;
        size++;

    }
    public void add(T data, int atIndex)
    {

    }

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
        for(int i = 0; i < size; i++)
        {
            Debug.Log(i);
            result = result.next;
        }
        return result.data;
    }
    private ListNode<T> nodeAt(int qIndex)
    {
        ListNode<T> result = first;
        for (int i = 0; i < size; i++)
        {
            result = result.next;
        }
        return result;
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



    // allow this class to be subject to a foreach loop
    private int pos = -1;
    public IEnumerator GetEnumerator() { return (IEnumerator)this; }
    public bool MoveNext()
    {
        pos++;
        return (pos < size);

    }

    public void Reset() { pos = -1; }

    public object Current
    {
        get { return dataAt(pos); }
    }


}
