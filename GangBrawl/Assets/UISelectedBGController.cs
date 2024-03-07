using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISelectedBGController : MonoBehaviour, ISelectHandler,IDeselectHandler
{
    public GameObject m_SelectedBG;
    public void OnDeselect(BaseEventData eventData)
    {
        m_SelectedBG.SetActive(false);
        Debug.Log("Deselected");
    }

    public void OnSelect(BaseEventData eventData)
    {
       m_SelectedBG.SetActive(true);
        Debug.Log("Selected");
    }
    public void OnUnSelect() {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
    }
}
