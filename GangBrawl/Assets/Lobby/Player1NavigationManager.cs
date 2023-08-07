using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class Player1NavigationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D cursorTexture;
    void Start()
    {
        Cursor.SetCursor(cursorTexture,new Vector2(0,0),CursorMode.ForceSoftware);
    }
    private void OnDestroy()
    {
        Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.ForceSoftware);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
