using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class tbCombat : Turn
{
    DynList<Move> movesOwned;
    VisualElement root;
    Button attack, defend, item;
    Button target1, target2, target3;
    VisualElement arrow1, arrow2, arrow3;
    TextElement yourHealth, log;
    private void Awake()
    {
        //the player turn gets enqueued by the MoveManager code.
        moved = false;
        baseCounter = PlayerStat.speed;
        counter = baseCounter;

        root = GetComponent<UIDocument>().rootVisualElement;
        // button assigments ---------------------------------------------------------------------------------------------------------------------------------------------
        attack = root.Q<Button>("Attack");
        defend = root.Q<Button>("Defend");
        item = root.Q<Button>("Item");

        target1 = root.Q<Button>("Target1");
        target2 = root.Q<Button>("Target2");
        target3 = root.Q<Button>("Target3");

        // arrow assigments ------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        arrow1 = root.Q<VisualElement>("Arrow1"); arrow1.SetEnabled(false); //assign arrow object and disable them.
        arrow2 = root.Q<VisualElement>("Arrow2"); arrow2.SetEnabled(false); //arrows are only allowed to be enabled when the user 
        arrow3 = root.Q<VisualElement>("Arrow3"); arrow3.SetEnabled(false);//presses the button according to it.

        // Your Health display--------------------------------------------------------------------------------------------------------------------------------------------
        yourHealth = root.Q<TextElement>("health-value");
        yourHealth.text = ("Level " + PlayerStat.level + ", Health: " + PlayerStat.currentHP + "/" + PlayerStat.maxHP);
        //--------------------------------------------------------------------------------------------------------------------------------------------
        log = root.Q<UnityEngine.UIElements.TextElement>("log-text");

        target1.clicked += () => targetIndex = 0;
        target2.clicked += () => targetIndex = 1;
        target3.clicked += () => targetIndex = 2;

        attack.clicked += () => handleAttack();
        defend.clicked += () => handleDefend();
        item.clicked += () => handleItem();
    }
    int targetIndex;



    private void FixedUpdate()
    {
        yourHealth.text = ("Level " + PlayerStat.level + "Health: " + PlayerStat.currentHP + "/" + PlayerStat.maxHP);
    }
    public override void ManageTurn()
    {
        log.text = "Your Turn";
        moved = true;


    }
    void handleAttack()
    {
       // Debug.Log("attack");

        if (moved && arrow1.enabledSelf)
        {
            //Debug.Log("ATTACK");
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
            
            try
            {
                targets[targetIndex].GetComponent<EnemyStat>().TakeDamage(PlayerStat.attack);
            }catch(IndexOutOfRangeException e)
            {
                targetIndex = targets.Length-1;
                targets[targetIndex].GetComponent<EnemyStat>().TakeDamage(PlayerStat.attack);
            }

            log.text = ((int)PlayerStat.attack + " Damage");

            Debug.Log("you have dealt " + PlayerStat.attack + " damage to the enemies");
            MoveManager.Deq();
            moved = false;
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
            moved = false;
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
            moved = false;
        }
        else
        {
            arrow1.SetEnabled(false);
            arrow2.SetEnabled(false);
            arrow3.SetEnabled(true);
        }



    }

    

}
