using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public UIflipper uiflip;
    public int KillCount = 0;
    public TextMeshProUGUI kills;
    void Update()
    {
        kills.text = "Kills: " + KillCount.ToString();
        uiflip.FlipUIElement();
    }
}
