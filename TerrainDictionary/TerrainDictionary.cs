using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDictionary : MonoBehaviour
{
    public List<ConstructionDisplay> terrains;

    public Dictionary<Vector2, Construction> terrainsDictionary = new Dictionary<Vector2, Construction>();

    public void AddTerrain(Construction terrainConstruction, Vector2 node)
    {
        terrainsDictionary.Add(node, terrainConstruction);
    }

    public Construction ReturnTerrain(int ID)
    {
        foreach (KeyValuePair<Vector2, Construction> terrain in terrainsDictionary)
        {
            if (terrain.Value.ID == ID)
            {
                return terrain.Value;
            }
        }
        return null;
    }

    public Vector2 ReturnTerrainPosition(int ID)
    {
        foreach (KeyValuePair<Vector2, Construction> terrain in terrainsDictionary)
        {
            if (terrain.Value.ID == ID)
            {
                return terrain.Key;
            }
        }
        return new Vector2(0, 0);
    }
}
