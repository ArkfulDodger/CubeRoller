using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TypeMaterials", order = 2)]
public class TypeMaterials : ScriptableObject
{
    [Serializable]
    public struct TypeMat
    {
        public TileType type;
        public Material material;
    }

    [SerializeField] private List<TypeMat> _typesToMaterials;
    private Dictionary<TileType, Material> _scheme;
    public Dictionary<TileType, Material> Scheme
    {
        get
        {
            if (_scheme == null)
                return GetScheme();
            else
                return _scheme;
        }
    }

    private Dictionary<TileType, Material> GetScheme()
    {
        _scheme = new Dictionary<TileType, Material>();

        foreach (TypeMat typeMat in _typesToMaterials)
            _scheme.Add(typeMat.type, typeMat.material);

        return _scheme;
    }
}
