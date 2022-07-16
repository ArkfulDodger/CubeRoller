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

    // Landed on mismatched tile
    public event Action BadTileStep;
    public void BadTileStepHandler() { BadTileStep?.Invoke(); }

    // Matched on End Tile
    public event Action LevelCleared;
    public void LevelClearedHandler() { LevelCleared?.Invoke(); }


}
