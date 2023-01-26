using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class AdyacencyStruct
{
    public AnalizeForms analiceMod;
    [Header("Los Tiles Requeridos")]
    public List<AdyacencyEnum> tilesRequirement;
    [Space]
    [Header("Tile que devuelve")]
    public AdyacencyEnum returnTile;
    public Directions    tilePos;

    public bool isComboConstruction;
}
