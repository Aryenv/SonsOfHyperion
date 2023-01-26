using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardClick : MonoBehaviour
{
    DeckBuildManager deckManager;
    CardValues card;
    AudioSource audio;
    public AudioClip clickar;
    public GameObject cardSelected;
    public GameObject villagers;
    public GameObject gold;
    public GameObject fiber;
    public GameObject minerals;
    public GameObject berries;
    public GameObject outline;
    bool selected = true;

    void Start()
    {
        Debug.Log(clickar);
        outline.SetActive(false);
        cardSelected.SetActive(true);
        card = GetComponent<CardValues>();
        audio = GetComponent<AudioSource>();
        deckManager = FindObjectOfType<DeckBuildManager>();
        Payments();
    }

    public void ClickOnCard()
    {
        audio.PlayOneShot(clickar, 0.7f);
        if (deckManager.DeckCards.Count <= 9)
        {
            if (!deckManager.DeckCards.ContainsValue(this.gameObject.GetComponent<CardValues>().dataCard.cardName))
            {
                deckManager.DeckCards.Add(this.gameObject.GetComponent<CardValues>().dataCard, this.gameObject.GetComponent<CardValues>().dataCard.cardName);
                cardSelected.SetActive(false);
                this.gameObject.GetComponent<CardValues>().dataCard.isInDeck = true;
            }
            else
            {
                deckManager.DeckCards.Remove(this.gameObject.GetComponent<CardValues>().dataCard);
                cardSelected.SetActive(true);
                this.gameObject.GetComponent<CardValues>().dataCard.isInDeck = false;
            }
        }
        else if(deckManager.DeckCards.Count >=9)
        {
            deckManager.DeckCards.Remove(this.gameObject.GetComponent<CardValues>().dataCard);
            cardSelected.SetActive(true);
            this.gameObject.GetComponent<CardValues>().dataCard.isInDeck = false;
        }

    }

    public void ScaleUp()
    {
        this.gameObject.transform.localScale += new Vector3((float)0.2, (float)0.2,0);
        deckManager.cardDescription.text = this.gameObject.GetComponent<CardValues>().dataCard.description.ToString();
        deckManager.cardTitle.text = this.gameObject.GetComponent<CardValues>().dataCard.cardName.ToString();
    }

    public void ScaleDown()
    {
        this.gameObject.transform.localScale -= new Vector3((float)0.2, (float)0.2, 0);
    }

    public void ShowOutline()
    {
        outline.SetActive(true);
    }

    public void notShowOutline()
    {
        outline.SetActive(false);
    }

    public void Payments()
    {
        List<PayamentCost> cardCost = new List<PayamentCost>();
        if (card.dataCard.cardCost != null)
        {
            for (int i = 0; i < card.dataCard.cardCost.Length; i++)
            {
                cardCost.Add(card.dataCard.cardCost[i]);
            }
        }
        if (cardCost.Count > 0)
        {
            for (int i = 0; i < cardCost.Count; i++)
            {
                switch (cardCost[i].currencyType)
                {
                    case CurrencyType.minerals:
                        minerals.SetActive(true);
                        minerals.GetComponentInChildren<TextMeshProUGUI>().text = cardCost[i].cost.ToString();
                        Image img = fiber.GetComponentInChildren<Image>();
                        img.color = new Color(185, 185, 185, 50);
                        break;
                    case CurrencyType.fiber:
                        fiber.SetActive(true);
                        fiber.GetComponentInChildren<TextMeshProUGUI>().text = cardCost[i].cost.ToString();
                        Image img1 = fiber.GetComponentInChildren<Image>();
                        img1.color = new Color(185, 185, 185, 50);
                        break;
                    case CurrencyType.food:
                        berries.SetActive(true);
                        berries.GetComponentInChildren<TextMeshProUGUI>().text = cardCost[i].cost.ToString();
                        Image img2 = fiber.GetComponentInChildren<Image>();
                        img2.color = new Color(185, 185, 185, 50);
                        break;
                    case CurrencyType.currentVillagers:
                        villagers.SetActive(true);
                        villagers.GetComponentInChildren<TextMeshProUGUI>().text = cardCost[i].cost.ToString();
                        Image img3 = fiber.GetComponentInChildren<Image>();
                        img3.color = new Color(185, 185, 185, 50);
                        break;
                    case CurrencyType.gold:
                        gold.SetActive(true);
                        gold.GetComponentInChildren<TextMeshProUGUI>().text = cardCost[i].cost.ToString();
                        Image img4 = fiber.GetComponentInChildren<Image>();
                        img4.color = new Color(185, 185, 185, 50);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
