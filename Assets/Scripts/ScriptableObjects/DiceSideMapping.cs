using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DiceSideMapping", order = 1)]
public class DiceSideMapping : ScriptableObject
{
    [SerializeField] private TileType _side1Front;
    [SerializeField] private TileType _side2Top;
    [SerializeField] private TileType _side3Right;
    [SerializeField] private TileType _side4Left;
    [SerializeField] private TileType _side5Back;
    [SerializeField] private TileType _side6Bottom;

    public Dictionary<int, TileType> Sides
    {
        get {
            return new Dictionary<int, TileType>()
            {
                {1, _side1Front},
                {2, _side2Top},
                {3, _side3Right},
                {4, _side4Left},
                {5, _side5Back},
                {6, _side6Bottom},
            };
        }
    }
}
