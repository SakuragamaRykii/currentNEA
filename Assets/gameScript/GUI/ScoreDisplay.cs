using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI display;
    private void Start()
    {
        display = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        
        display.text = "Current Score: " + PlayerStat.score; 
    }
}
