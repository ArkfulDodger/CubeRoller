using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Dictionary<Vector2, Tile> Map { get; private set; }
    [SerializeField] private TypeMaterials _typeMaterials;
    public TypeMaterials TypeMats { get { return _typeMaterials; } }
    [SerializeField] private DiceSideMapping _diceSideMapping;
    public DiceSideMapping DiceMapping { get { return _diceSideMapping; } }

    private void Awake()
    {
        Map = new Dictionary<Vector2, Tile>();
        GenerateMap();
    }

    private void Start()
    {
        if (!_typeMaterials)
            _typeMaterials = GameManager.Instance.DefaultTypeMats;
    }

    private void GenerateMap()
    {
        Map.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Tile tile = child.GetComponent<Tile>();
            if (tile)
                Map.Add(new Vector2(child.position.x, child.position.z), tile);
        }
    }

    public bool IsTileAtCoordinates(Vector2 coordinates)
    {
        return Map.ContainsKey(coordinates);
    }
}
