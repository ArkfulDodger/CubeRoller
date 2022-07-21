using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Level is Loading
    public event Action LevelLoading;
    public void LevelLoadingHandler() { LevelLoading?.Invoke(); }

    // Level is Loaded
    public event Action LevelLoaded;
    public void LevelLoadedHandler() { LevelLoaded?.Invoke(); }

    // Level is Reloaded
    public event Action LevelReloaded;
    public void LevelReloadedHandler() { LevelReloaded?.Invoke(); }
    
    // Landed on mismatched tile
    public event Action BadTileStep;
    public void BadTileStepHandler() { BadTileStep?.Invoke(); }

    // Matched on End Tile
    public event Action LevelCleared;
    public void LevelClearedHandler() { LevelCleared?.Invoke(); }

    // Lost Level
    public event Action LevelFailed;
    public void LevelFailedHandler() { LevelFailed?.Invoke(); }

    // Lost Level
    public event Action DiceClacked;
    public void DiceClackedHandler() { DiceClacked?.Invoke(); }

    // Lost Level
    public event Action Jumped;
    public void JumpedHandler() { Jumped?.Invoke(); }
    
    // Lost Level
    public event Action Flipped;
    public void FlippedHandler() { Flipped?.Invoke(); }
    
    // Lost Level
    public event Action RandomRolled;
    public void RandomRolledHandler() { RandomRolled?.Invoke(); }
        
    // Lost Level
    public event Action StartingGame;
    public void StartingGameHandler() { StartingGame?.Invoke(); }     
    
    // Lost Level
    public event Action EnteringNeutralTile;
    public void EnteringNeutralTileHandler() { EnteringNeutralTile?.Invoke(); }      
    
    // Lost Level
    public event Action EnteringColorTile;
    public void EnteringColorTileHandler() { EnteringColorTile?.Invoke(); }

}
