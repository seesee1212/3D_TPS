using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]

public class PartData
{
    public string id; // key
    public GameObject prefab; // value
}
[CreateAssetMenu(fileName = "Scriptable", menuName = "DB", order = int.MaxValue)]
public class PartDB : ScriptableObject
{
    public List<PartData> parts;

    private Dictionary<string, GameObject> partMap;

    public GameObject Get(string id)
    {
        if (partMap == null)
        {
            partMap = parts.ToDictionary(p => p.id, p => p.prefab);
        }

        return partMap.ContainsKey(id) ? partMap[id] : null;
    }
}
