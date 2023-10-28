using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class XPBar : MonoBehaviour
{
    private Scrollbar bar;
    private TextMeshProUGUI xp;
    void Start()
    {
        xp = GetComponentInChildren<TextMeshProUGUI>();
        bar = GetComponent<Scrollbar>();
    }

    void Update()
    {
        xp.text = "Current XP: " + (int)PlayerStat.currentEXP + "/" + (int)PlayerStat.expToNextLevel;
        bar.size = PlayerStat.currentEXP / PlayerStat.expToNextLevel;
    }
}
