using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    Button beginButton, quitButton;
    VisualElement vis;


    // Start is called before the first frame update
    void Start()
    {
        vis = GetComponent<UIDocument>().rootVisualElement;
        beginButton = vis.Q<Button>("Start");
        quitButton = vis.Q<Button>("Quit");

        beginButton.clicked += () => { SceneManager.LoadScene("FieldScene");  };

        quitButton.clicked += () => { Application.Quit(); };
    }



}
