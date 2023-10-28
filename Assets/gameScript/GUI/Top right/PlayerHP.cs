using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    private Scrollbar bar;
    private TextMeshProUGUI hp;
    void Start()
    {
        bar = GetComponent<Scrollbar>();
        hp = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        hp.text = "Current HP: " + (int)PlayerStat.currentHP + "/" + (int)PlayerStat.maxHP;
        bar.size = PlayerStat.currentHP / PlayerStat.maxHP;
    }
}
