using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using UnityEngine.InputSystem;
using System;
using UnityEngine.InputSystem.UI;

[Serializable]
public class EnabledWeapon
{
    public bool enabled = true;
    public Sprite graphic;
    public GameObject prefab;

}
public class LevelSelector : MonoBehaviour
{


    public List<EnabledWeapon> enabledWeapons;
    public List<EnabledWeapon> enabledStaffs;
    public TMP_Text modeLabel;
    /// <summary>
    /// "Time(S)" or "Starting Lives"
    /// </summary>
    public TMP_Text SecondaryModifierHeaderText;

    /// <summary>
    /// Can be lives or time in S
    /// </summary>
    public TMP_Text SecondaryModifierLabel;

    public Image mapThumbnail;

    private int TIMESCALE = 30;


    //Map change logic
    public List<Map> maps;
    private int mapIndex=0;

    public GameObject LobbyGunPrefab;
    public List<GameObject> weaponmenu=new List<GameObject>();
    public GameObject canvas;
    public MultiplayerEventSystem eventsys;
    public int rowsize = 5;
    public int columsize = 5;
    public GameObject closebuttonPrefab;
    public GameObject resetButton;
    public void ShowWeaponSetUp()
    {
        Debug.Log("Running gun Setup");
        weaponmenu = new List<GameObject>();
        int row = 0;
        int colum = 0;
        int index = 0;
        foreach(var i in enabledWeapons)
        {
            Debug.Log("Running gun Setup");

            row = index % rowsize;
            colum = index / columsize;
            var ob= Instantiate(LobbyGunPrefab);
            var temp = ob.GetComponent<GunOb>();
            temp.eweapon = i;
            temp.im.sprite = i.graphic;
            ob.transform.parent = canvas.transform;
            ob.transform.localScale = Vector3.one*10;
            ob.transform.localPosition = new Vector3(row*200,-colum*200,1000);
            weaponmenu.Add(ob);
            index++;
        }
        foreach (var i in enabledStaffs)
        {
            Debug.Log("Running gun Setup");

            row = index % 5;
            colum = index / 5;
            var ob = Instantiate(LobbyGunPrefab);
            var temp = ob.GetComponent<GunOb>();
            temp.eweapon = i;
            temp.im.sprite = i.graphic;
            ob.transform.parent = canvas.transform;
            ob.transform.localScale = Vector3.one * 10;
            ob.transform.localPosition = new Vector3(row * 200, -colum * 200, 1000);
            weaponmenu.Add(ob);
            index++;
        }
        {
            row = (index + 1) % 5;
            colum = (index + 1) / 5;
            var ob = Instantiate(closebuttonPrefab);
            ob.transform.parent = canvas.transform;
            ob.transform.localScale = Vector3.one * 10;
            ob.transform.localPosition = new Vector3(row * 200, -colum * 200, 1000);
            ((Button)ob.GetComponent<GunOb>().toggle).onClick.AddListener(()=> {
                var sys = MultiplayerEventSystem.current;
                Debug.Log(sys);
                sys.SetSelectedGameObject(resetButton);
                Debug.Log(sys);
                foreach(var i in weaponmenu)
                {
                    GameObject.Destroy(i.gameObject);
                }
                weaponmenu.Clear();
            });
            weaponmenu.Add(ob);
        }
        for (int i=0; i<weaponmenu.Count; i++)
        {
            var x = weaponmenu[i].gameObject.GetComponent<GunOb>().toggle.navigation;
            
            x.selectOnLeft = weaponmenu[Math.Abs((i+weaponmenu.Count-1)%(weaponmenu.Count))].gameObject.GetComponent<GunOb>().toggle;
            x.selectOnRight = weaponmenu[Math.Abs((i + 1) % (weaponmenu.Count))].gameObject.GetComponent<GunOb>().toggle;
            x.selectOnDown = weaponmenu[Math.Abs((i + rowsize) % (weaponmenu.Count))].gameObject.GetComponent<GunOb>().toggle;
            x.selectOnUp = weaponmenu[Math.Abs((i + weaponmenu.Count - rowsize) % (weaponmenu.Count))].gameObject.GetComponent<GunOb>().toggle;
            weaponmenu[i].gameObject.GetComponent<GunOb>().toggle.navigation = x;

        }

        var sys = MultiplayerEventSystem.current;
        Debug.Log(sys);
            sys.SetSelectedGameObject( canvas.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
        Debug.Log(sys);
    }
    
    public void NextMap(){
        mapIndex++;
        mapIndex=mapIndex%(maps.Count);
        LobbyManager.instance.map=maps[mapIndex].map;
        mapThumbnail.sprite=maps[mapIndex].mapImage;
        Debug.Log(maps[mapIndex]);
    } 
    public void PreviousMap(){
        mapIndex--;
        if(mapIndex<0){
            mapIndex=maps.Count-1;
        }
        LobbyManager.instance.map=maps[mapIndex].map;
        mapThumbnail.sprite=maps[mapIndex].mapImage;
    }
    //Map Logic End

    // Start is called before the first frame update
    void Start()
    {
        UpdateModeLabel();
        UpdateSecondaryModifierLabel();
    }

    // Update is called once per frame
    int modeCounter = 0;
    private DirectorBehaviour.Gamemode modeChange(int direction)
    {

        DirectorBehaviour.Gamemode[] modes = { DirectorBehaviour.Gamemode.DEATHMATCH, DirectorBehaviour.Gamemode.LASTMANSTANDING,DirectorBehaviour.Gamemode.GOLDENSPIRIT };
        int wantedModeNumber = (modeCounter + direction) % modes.Length;
 
        // If mode is moved down, under index zero, set it to highest value
        int modeCounterNumber = wantedModeNumber < 0 ? modes.Length - 1 : wantedModeNumber;
        modeCounter = modeCounterNumber;
        DirectorBehaviour.Gamemode mode = modes[modeCounter];
        DirectorBehaviour.gameMode = mode;
        if (DirectorBehaviour.gameMode == DirectorBehaviour.Gamemode.GOLDENSPIRIT)
        {
            DirectorBehaviour.gameTime = 30;
            TIMESCALE = 5;
        }else if (DirectorBehaviour.gameMode == DirectorBehaviour.Gamemode.DEATHMATCH) {
            DirectorBehaviour.gameTime = 120;
            TIMESCALE = 30;
        }
        // change the mode button text in the settings
        UpdateModeLabel();
        UpdateSecondaryModifierLabel();

        return mode;
    }

    public void MoveModeUp()
    {
        modeChange(1);
    }

    public void MoveModeDown()
    {
        modeChange(-1);
    }

    public void AddTime()
    {
        if (isGameModeDeathMatch()||isGameModeGoldenSpirit())
        {
            DirectorBehaviour.gameTime = DirectorBehaviour.gameTime + TIMESCALE;
        }
        else if (isGameModeLastManStanding())
        {
            DirectorBehaviour.MaxLives = DirectorBehaviour.MaxLives + 1;
        }
        UpdateSecondaryModifierLabel();
    }

    public void ReduceTime()
    {
        if ((DirectorBehaviour.gameTime - TIMESCALE) > 0)
        {
            DirectorBehaviour.gameTime = DirectorBehaviour.gameTime - TIMESCALE;
        }
        else if (isGameModeLastManStanding() && DirectorBehaviour.MaxLives >= 2)
        {
            DirectorBehaviour.MaxLives = DirectorBehaviour.MaxLives - 1;
        }
        UpdateSecondaryModifierLabel();
    }

    public void UpdateModeLabel()
    {
        // Not entirely sure why this requires a null check
        if (modeLabel != null)
        {
            if (isGameModeDeathMatch())
            {
                modeLabel.text = "Deathmatch";
            }
            else if (isGameModeLastManStanding())
            {
                modeLabel.text = "Last Man Standing";
            }
            else if (isGameModeGoldenSpirit())
            {
                modeLabel.text = "Golden Spirit";
            }
        }
    }

    private bool isGameModeGoldenSpirit()
    {
        return DirectorBehaviour.gameMode == DirectorBehaviour.Gamemode.GOLDENSPIRIT;
    }

    public void UpdateSecondaryModifierLabel()
    {
        if (SecondaryModifierLabel != null)
        {
            if (isGameModeDeathMatch())
            {
                SecondaryModifierHeaderText.text = "Time (S)";
                SecondaryModifierLabel.text = DirectorBehaviour.gameTime.ToString();

            }
            else if (isGameModeGoldenSpirit())
            {
                SecondaryModifierHeaderText.text = "Time (S)";
                SecondaryModifierLabel.text = DirectorBehaviour.gameTime.ToString();
            }
            else if (isGameModeLastManStanding())
            {
                SecondaryModifierHeaderText.text = "Starting Lives";
                SecondaryModifierLabel.text = DirectorBehaviour.MaxLives.ToString();
            }
        }
    }
    void Update()
    {

        if (Gamepad.current != null)
        {
            if (Gamepad.current.yButton.wasPressedThisFrame)
            {
                Debug.Log("Map change was requested.");
            }
            if (Gamepad.current.rightTrigger.wasPressedThisFrame)
            {
                //Debug.Log("mode: " + modeChange(1));
            }
        }
        if (Keyboard.current != null)
        {
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                Debug.Log("Map change was requested.");
            }
            if (Keyboard.current.mKey.wasPressedThisFrame)
            {
                //Debug.Log("mode: " + modeChange(1));
            }
        }
    }

    private bool isGameModeDeathMatch()
    {
        return DirectorBehaviour.gameMode == DirectorBehaviour.Gamemode.DEATHMATCH;
    }
    private bool isGameModeLastManStanding()
    {
        return DirectorBehaviour.gameMode == DirectorBehaviour.Gamemode.LASTMANSTANDING;
    }

}
