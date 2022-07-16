using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTop : MonoBehaviour
{
    private Material _material;
    private Renderer _renderer;
    private Tile _tile;
    public Tile Tile { get { return _tile; } }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _tile = transform.parent.GetComponent<Tile>();
    }

    private void Start()
    {
        _material = GameManager.Instance.CurrentLevel.TypeMats.Scheme[_tile.Type];
        _renderer.material = _material;
    }
}
