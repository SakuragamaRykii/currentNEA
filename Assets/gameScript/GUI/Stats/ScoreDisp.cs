using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreDisp : MonoBehaviour
{
    private TextMeshProUGUI score;

    void Start()
    {
       score = GetComponent<TextMeshProUGUI>();
       score.text = "Current Score: " + PlayerStat.score;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Current Score: " + PlayerStat.score;

    }
}
