using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public LobbyManager lobbymanager;
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
