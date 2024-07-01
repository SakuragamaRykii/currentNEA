using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MoveManager : MonoBehaviour
{
// Queue properties --------------------------

    public static DynList<Turn> turns;
    public static DynList<Turn> bench;
    public static bool finished = false;
    public static VisualElement root;
    


    private void Awake() 
    {
        root = GameObject.FindObjectOfType<UIDocument>().rootVisualElement;//MAY TAKE SELECTION GUI
        root.Q<Button>("Target1").SetEnabled(false);
        root.Q<Button>("Target2").SetEnabled(false);
        root.Q<Button>("Target3").SetEnabled(false);

        enemiesOnATM = 0;
        turns = new DynList<Turn>();
        bench = new DynList<Turn>();
        finished = false;
        Enq(GetComponentInParent<tbCombat>());
        spawnEnemies();
        StartCoroutine(ManageTurns());
        Sort(turns, 0, turns.size);

    }

    public static int enemiesOnATM;
    private IEnumerator ManageTurns()
    {
        while (!finished)
        {
            yield return new WaitForSeconds(1) ;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <=0 ) { finished = true; break; }
            try
            {
                if (!Peek().moved) Peek().ManageTurn();

            }catch(Exception e)
            {
                //turns.Remove(Peek());
              //  Debug.Log("turn = " + (Peek() == null));
            }
            if (turns.isEmpty() && !bench.isEmpty())
            {
                Debug.Log("reallocating turns");
 
                Sort(bench, 0, bench.size);
                turns.Concat(bench);
                bench.Clear();
                Debug.Log(turns);
                foreach (Turn t in turns)
                {
                    t.moved = false;
                }


            }

        }
        //EnemyStat.ended = true;

        Debug.Log("fight finished");
        SceneManager.LoadScene("FieldScene");
        
    }




    public static void Enq(Turn element)
    {
        turns.Add(element);
    }

    public static void Deq()
    {
        bench.Add(turns.first.data);
        turns.Remove(turns.first.data);
        
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

        float pivot = target.DataAt(right-1).counter; //counter of value at index right
        int result = left - 1;
        for(int i = left; i < right; i++)
        {
            if (target.DataAt(i).counter > pivot) Swap(target, ++result, i);
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


    [SerializeField] private GameObject enemy;
    private void spawnEnemies()
    {
        int xPos = -8;
        //Random.Range(1, 4);
        for (int i = 0; i < UnityEngine.Random.Range(1, 4); i++)
        {
            enemiesOnATM++;
            enemy.name = "Enemy" + i;
            Debug.Log("i = " + i);
            Instantiate(enemy, new Vector3(xPos, 2, -5), transform.rotation);
            xPos += 3;
            root.Q<Button>("Target" + enemiesOnATM).SetEnabled(true);
        }

    }




}
