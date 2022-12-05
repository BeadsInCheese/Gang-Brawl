using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
                SceneManager.LoadScene("Lobby");
    }

    public void Quitgame()
    {
        Application.Quit();
    }

    public void Start()
    {
        if (gameObject.name.Equals("SettingsMenu"))
        {
            gameObject.SetActive(false);
        }
    }
}