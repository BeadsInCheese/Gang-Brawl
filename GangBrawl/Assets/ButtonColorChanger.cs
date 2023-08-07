using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChanger : MonoBehaviour
{
    public Color newColor; // The new highlight color
    public Color pressedColor;
    public Color selectedColor;
    private List<Button> buttons;

    // Start is called before the first frame update
    void Start()
    {
        buttons=new List<Button>(Object.FindObjectsOfType<Button>());

        foreach (Button button in buttons){
            // Get the current color block of the button
            ColorBlock colorBlock = button.colors;
            // Change the highlighted color to the new color
            colorBlock.highlightedColor = newColor;
            colorBlock.pressedColor = pressedColor;
            colorBlock.selectedColor = selectedColor;
            // Apply the modified color block to the button
            button.colors = colorBlock;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
