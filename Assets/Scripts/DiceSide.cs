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
        _renderer.material = GameManager.Instance.CurrentLevel.TypeMats.Scheme[type];
    }
}
