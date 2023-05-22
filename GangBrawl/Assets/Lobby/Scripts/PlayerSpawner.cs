using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerCPU;
    PlayerInputManager playerInputManager;
    public List<Color> PlayerColorTints = new List<Color>();
    public List<Color> usedColors = new List<Color>();
    public void spawnPlayers()
    {
        int playerNo = 0;
        foreach (PlayerData i in LobbyManager.playerData.Values)
        {

            var player = playerInputManager.JoinPlayer(playerNo, -1, null, i.pairWithDevices);
            player.gameObject.GetComponent<CharacterControl>().tint = i.tintColor;
            usedColors.Add(i.tintColor);
            var hob = player.gameObject.transform.Find("FactionDownscaled/bone_1/bone_2/Hat");
            Debug.Log(hob);
            Debug.Log(i.hat);
            hob.gameObject.GetComponent<SpriteRenderer>().sprite = i.hat;
            playerNo = playerNo + 1;

        }
        for (int i = 0; i < LobbyManager.CPUCount; i++)
        {
            var c = Instantiate(PlayerCPU);
            var ai = c.GetComponent<AICharacter>();
            ai.tint = getAITintColor();
            ai.index = playerNo;
            playerNo += 1;
        }
        LobbyManager.CPUCount = 0;
        LobbyManager.playerData = new Dictionary<string, PlayerData>();

    }

    /// <summary>
    ///   AI does not have assigned tintColor. Therefore only thing that matters is that
    /// players have correct tint colors and the AI uses the unused ones.
    /// </summary>
    /// <returns> Assigns AI an unsed tintColor</returns>
    public Color getAITintColor()
    {
        // The dfault value should be never needed
        Color tintColor = Color.white;
        bool colorHasBeenAssigned = false;
        foreach (Color c in PlayerColors.Colors)
        {
            if (!usedColors.Contains(c) && !colorHasBeenAssigned)
            {
                tintColor = c;
                usedColors.Add(c);
                colorHasBeenAssigned = true;
            }
        }
        return tintColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        spawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
