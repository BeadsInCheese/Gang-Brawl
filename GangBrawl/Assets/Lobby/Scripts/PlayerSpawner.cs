using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            var bodyOB = player.gameObject.transform.Find("FactionDownscaled/bone_1/Body");
            var HandOB1 = player.gameObject.transform.Find("FactionDownscaled/bone_1/bone_2/bone_3/HandShoot");
            var HandOB2 = player.gameObject.transform.Find("FactionDownscaled/bone_1/bone_2/bone_4/HandPunch");
            var LegOB = player.gameObject.transform.Find("FactionDownscaled/bone_5/Leg1");
            var LegOB2 = player.gameObject.transform.Find("FactionDownscaled/bone_6/Leg2");
            Debug.Log(hob);
            Debug.Log(i.hat);
            hob.gameObject.GetComponent<SpriteRenderer>().sprite = i.hat;
            bodyOB.gameObject.GetComponent<SpriteRenderer>().sprite = i.Body;
            HandOB1.gameObject.GetComponent<SpriteRenderer>().sprite = i.Hands;
            HandOB2.gameObject.GetComponent<SpriteRenderer>().sprite = i.Hands;
            LegOB2.gameObject.GetComponent<SpriteRenderer>().sprite = i.Legs;
            LegOB.gameObject.GetComponent<SpriteRenderer>().sprite = i.Legs;
            playerNo = playerNo + 1;
            Camera.main.GetComponent<CameraControl>().players.Add(player.transform);
            if (DirectorBehaviour.spawnWithGuns)
            {
                var hp = player.gameObject.GetComponent<HPSystem>();
                
                hp.OnDeath.AddListener(hp.CreateCrate);
            }
        }
        for (int i = 0; i < LobbyManager.CPUCount; i++)
        {
            var c = Instantiate(PlayerCPU);
            var ai = c.GetComponent<AICharacter>();
            ai.tint = getAITintColor();
            if (DirectorBehaviour.spawnWithGuns)
            {
                var hp = c.GetComponent<HPSystem>();
                    hp.OnDeath.AddListener(hp.CreateCrate);
            }
                ai.index = playerNo;
            playerNo += 1;
            Camera.main.GetComponent<CameraControl>().players.Add(ai.transform);
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
