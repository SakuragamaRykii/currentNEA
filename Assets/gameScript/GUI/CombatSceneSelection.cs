using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CombatSceneSelection : MonoBehaviour
{
    private Button tbButton, rtButton;
    [SerializeField] private GameObject tbManager, rtManager;

    // Start is called before the first frame update
    void Start()
    {
        VisualElement vis = GetComponent<UIDocument>().rootVisualElement;
        tbButton = vis.Q<Button>("TB");
        rtButton = vis.Q<Button>("RT");

        tbButton.clicked += () => {
            Debug.Log("TB");
            tbManager.SetActive(true);
            gameObject.SetActive(false);

        };



        rtButton.clicked += () => {
            Debug.Log("RT");
            SceneManager.LoadScene("RTScene");
            gameObject.SetActive(false);
        };


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
