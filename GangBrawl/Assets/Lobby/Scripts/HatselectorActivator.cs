using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatselectorActivator : MonoBehaviour
{
    public HatSelector hs;
    private void OnEnable()
    {
        hs.turnOn();
    }
}
