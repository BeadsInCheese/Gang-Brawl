using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    public int deathCount = 0;
    public CharacterControl charcontrol;
    public TextMeshProUGUI deaths;
    void Update()
    {
        deaths.text = "Deaths: " + deathCount.ToString();
        if (charcontrol.isSpriteFlipped() == true)
        {
            transform.rotation = Quaternion.Euler(new Vector3(-180f, -180f, -180f));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }
}
