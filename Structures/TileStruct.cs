using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct TileStruct
{
    public CardType ListType;

    [Tooltip("Lista de tiles a elegir")]
    public List<AdyacencyEnum> Tiles;
    [Tooltip("Se puede poner en: ")]
    public List<AdyacencyEnum> PlaceOn;
    [Tooltip("Lista de tiles a los que no van a ir adyacentes")]
    public List<AdyacencyEnum> NotAdyacents;

    public bool isAdyacentType;
}
