using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    public UIflipper uiflip;
    public int deathCount = 0;
    public TextMeshProUGUI deaths;
    void Update()
    {
        deaths.text = "Deaths: " + deathCount.ToString();
        uiflip.FlipUIElement();
    }
}
