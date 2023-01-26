using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardTypeStruct
{
    public CardType ListType;
    [Tooltip("Lista de tipos de carta")]
    public List<Card> tipoCarta;

    public CardTypeStruct(CardType ListType)
    {
        this.ListType = ListType;
        this.tipoCarta = new List<Card>();
    }
}