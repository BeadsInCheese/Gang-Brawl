using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.InputSystem;
public class LevelSelector : MonoBehaviour
{
    public TMP_Text modeLabel;

    public TMP_Text SecondaryModifierHeaderText;
    public TMP_Text TimeLabel;

    private int TIMESCALE = 30;
    // Start is called before the first frame update
    void Start()
    {
        UpdateModeLabel();
        UpdateTimeLabel();
    }

    // Update is called once per frame
    int modeCounter = 0;
    private DirectorBehaviour.Gamemode modeChange(int direction)
    {
        DirectorBehaviour.Gamemode[] modes = { DirectorBehaviour.Gamemode.DEATHMATCH, DirectorBehaviour.Gamemode.LASTMANSTANDING };
        int wantedModeNumber = (modeCounter + direction) % modes.Length;
        // If mode is moved down, under index zero, set it to highest value
        int modeCounterNumber = wantedModeNumber < 0 ? modes.Length - 1 : wantedModeNumber;
        modeCounter = modeCounterNumber;
        DirectorBehaviour.Gamemode mode = modes[modeCounter];
        DirectorBehaviour.gameMode = mode;
        // change the mode button text in the settings
        UpdateModeLabel();
        UpdateTimeLabel();

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
        if (isGameModeDeathMatch())
        {
            DirectorBehaviour.gameTime = DirectorBehaviour.gameTime + TIMESCALE;
        }
        else if (isGameModeLastManStanding())
        {
            DirectorBehaviour.MaxLives = DirectorBehaviour.MaxLives + 1;
        }
        UpdateTimeLabel();
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
        UpdateTimeLabel();
    }

    public void UpdateModeLabel()
    {
        // Not entirely sure why this requires a null check
        if (modeLabel != null)
        {
            Debug.Log("ModeLabel was updated and was not null");
            if (isGameModeDeathMatch())
            {
                modeLabel.text = "Deathmatch";
            }
            else if (isGameModeLastManStanding())
            {
                modeLabel.text = "Last Man Standing";
            }
        }
    }

    public void UpdateTimeLabel()
    {
        if (TimeLabel != null)
        {
            if (isGameModeDeathMatch())
            {
                SecondaryModifierHeaderText.text = "Time (S)";
                TimeLabel.text = DirectorBehaviour.gameTime.ToString();

            }
            else if (isGameModeLastManStanding())
            {
                SecondaryModifierHeaderText.text = "Starting Lives";
                TimeLabel.text = DirectorBehaviour.MaxLives.ToString();
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
                Debug.Log("mode: " + modeChange(1));
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
                Debug.Log("mode: " + modeChange(1));
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
