using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunOb : MonoBehaviour
{
    public Image im;
    public EnabledWeapon eweapon;
    public Selectable toggle;
    // Start is called before the first frame update
    public void OnchangedValue(bool val) {
        eweapon.enabled = val;
        var x = toggle.colors;
            x.normalColor = !val ? new Color(1,1,1,0.5f): new Color(1, 1, 1, 1);
        toggle.colors = x;
        Debug.Log(val);
    }
    void Start()
    {
        var x = toggle.colors;
        if (toggle is Toggle)
        {
            x.normalColor = !eweapon.enabled ? new Color(1, 1, 1, 0.5f) : new Color(1, 1, 1, 1);
        }
        toggle.colors = x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
