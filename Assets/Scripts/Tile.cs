using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileType _type;
    public TileType Type { get { return _type; } }
    [SerializeField] private bool _isStart;
    [SerializeField] private bool _isEnd;
    [SerializeField] private GameObject _goalIndicator;
    public bool IsEnd { get { return _isEnd; } }

    private void Awake()
    {
        if (_isEnd)
            _goalIndicator.SetActive(true);
        else
            _goalIndicator.SetActive(false);
    }
}
