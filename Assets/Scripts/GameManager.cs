using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private void Start()
    {
        GetAndSetDice();
    }

    private void GetCurrentLevel()
    {
        CurrentLevel = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
    }

    private void GetAndSetDice()
    {
        CurrentDice = GameObject.FindGameObjectWithTag("Dice").GetComponent<Dice>();
        CurrentDice.UpdateSideMapping();
    }
}
