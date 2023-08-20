using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class tbCombat : Turn
{
    DynList<Move> movesOwned;
    VisualElement root;
    Button attack, defend, item;
    VisualElement arrow1, arrow2, arrow3;
    private void Awake()
    {

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


    private void Update()
    {

        if (MoveManager.Peek() == this && !moving) ManageTurn();

    }


    public new void ManageTurn()
    {
        Debug.Log("your turn");
        moving = true;


    }
    void handleAttack()
    {
        Debug.Log("attack");

        if (moving && arrow1.enabledSelf)
        {
            Debug.Log("ATTACK");
            MoveManager.Deq();
            MoveManager.Enq(this, MoveManager.bench);
            moving = false;
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
        if (moving && arrow2.enabledSelf)
        {
            Debug.Log("DEFEND");
            MoveManager.Deq();
            MoveManager.Enq(this, MoveManager.bench);
            moving = false;
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
        if (moving && arrow3.enabledSelf)
        {
            Debug.Log("ITEM");
            MoveManager.Deq();
            MoveManager.Enq(this, MoveManager.bench);
            moving = false;
        }
        else
        {
            arrow1.SetEnabled(false);
            arrow2.SetEnabled(false);
            arrow3.SetEnabled(true);
        }



    }

}
