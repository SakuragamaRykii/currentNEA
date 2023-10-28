using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelValue : MonoBehaviour
{
    private TextMeshProUGUI level;
     
    void Start()
    {
        level = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        level.text = PlayerStat.level.ToString();
    }
}
