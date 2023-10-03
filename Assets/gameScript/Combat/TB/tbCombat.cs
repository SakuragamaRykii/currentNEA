using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class tbCombat : Turn
{
    DynList<Move> movesOwned;
    VisualElement root;
    Button attack, defend, item;
    VisualElement arrow1, arrow2, arrow3;
    private void Awake()
    {
        //the player turn gets enqueued by the MoveManager code.
        moved = false;
        baseCounter = PlayerStat.speed;
        counter = baseCounter;

        root = GetComponent<UIDocument>().rootVisualElement;
        // button assigments ------------------------------------
        attack = root.Q<Button>("Attack");
        defend = root.Q<Button>("Defend");
        item = root.Q<Button>("Item");


        // -------------------------------------------------------

        // arrow assigments ------------------------------------
        arrow1 = root.Q<VisualElement>("Arrow1"); arrow1.SetEnabled(false); //assign arrow object and disable them.
        arrow2 = root.Q<VisualElement>("Arrow2"); arrow2.SetEnabled(false); //arrows are only allowed to be enabled when the user 
        arrow3 = root.Q<VisualElement>("Arrow3"); arrow3.SetEnabled(false);//presses the button according to it.
                                                                           // -------------------------------------------------------

        attack.clicked += () => handleAttack();
        defend.clicked += () => handleDefend();
        item.clicked += () => handleItem();
    }





    public override void ManageTurn()
    {
        Debug.Log("your turn");
        moved = true;


    }
    void handleAttack()
    {
        Debug.Log("attack");

        if (moved && arrow1.enabledSelf)
        {
            Debug.Log("ATTACK");
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject g in targets)
            {
                g.GetComponent<EnemyStat>().TakeDamage(PlayerStat.attack);
            }
            Debug.Log("you have dealt " + PlayerStat.attack + " damage to the enemies");
        
            MoveManager.Deq();
        }
        else
        {
            arrow1.SetEnabled(true);
            arrow2.SetEnabled(false);
            arrow3.SetEnabled(false);
        }
    }
    void handleDefend()
    {
        Debug.Log("defend");
        if (moved && arrow2.enabledSelf)
        {
            Debug.Log("DEFEND");
            PlayerStat.currentHP += PlayerStat.defence;
            MoveManager.Deq();
        }
        else
        {
            arrow1.SetEnabled(false);
            arrow2.SetEnabled(true);
            arrow3.SetEnabled(false);
        }


    }
    void handleItem()
    {
        Debug.Log("item");
        if (moved && arrow3.enabledSelf)
        {
            Debug.Log("ITEM");
            MoveManager.Deq();
        }
        else
        {
            arrow1.SetEnabled(false);
            arrow2.SetEnabled(false);
            arrow3.SetEnabled(true);
        }



    }

}
