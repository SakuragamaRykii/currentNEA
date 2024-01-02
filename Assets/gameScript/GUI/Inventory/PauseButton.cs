using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseButton : MonoBehaviour
{
    Button button;
   
    public Button resumeButton;
    public Button quitButton;
    public GameObject darken;

    private void Awake()
    {
        button = GetComponent<Button>();
        

        button.onClick.AddListener(() => PauseGame());
        quitButton.onClick.AddListener(() => QuitGame()); 
        resumeButton.onClick.AddListener(() => ResumeGame());
        quitButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        darken.SetActive(false);
    }

    void PauseGame()
    {
        quitButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);
        darken.SetActive(true);

        Time.timeScale = 0;

    }
    void ResumeGame()
    {
        quitButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        darken.SetActive(false);

        Debug.Log("reseme");
        Time.timeScale = 1;
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
