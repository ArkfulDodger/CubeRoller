using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//public enum TileType
//{
//    Neutral,
//    Red,
//    Blue
//}

public class GameManager : MonoBehaviour
{
    public Level CurrentLevel { get; private set; }
    public Dice CurrentDice { get; private set; }
    [SerializeField] private TypeMaterials _defaultTypeMats;
    public TypeMaterials DefaultTypeMats { get { return _defaultTypeMats; } }

    // Audio Components
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private SFXController _sfxController;


    // Game Variables
    private int _levelHitsRemaining = 0;
    private int _levelNum;
    public int LevelNum { get { return _levelNum; } }

    public static GameManager Instance { get; private set; }

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

        GetCurrentLevel();
    }

    private void OnEnable()
    {
        EventManager.Instance.BadTileStep += OnBadTileStep;
        EventManager.Instance.LevelCleared += OnLevelCleared;
        EventManager.Instance.LevelLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        EventManager.Instance.BadTileStep -= OnBadTileStep;
        EventManager.Instance.LevelCleared -= OnLevelCleared;
        EventManager.Instance.LevelLoaded -= OnLevelLoaded;
    }


    private void Start()
    {
        GetAndSetDice();
    }

    private void GetCurrentLevel()
    {
        CurrentLevel = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
        _levelNum = SceneManager.GetActiveScene().buildIndex;
    }

    private void GetAndSetDice()
    {
        CurrentDice = GameObject.FindGameObjectWithTag("Dice").GetComponent<Dice>();
        CurrentDice.UpdateSideMapping();
    }

    private void OnLevelLoaded()
    {
        GetCurrentLevel();
        GetAndSetDice();
    }

    private void OnBadTileStep()
    {
        if (_levelHitsRemaining < 1)
            EventManager.Instance.LevelFailedHandler();
        else
            _levelHitsRemaining--;
    }

    private void OnLevelCleared()
    {
        Debug.Log("Level Cleared!");
    }
}
