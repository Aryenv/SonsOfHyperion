using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStructuresManager : MonoBehaviour
{
    [ArrayElementTitle("cardType")]
    public CardsStruct[] startCards;

    [ArrayElementTitle("cardType")]
    public CardsStruct[] cycleTymerCards;

    [ArrayElementTitle("cardType")]
    public CardsStruct[] resourceCards;

    public void DrawStartCards()
    {
        GameManager.Instance.CardManager.AddCards(startCards);
    }

    public void DrawCycleTimerCards()
    {
        GameManager.Instance.CardManager.AddCards(cycleTymerCards);
    }

    public void DrawResourceCard()
    {
        GameManager.Instance.CardManager.AddCards(resourceCards);
    }
}
