using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextHealth : MonoBehaviour
{
    public TextMeshProUGUI text;

    void UpdateHealthText()
    {
        int crntHp = PlayerData.Instance.GetCurrentHP();
        int maxHp = PlayerData.Instance.GetMaxHP();
        text.text = $"{crntHp}/{maxHp}";
    }

    void Start()
    {
        UpdateHealthText();
    }

    // Call this method whenever you need to update the health text
    public void RefreshHealthText()
    {
        UpdateHealthText();
    }
}
