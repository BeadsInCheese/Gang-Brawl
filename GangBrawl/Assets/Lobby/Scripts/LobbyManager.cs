using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LobbyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject row1;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public Button StartButton;
    int playersInGame = 0;
    int playersReady = 0;
    float countdown = 5;
    public TMPro.TextMeshProUGUI countdownText;
    public static int CPUCount = 0;
    public static List<PlayerData> playerData = new List<PlayerData>();



    //Map
    public string map = "Airship";






    public static bool startHasBeenPressed;
    public void playerPressedReady()
    {
        playersReady += 1;
    }
    public void playerPressedUnready()
    {
        playersReady -= 1;
    }
    public static LobbyManager instance;
    public void AddAI()
    {
        if (!player1.tag.Equals("Player"))
        {
            SetCPUReadyOnUI(player1);

        }
        else if (!player2.tag.Equals("Player"))
        {
            SetCPUReadyOnUI(player2);
        }
        else if (!player3.tag.Equals("Player"))
        {
            SetCPUReadyOnUI(player3);
        }
        else if (!player4.tag.Equals("Player"))
        {
            SetCPUReadyOnUI(player4);
        }
        else
        {
            return;
        }

        LobbyManager.CPUCount += 1;
        LobbyManager.instance.playerPressedReady();

    }

    /// <summary>
    /// Enables correct UI components to display that the "player" joined in the game is CPU.
    /// </summary>
    /// <param name="cpu">This is actually the player object, just different images are set visible when it is CPU</param>
    private void SetCPUReadyOnUI(GameObject cpu)
    {
        cpu.tag = "Player";
        cpu.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Ready";
        cpu.transform.Find("ImageCPUJoined").gameObject.SetActive(true);
        cpu.transform.Find("CPUImg").gameObject.SetActive(true);
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        InputDevice[] d;
        d = playerInput.devices.ToArray();
        playerData.Add(new PlayerData("", -1, playerInput.currentControlScheme, d));
        playersInGame += 1;
        //playerInput.transform.SetParent(row1.transform,false);
        if (!player1.tag.Equals("Player"))
        {
            //player1.transform.SetParent(playerInput.transform,false);
            SetPlayerReadyOnUI(playerInput, player1);
        }
        else if (!player2.tag.Equals("Player"))
        {
            SetPlayerReadyOnUI(playerInput, player2);
        }
        else if (!player3.tag.Equals("Player"))
        {
            SetPlayerReadyOnUI(playerInput, player3);
        }
        else if (!player4.tag.Equals("Player"))
        {
            SetPlayerReadyOnUI(playerInput, player4);
        }


    }

    private void SetPlayerReadyOnUI(PlayerInput playerInput, GameObject player)
    {
        playerInput.transform.gameObject.GetComponent<lobbyPlayer>().LobbyObject = player;
        player.tag = "Player";
        if (PlayerData.recogniseControllerType(playerInput.currentControlScheme) == ControllerType.Gamepad)
        {
            player.transform.Find("ReadyPrompt").gameObject.SetActive(true);
            player.transform.Find("ControllerImg").gameObject.SetActive(true);
        }
        else if (PlayerData.recogniseControllerType(playerInput.currentControlScheme) == ControllerType.Keyboard)
        {

            player.transform.Find("KeyboardSpaceImg").gameObject.SetActive(true);
            player.transform.Find("KeyboardIndicator").gameObject.SetActive(true);
        }

        player.transform.Find("ImagePlayerJoined").gameObject.SetActive(true);

    }
    void Awake()
    {
        CPUCount = 0;
        if (instance != null && instance != this)
        {
            Destroy(instance);
            instance = this;
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        DirectorBehaviour.PlayersAlive = new Dictionary<string, int>();
        DirectorBehaviour.PlayerKills = new Dictionary<string, int>();
        DirectorBehaviour.gameTime = DirectorBehaviour.INITIAL_STARTING_GAMETIME;
        StartButton.interactable = false;
    }

    public static void pressStart()
    {
        startHasBeenPressed = !startHasBeenPressed;
    }

    /// <summary>
    /// Has Enough players and/or CPU characters. Currently requires at least two characters for game to start
    /// </summary>
    /// <returns></returns>
    private bool HasEnoughPlayersAndCPU()
    {
        return playersInGame + CPUCount > 1;
    }

    /// <summary>
    /// I guess this is what this does, not sure :D
    /// </summary>
    /// <returns></returns>
    private bool AllPlayersHasPressedReady()
    {
        return playersReady >= playersInGame + CPUCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (AllPlayersHasPressedReady() && HasEnoughPlayersAndCPU())
        {
            StartButton.interactable = true;
        }
        else
        {
            StartButton.interactable = false;
        }
        //(playersReady >= playersInGame + CPUCount && HasEnoughPlayersAndCPU())  ||
        if (startHasBeenPressed && AllPlayersHasPressedReady() && HasEnoughPlayersAndCPU())
        {
            countdown -= Time.deltaTime;
            countdownText.text = Mathf.Ceil(countdown).ToString();
            if (countdown <= 0)
            {
                SceneManager.LoadScene(map);
            }
        }
        else
        {
            countdown = 5;
            countdownText.text = "";
        }
    }
}
