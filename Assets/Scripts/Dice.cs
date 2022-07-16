using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private DiceSide _side1Front;
    [SerializeField] private DiceSide _side2Top;
    [SerializeField] private DiceSide _side3Right;
    [SerializeField] private DiceSide _side4Left;
    [SerializeField] private DiceSide _side5Back;
    [SerializeField] private DiceSide _side6Bottom;

    private Dictionary<int, TileType> _currentSides;

    public void UpdateSideMapping()
    {
        _currentSides = GameManager.Instance.CurrentLevel.DiceMapping.Sides;
        _side1Front.SetType(_currentSides[1]);
        _side2Top.SetType(_currentSides[2]);
        _side3Right.SetType(_currentSides[3]);
        _side4Left.SetType(_currentSides[4]);
        _side5Back.SetType(_currentSides[5]);
        _side6Bottom.SetType(_currentSides[6]);
    }
}
