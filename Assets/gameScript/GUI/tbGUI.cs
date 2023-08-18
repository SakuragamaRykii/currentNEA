using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class tbManager : MonoBehaviour
{
    VisualElement root;
    Button attack, defend, item;
    VisualElement arrow1, arrow2, arrow3;


    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        // button assigments ------------------------------------
        attack = root.Q<Button>("Attack");
        defend = root.Q<Button>("Defend");
        item = root.Q<Button>("Item");

        attack.clicked += () => handleAttack();
        defend.clicked += () => handleDefend();
        item.clicked += () => handleItem();
        // -------------------------------------------------------

        // arrow assigments ------------------------------------
        arrow1 = root.Q<VisualElement>("Arrow1");
        arrow2 = root.Q<VisualElement>("Arrow2");
        arrow3 = root.Q<VisualElement>("Arrow3");
        // -------------------------------------------------------






    }


    void handleAttack()
    {
        Debug.Log("attack");
        arrow1.SetEnabled(true);
        arrow2.SetEnabled(false);
        arrow3.SetEnabled(false);

    }
    void handleDefend()
    {
        Debug.Log("defend");
        arrow1.SetEnabled(false);
        arrow2.SetEnabled(true);
        arrow3.SetEnabled(false);
    }
    void handleItem()
    {
        Debug.Log("item");
        arrow1.SetEnabled(false);
        arrow2.SetEnabled(false);
        arrow3.SetEnabled(true);
    }



}
