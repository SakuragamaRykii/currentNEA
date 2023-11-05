using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class hpBar : MonoBehaviour
{
    private Scrollbar hp;
    private TextMeshProUGUI value;
    void Start()
    {
        hp = GetComponent<Scrollbar>();
        value = GetComponentInChildren<TextMeshProUGUI>();
        hp.size = PlayerStat.currentHP / PlayerStat.maxHP;
        value.text = "Current HP: " + PlayerStat.currentHP + "/" + PlayerStat.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        hp.size = PlayerStat.currentHP / PlayerStat.maxHP;
        value.text = "Current HP: " + PlayerStat.currentHP + "/" + PlayerStat.maxHP;
    }
}
