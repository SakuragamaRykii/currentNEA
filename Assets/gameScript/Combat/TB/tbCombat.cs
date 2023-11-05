using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class tbCombat : Turn, IOverPrevention
{
    DynList<Move> movesOwned;
    VisualElement root;
    VisualElement tbInv;
    Button attack, defend, item;
    Button target1, target2, target3;
    VisualElement arrow1, arrow2, arrow3;
    TextElement yourHealth, log;
    Button invLeft, invRight, invCurrent;
    private void Awake()
    {
        //the player turn gets enqueued by the MoveManager code.
        moved = false;
        baseCounter = PlayerStat.speed;
        counter = baseCounter;

        root = GetComponent<UIDocument>().rootVisualElement;
        tbInv = root.Q<VisualElement>("SelectBar");
        tbInv.style.display = DisplayStyle.None;
        tbInv.SetEnabled(false);
        // button assigments ---------------------------------------------------------------------------------------------------------------------------------------------
        attack = root.Q<Button>("Attack");
        defend = root.Q<Button>("Defend");
        item = root.Q<Button>("Item");

        target1 = root.Q<Button>("Target1");
        target2 = root.Q<Button>("Target2");
        target3 = root.Q<Button>("Target3");

        invLeft = root.Q<Button>("LeftArrow");
        invRight = root.Q<Button>("RightArrow");
        invCurrent = root.Q<Button>("Current");
        Item first;
        try
        {
            first = Inventory.peek(0);
            invCurrent.text = first.name;
        }catch(NullReferenceException e) { invCurrent.text = "Empty"; }
        
        // arrow assigments ------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        arrow1 = root.Q<VisualElement>("Arrow1"); arrow1.SetEnabled(false); //assign arrow object and disable them.
        arrow2 = root.Q<VisualElement>("Arrow2"); arrow2.SetEnabled(false); //arrows are only allowed to be enabled when the user 
        arrow3 = root.Q<VisualElement>("Arrow3"); arrow3.SetEnabled(false);//presses the button according to it.

        // Your Health display--------------------------------------------------------------------------------------------------------------------------------------------
        yourHealth = root.Q<TextElement>("health-value");
        yourHealth.text = ("Level " + PlayerStat.level + ", Health: " + (int)PlayerStat.currentHP + "/" + (int)PlayerStat.maxHP);
        //--------------------------------------------------------------------------------------------------------------------------------------------
        log = root.Q<UnityEngine.UIElements.TextElement>("log-text");

        target1.clicked += () => targetIndex = 0;
        target2.clicked += () => targetIndex = 1;
        target3.clicked += () => targetIndex = 2;

        attack.clicked += () => handleAttack();
        defend.clicked += () => handleDefend();
        item.clicked += () => handleItem();

        invRight.clicked += () => SelectRight();
        invLeft.clicked += () => SelectLeft();
        invCurrent.clicked += () => UseCurrent();

    }
    int targetIndex;

    public void PreventHPOver(){if (PlayerStat.currentHP > PlayerStat.maxHP) PlayerStat.currentHP = PlayerStat.maxHP;}

    private void FixedUpdate()
    {
        PreventHPOver();
        yourHealth.text = ("Level " + PlayerStat.level + "Health: " + (int)PlayerStat.currentHP + "/" + (int)PlayerStat.maxHP);
        Inventory.Fist();
         switch (Inventory.currentlyEquipped.name)
         {
            
             case "Hammer":
                 counter = baseCounter * 0.7f;
                 break;
             case "Drill":
                 counter = baseCounter * 0.4f;
                 break;
         }

        if (!arrow3.enabledInHierarchy)
        {
            tbInv.SetEnabled(false);
            tbInv.style.display = DisplayStyle.None;
        }
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
            tbInv.SetEnabled(true);
            tbInv.style.display = DisplayStyle.Flex;
            //move is dequeued in the tbInvGUI

        }
        else
        {
            arrow1.SetEnabled(false);
            arrow2.SetEnabled(false);
            arrow3.SetEnabled(true);
        }



    }

    public int InvIndex = 0;
    public void SelectRight()
    {
        if (InvIndex < Inventory.itemsIn.Length-1) InvIndex++;
        Item current;
        try { current = Inventory.peek(InvIndex); }
        catch(NullReferenceException e)
        {
            invCurrent.text = "Empty";
            return;
        }
        invCurrent.text = current.name;

       
    }
    public void SelectLeft()
    {
        if (InvIndex > 0) InvIndex--;
        Item current;
        try { current = Inventory.peek(InvIndex); }
        catch (NullReferenceException e)
        {
            invCurrent.text = "Empty";
            return;
        }
        invCurrent.text = current.name;

    }
    public void UseCurrent()
    {
        Label log = root.Q<Label>("log-text");
        Item current;
        try
        {
            current = Inventory.peek(InvIndex);

        }catch(NullReferenceException e)
        {
            log.text = "Selected slot does not contain an item";
            return;
        }

        if (moved) { MoveManager.Deq(); moved = false; } else return;

        if (current.hasDurability)
        {
            Inventory.currentlyEquipped = (WeaponItem)current;
            log.text = "You equipped " + current.name;
        }
        else
        {
            Inventory.Use(current.name);
            log.text = "You used " + current.name;
        }
        tbInv.SetEnabled(false);
        tbInv.style.display = DisplayStyle.None;



    }




    }
