using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class xpBar : MonoBehaviour
{
    private Scrollbar xp;
    private TextMeshProUGUI value;

    void Start()
    {
        xp = GetComponent<Scrollbar>();
        xp.size = 1;
        value = GetComponentInChildren<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        xp.size = PlayerStat.currentEXP/PlayerStat.expToNextLevel;
        value.text = "XP to Next: " + (int)PlayerStat.currentEXP + "/" + (int)PlayerStat.expToNextLevel;

    }
}
