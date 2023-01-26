using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DeckBuildManager : MonoBehaviour
{
    public Dictionary<CardType, Card> AllCards = new Dictionary<CardType, Card>();
    public Dictionary<Card, string> DeckCards = new Dictionary<Card, string>();

    public CardClick cartaDeck;

    public Card[] cards;

    public List<CardTypeStruct> cartasStruc;

    public GameObject CardPrefab;
    public GameObject SpawnCard;
    public GameObject SpawnCard2;
    public GameObject SpawnCard3;
    public GameObject playButton;

    public TextMeshProUGUI cardTitle;
    public TextMeshProUGUI cardDescription;
    public TextMeshProUGUI deckCardsText;
    public int cardTextNumber;
    public int contador = 0;

    private void Start()
    {
        playButton.GetComponent<Button>().interactable = false;
        LoadCards();
        LoadCardTypes();
        AddCardToList();
    }

    private void Update()
    {
        CheckDeckCards();
        DoDeckText();
        showPlayButton();
    }

    private void showPlayButton()
    {
        if (DeckCards.Count == 10)
        {
            playButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            playButton.GetComponent<Button>().interactable = false;
        }
    }

    private void LoadCards()
    {
        cards = Resources.LoadAll<Card>("_Cards");
        for (int i = 0; i < cards.Length; i++)
        {
            if (!AllCards.ContainsKey(cards[i].cardType))
            {            
                AllCards.Add(cards[i].cardType, cards[i]);
                cards[i].isInDeck = false;
                cards[i].currentPriority = cards[i].basePriority;
            }
        }
    }

    void LoadCardTypes()
    {
        for (int i = 0; i < Enum.GetValues(typeof(CardType)).Length; i++)
        {
            cartasStruc.Add(new CardTypeStruct((CardType)i));
        }
    } 

    void AddCardToList()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].IsUnlocked)
            {
                CheckCoincidence(cards[i]);
            }
        }
    }

    void CheckCoincidence(Card card)
    {
        for (int i = 0; i < cartasStruc.Count; i++)
        {
            if (cartasStruc[i].ListType == card.cardType)
            {
                contador++;
                cartasStruc[i].tipoCarta.Add(card);
                SpawnCards(card);
            }
        }
    }


    private void SpawnCards(Card card)
    {
        if (contador <= 5)
        {
            GameObject go = Instantiate(CardPrefab, SpawnCard.transform.position, transform.rotation);
            card.isInDeck = false;
            card.currentPriority = card.basePriority;
            go.GetComponent<CardValues>().dataCard = card;
            go.transform.SetParent(SpawnCard.transform, false);
            go.transform.GetChild(3).GetComponent<Image>().sprite = card.image;
            //go.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = card.cardType.ToString();
            go.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = card.cardName;
        }
        else if (contador <= 10)
        {
            GameObject go = Instantiate(CardPrefab, SpawnCard2.transform.position, transform.rotation);
            card.isInDeck = false;
            card.currentPriority = card.basePriority;
            go.GetComponent<CardValues>().dataCard = card;
            go.transform.SetParent(SpawnCard2.transform, false);
            go.transform.GetChild(3).GetComponent<Image>().sprite = card.image;
            //go.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = card.cardType.ToString();
            go.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = card.cardName;
        }
        else if (contador <= 15)
        {
            GameObject go = Instantiate(CardPrefab, SpawnCard3.transform.position, transform.rotation);
            card.isInDeck = false;
            card.currentPriority = card.basePriority;
            go.GetComponent<CardValues>().dataCard = card;
            go.transform.SetParent(SpawnCard3.transform, false);
            go.transform.GetChild(3).GetComponent<Image>().sprite = card.image;
            //go.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = card.cardType.ToString();
            go.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = card.cardName;
        }       
    }

    void CheckDeckCards()
    {
        cardTextNumber = DeckCards.Count;
    }
    public void DoDeckText()
    {
        deckCardsText.text = cardTextNumber.ToString();
    }
}
