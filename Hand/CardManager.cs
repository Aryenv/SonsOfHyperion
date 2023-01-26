using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject               prefabCard;

    public List<Card>               tempCards;

    public List<GameObject>         cardsToDraw;

    public List<Card>               cardsPlayed;

    //public bool cardsDrawed;

    public int priority1Percent;
    public int priority2Percent;
    public int priority3Percent;
    public int priority4Percent;
    public int priority5Percent;

    private int percent1;
    private int percent2;
    private int percent3;
    private int percent4;
    private int percent5;

    private int maxPercent;

    private void Awake()
    {
        maxPercent = 100 /*priority1Percent + priority2Percent + priority3Percent + priority4Percent + priority5Percent*/;

        percent1 = 5;
        percent2 = 15;
        percent3 = 35;
        percent4 = 60;
        percent5 = 100;
    }

    public void AddCards(CardsStruct[] addedCards)
    {
        foreach (var cards in addedCards)
        {
            switch (cards.cardType)
            {
                case CardType.military:
                    AddMilitaryCards(cards.cardType, cards.amount);
                    break;
                case CardType.extractor:
                    AddExtractorCards(cards.cardType, cards.amount);
                    break;
                case CardType.terrain:
                    AddTerrainCards(cards.cardType, cards.amount);
                    break;
                case CardType.power:
                    AddPowerCards(cards.cardType, cards.amount);
                    break;
                case CardType.construction:
                    AddConstructionCards(cards.cardType, cards.amount);
                    break;
                default:
                    break;
            }
        }

        DrawListCards();
        ClearCardsListToDraw();
    }

    public void AddRandomCard(int amount)
    {
        ClearCardsListToDraw();
        for (int i = 0; i < amount;)
        {
            foreach (KeyValuePair<int, List<Card>> card in LoadCards.Instance.DictionaryPriorities)
            {
                int randomPriority = Random.Range(0, maxPercent);

                if (randomPriority <= percent1)
                {
                    if (card.Key == priority1Percent && card.Value.Count > 0)
                    {
                        int rnd = Random.Range(0, card.Value.Count);
                        Card tempScriptableObj = LoadCards.Instance.ReturnRandomCardPerPercent(priority1Percent, rnd);
                        GameObject cardTemp = Instantiate(prefabCard);
                        cardTemp.GetComponent<CardDisplay>().dataCard = tempScriptableObj;
                        tempScriptableObj.isPlayed = true;
                        cardsToDraw.Add(cardTemp);
                        if (cardsPlayed.Contains(tempScriptableObj))
                        {
                            cardsPlayed.Remove(tempScriptableObj);
                        }
                        cardsPlayed.Add(tempScriptableObj);
                        LoadCards.Instance.DecrementPriority(tempScriptableObj);
                        i++;
                        break;
                    }
                }
                if (randomPriority <= percent2 && randomPriority > percent1)
                {
                    if (card.Key == priority2Percent && card.Value.Count > 0)
                    {
                        int rnd = Random.Range(0, card.Value.Count);
                        Card tempScriptableObj = LoadCards.Instance.ReturnRandomCardPerPercent(priority2Percent, rnd);
                        GameObject cardTemp = Instantiate(prefabCard);
                        cardTemp.GetComponent<CardDisplay>().dataCard = tempScriptableObj;
                        tempScriptableObj.isPlayed = true;
                        cardsToDraw.Add(cardTemp);
                        if (cardsPlayed.Contains(tempScriptableObj))
                        {
                            cardsPlayed.Remove(tempScriptableObj);
                        }
                        cardsPlayed.Add(tempScriptableObj);
                        LoadCards.Instance.DecrementPriority(tempScriptableObj);
                        i++;
                        break;
                    }
                }
                if (randomPriority <= percent3 && randomPriority > percent2)
                {
                    if (card.Key == priority3Percent && card.Value.Count > 0)
                    {
                        int rnd = Random.Range(0, card.Value.Count);
                        Card tempScriptableObj = LoadCards.Instance.ReturnRandomCardPerPercent(priority3Percent, rnd);
                        GameObject cardTemp = Instantiate(prefabCard);
                        cardTemp.GetComponent<CardDisplay>().dataCard = tempScriptableObj;
                        tempScriptableObj.isPlayed = true;
                        cardsToDraw.Add(cardTemp);
                        if (cardsPlayed.Contains(tempScriptableObj))
                        {
                            cardsPlayed.Remove(tempScriptableObj);
                        }
                        cardsPlayed.Add(tempScriptableObj);
                        LoadCards.Instance.DecrementPriority(tempScriptableObj);
                        i++;
                        break;
                    }
                }
                if (randomPriority <= percent4 && randomPriority > percent3)
                {
                    if (card.Key == priority4Percent && card.Value.Count > 0)
                    {
                        int rnd = Random.Range(0, card.Value.Count);
                        Card tempScriptableObj = LoadCards.Instance.ReturnRandomCardPerPercent(priority4Percent, rnd);
                        GameObject cardTemp = Instantiate(prefabCard);
                        cardTemp.GetComponent<CardDisplay>().dataCard = tempScriptableObj;
                        tempScriptableObj.isPlayed = true;
                        cardsToDraw.Add(cardTemp);
                        if (cardsPlayed.Contains(tempScriptableObj))
                        {
                            cardsPlayed.Remove(tempScriptableObj);
                        }
                        cardsPlayed.Add(tempScriptableObj);
                        LoadCards.Instance.DecrementPriority(tempScriptableObj);
                        i++;
                        break;
                    }
                }
                if (randomPriority <= percent5 && randomPriority > percent4)
                {
                    if (card.Key == priority5Percent && card.Value.Count > 0)
                    {
                        int rnd = Random.Range(0, card.Value.Count);
                        Card tempScriptableObj = LoadCards.Instance.ReturnRandomCardPerPercent(priority5Percent, rnd);
                        GameObject cardTemp = Instantiate(prefabCard);
                        cardTemp.GetComponent<CardDisplay>().dataCard = tempScriptableObj;
                        tempScriptableObj.isPlayed = true;
                        cardsToDraw.Add(cardTemp);
                        if (cardsPlayed.Contains(tempScriptableObj))
                        {
                            cardsPlayed.Remove(tempScriptableObj);
                        }
                        cardsPlayed.Add(tempScriptableObj);
                        LoadCards.Instance.DecrementPriority(tempScriptableObj);
                        i++;
                        break;
                    }
                }
            }
        }

        if (cardsPlayed.Count != 0)
        {
            for (int i = 0; i < cardsPlayed.Count; i++)
            {
                if (cardsPlayed[i].isPlayed == false)
                {
                    LoadCards.Instance.IncrementPriority(cardsPlayed[i]);
                    if (cardsPlayed[i].currentPriority >= cardsPlayed[i].basePriority)
                    {
                        cardsPlayed.Remove(cardsPlayed[i]);
                    }
                }

                if (cardsPlayed[i].isPlayed)
                {
                    cardsPlayed[i].isPlayed = false;
                }
            }
        }

        DrawListCards();
    }


    #region Logic
    public void AddTerrainCards(CardType terrainType, int quanty)
    {
        AddCardsLogic(terrainType, quanty);
        DrawCards();
        ClearCardsListToDraw();
    }

    private void AddMilitaryCards(CardType militaryType, int quanty)
    {
        AddCardsLogic(militaryType, quanty);
    }

    private void AddConstructionCards(CardType constructionType, int quanty)
    {
        AddCardsLogic(constructionType, quanty);
    }

    private void AddExtractorCards(CardType extractorType, int quanty)
    {
        AddCardsLogic(extractorType, quanty);
    }

    private void AddPowerCards(CardType powerType, int quanty)
    {
        AddCardsLogic(powerType, quanty);
    }

    private void AddCardsLogic(CardType type, int quanty)
    {
        for (int i = 0; i < quanty; i++)
        {
            if (LoadCards.Instance.CardsDictionary[type] != null)
            {
                tempCards = LoadCards.Instance.CardsDictionary[type];

                int randomNumber = Random.Range(0, tempCards.Count);

                Card tempScriptableObj = tempCards[randomNumber];

                GameObject cardTemp = Instantiate(prefabCard);
                cardTemp.GetComponent<CardDisplay>().dataCard = tempScriptableObj;

                cardsToDraw.Add(cardTemp);
            }
        }
    }

    public void GiveCardAndDraw(int ID, int quanty)
    {
        if (LoadCards.Instance.allCardsDictionary.ContainsKey(ID))
        {
            Card tempScriptableObj = LoadCards.Instance.allCardsDictionary[ID];
            GameObject cardTemp = Instantiate(prefabCard);
            cardTemp.GetComponent<CardDisplay>().dataCard = tempScriptableObj;

            cardsToDraw.Add(cardTemp);
            DrawListCards();
            ClearCardsListToDraw();
        }
    }


    private void DrawCards()
    {
        GameManager.Instance.HandManager.DrawCards(cardsToDraw);
    }

    private void DrawListCards()
    {
        GameManager.Instance.HandManager.ClearHand();
        StartCoroutine(GameManager.Instance.HandManager.DrawCard(cardsToDraw));
        //GameManager.Instance.HandManager.DrawCards(cardsToDraw);
    }

    public void ClearCardsListToDraw()
    {
        cardsToDraw.Clear();
    }

    #endregion
}
