using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    [SerializeField] private int _number;
    public int Number
    {
        get { return _number; }
    }
    public TileType Type { get; private set; }
    private Material _material;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetType(TileType type)
    {
        Type = type;
        _material = GameManager.Instance.CurrentLevel.TypeMats.Scheme[type];
        _renderer.material = _material;
    }

    private void OnTriggerEnter(Collider other)
    {
        Tile tile = other.GetComponent<TileTop>().Tile;
        TileType tileType = tile.Type;
        bool isMatch = tileType == Type;
        bool isNeutralSided = tileType == TileType.Neutral || Type == TileType.Neutral;

        // check for failure
        if (!isMatch && !isNeutralSided)
            Debug.Log("No Match! Fail!");
        // chack for success
        else if (isMatch && tile.IsEnd)
            Debug.Log("Success! Won Level!");
        // confirm safe
        else
            Debug.Log("Safe");
    }
}
