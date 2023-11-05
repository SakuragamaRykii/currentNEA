using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    private TextMeshProUGUI level;
    void Start()
    {
        level = GetComponent<TextMeshProUGUI>();
        level.text = PlayerStat.level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        level.text = PlayerStat.level.ToString();
    }
}
