using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ButtonInvoker : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void buttonSelect();
    public UnityEvent btOnClick;
    public buttonSelect btSelect;
    Button button;
    void Start()
    {
        button=this.GetComponent<Button>();
        btOnClick=button.onClick;
        btSelect=button.Select;
    }

}
