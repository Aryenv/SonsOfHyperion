using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCards : PersistentSingleton<LoadCards>
{
    [SerializeField]
    public List<Card>  allUnlockedCards;

    public List<Card> allCardsInfo;

    public List<Card> Priority1 = new List<Card>();
    public List<Card> Priority2 = new List<Card>();
    public List<Card> Priority3 = new List<Card>();
    public List<Card> Priority4 = new List<Card>();
    public List<Card> Priority5 = new List<Card>();

    [SerializeField]
    private List<Card>  extractorCards;
    [SerializeField]
    private List<Card>  terrainCards;
    [SerializeField]
    private List<Card>  buildingCards;
    [SerializeField]
    private List<Card>  militaryCards;
    [SerializeField]
    private List<Card>  powerCards;

    private Card[]          cards;
    private Construction[]  constructions;

    public Dictionary<CardType, List<Card>> CardsDictionary = new Dictionary<CardType, List<Card>>();
    public Dictionary<int, Card> allCardsDictionary = new Dictionary<int, Card>();

    public Dictionary<int, List<Card>> DictionaryPriorities = new Dictionary<int, List<Card>>();

    [SerializeField]
    private List<Construction> constructionsList;

    public Dictionary<int, Construction> constructionsDictionary = new Dictionary<int, Construction>();


    public override void Awake()
    {
        SetUpLoadCards();
        base.Awake();
    }

    #region Methods
    private void LoadAllCards()
    {
        LoadCardsInArray();
    }

    private void AddCardsToLists()
    {
        LoadCardsInList();
    }

    private void LoadDictionaries()
    {
        LoadDictionary();
    }
    #endregion

    #region Load Method Logic

    /// <summary>
    /// Carga un array con las cartas de la carpeta
    /// </summary>
    private void LoadCardsInArray()
    {
        cards = Resources.LoadAll<Card>("_Cards");
        for (int i = 0; i < cards.Length; i++)
        {
            allCardsInfo.Add(cards[i]);
            if (cards[i].IsUnlocked && cards[i].isInDeck)
            {
                allUnlockedCards.Add(cards[i]);
            }
        }

        constructions = Resources.LoadAll<Construction>("_Buildings");

        for (int i = 0; i < constructions.Length; i++)
        {
            constructionsList.Add(constructions[i]);
        }
    }

    private void SetPriorityDictionary()
    {
        for (int i = 0; i < allUnlockedCards.Count; i++)
        {
            switch (allUnlockedCards[i].basePriority)
            {
                case 1:
                    allUnlockedCards[i].currentPriority = allUnlockedCards[i].basePriority;
                    Priority1.Add(allUnlockedCards[i]);
                    break;
                case 2:
                    allUnlockedCards[i].currentPriority = allUnlockedCards[i].basePriority;
                    Priority2.Add(allUnlockedCards[i]);
                    break;
                case 3:
                    allUnlockedCards[i].currentPriority = allUnlockedCards[i].basePriority;
                    Priority3.Add(allUnlockedCards[i]);
                    break;
                case 4:
                    allUnlockedCards[i].currentPriority = allUnlockedCards[i].basePriority;
                    Priority4.Add(allUnlockedCards[i]);
                    break;
                case 5:
                    allUnlockedCards[i].currentPriority = allUnlockedCards[i].basePriority;
                    Priority5.Add(allUnlockedCards[i]);
                    break;
                default:
                    break;
            }
        }
    }

    public void IncrementPriority(Card card)
    {
        if (card.currentPriority < card.basePriority)
        {
            card.currentPriority++;
            switch (card.currentPriority)
            {
                case 2:
                    if (DictionaryPriorities[1].Contains(card))
                    {
                        DictionaryPriorities[1].Remove(card);
                    }
                    if (!DictionaryPriorities[2].Contains(card))
                    {
                        DictionaryPriorities[2].Add(card);
                    }
                    break;
                case 3:
                    if (DictionaryPriorities[2].Contains(card))
                    {
                        DictionaryPriorities[2].Remove(card);
                    }
                    if (!DictionaryPriorities[3].Contains(card))
                    {
                        DictionaryPriorities[3].Add(card);
                    }
                    break;
                case 4:
                    if (DictionaryPriorities[3].Contains(card))
                    {
                        DictionaryPriorities[3].Remove(card);
                    }
                    if (!DictionaryPriorities[4].Contains(card))
                    {
                        DictionaryPriorities[4].Add(card);
                    }
                    break;
                case 5:
                    if (DictionaryPriorities[4].Contains(card))
                    {
                        DictionaryPriorities[4].Remove(card);
                    }
                    if (!DictionaryPriorities[5].Contains(card))
                    {
                        DictionaryPriorities[5].Add(card);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void DecrementPriority(Card card)
    {
        if (card.currentPriority > 0)
        {
            card.currentPriority--;
            switch (card.currentPriority)
            {
                case 1:
                    if (DictionaryPriorities[2].Contains(card))
                    {
                        DictionaryPriorities[2].Remove(card);
                    }
                    if (!DictionaryPriorities[1].Contains(card))
                    {
                        DictionaryPriorities[1].Add(card);
                    }
                    break;
                case 2:
                    if (DictionaryPriorities[3].Contains(card))
                    {
                        DictionaryPriorities[3].Remove(card);
                    }
                    if (!DictionaryPriorities[2].Contains(card))
                    {
                        DictionaryPriorities[2].Add(card);
                    }
                    break;
                case 3:
                    if (DictionaryPriorities[4].Contains(card))
                    {
                        DictionaryPriorities[4].Remove(card);
                    }
                    if (!DictionaryPriorities[3].Contains(card))
                    {
                        DictionaryPriorities[3].Add(card);
                    }
                    break;
                case 4:
                    if (DictionaryPriorities[5].Contains(card))
                    {
                        DictionaryPriorities[5].Remove(card);
                    }
                    if (!DictionaryPriorities[4].Contains(card))
                    {
                        DictionaryPriorities[4].Add(card);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Carga la lista de cartas
    /// </summary>
    private void LoadCardsInList()
    {
        for (int i = 0; i < allUnlockedCards.Count; i++)
        {
            switch (allUnlockedCards[i].cardType)
            {
                case CardType.military:
                    militaryCards.Add(allUnlockedCards[i]);
                    break;
                case CardType.extractor:
                    extractorCards.Add(allUnlockedCards[i]);
                    break;
                case CardType.terrain:
                    terrainCards.Add(allUnlockedCards[i]);
                    break;
                case CardType.power:
                    powerCards.Add(allUnlockedCards[i]);
                    break;
                case CardType.construction:
                    buildingCards.Add(allUnlockedCards[i]);
                    break;
                default:
                    break;
            }
        }

        SetPriorityDictionary();
    }

    /// <summary>
    /// Carga todos los diccionarios
    /// </summary>
    private void LoadDictionary()
    {
        CardsDictionary.Add(CardType.extractor, extractorCards);
        CardsDictionary.Add(CardType.terrain, terrainCards);
        CardsDictionary.Add(CardType.construction, buildingCards);
        CardsDictionary.Add(CardType.military, militaryCards);
        CardsDictionary.Add(CardType.power, powerCards);

        DictionaryPriorities.Add(1, Priority1);
        DictionaryPriorities.Add(2, Priority2);
        DictionaryPriorities.Add(3, Priority3);
        DictionaryPriorities.Add(4, Priority4);
        DictionaryPriorities.Add(5, Priority5);

        for (int i = 0; i < allCardsInfo.Count; i++)
        {
            allCardsDictionary.Add(allCardsInfo[i].ID, allCardsInfo[i]);
        }

        for (int i = 0; i < constructionsList.Count; i++)
        {
            constructionsDictionary.Add(constructions[i].ID, constructions[i]);
        }
    }
    public Card ReturnRandomCardPerPercent(int percent, int number)
    {
        List<Card> cards = new List<Card>();
        DictionaryPriorities.TryGetValue(percent, out cards);
        return cards[number];
    }

    /// <summary>
    /// Carga todo
    /// </summary>
    public void SetUpLoadCards()
    {
        LoadAllCards();
        AddCardsToLists();
        LoadDictionaries();
    }

    #endregion
}
