using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card             dataCard;
    public Construction     dataBuild;
    public DragRotatorInfo  draggInfo;

    public TextMeshProUGUI  nameText;
    public TextMeshProUGUI  descriptionText;
    public TextMeshProUGUI  cardTypeText;

    public GameObject berriesObj;
    public GameObject villagersObj;
    public GameObject fiberObj;
    public GameObject mineralsObj;
    public GameObject goldObj;

    public GameObject baseImage;

    public Image            cardImage;
    public UnitData         unitData;
    public GameObject       feedUseCard;

    public GameObject       cardOutline;

    [Space]
    public int                          ID;
    public string                       cardName;
    public string                       description;
    public CardType                     cardType;
    public ConstructionsType            constructionType;
    public PlacementType                myPlaceType;
    public List<PayamentCost>           cardCost;
    public string                       imageName;
    public int                          chaos;
    public bool                         adyacencia;
    public int                          adyacenciaDist;

    public int                          mediumChaos;
    public int                          maxChaos;


    // Start is called before the first frame update
    void Start()
    {
        SetCard();
        SetCardPrefab();
    }

    public void SetCard()
    {
        dataBuild           = dataCard.constructionScriptable;
        cardName            = dataCard.cardName;
        cardType            = dataCard.cardType;
        cardTypeText.text   = cardType.ToString();
        myPlaceType         = dataCard.placementType;
        description         = dataCard.description;
        constructionType    = dataCard.productionType;
        imageName           = dataCard.imageName;
        chaos               = dataCard.chaos;
        adyacencia          = dataCard.adyacencia;
        adyacenciaDist      = dataCard.adyacenciaDist;
        ID                  = dataCard.ID;

        feedUseCard         = dataCard.feedUseCard;
        if (dataCard.unit != null)
        {
            unitData = dataCard.unit;
        }

        if (dataCard.cardCost != null)
        {
            for (int i = 0; i < dataCard.cardCost.Length; i++)
            {
                cardCost.Add(dataCard.cardCost[i]);
            }
        }

        if (cardCost.Count > 0)
        {
            for (int i = 0; i < cardCost.Count; i++)
            {
                switch (cardCost[i].currencyType)
                {
                    case CurrencyType.minerals:
                        mineralsObj.SetActive(true);
                        mineralsObj.GetComponentInChildren<TextMeshProUGUI>().text = cardCost[i].cost.ToString();
                        break;
                    case CurrencyType.fiber:
                        fiberObj.SetActive(true);
                        fiberObj.GetComponentInChildren<TextMeshProUGUI>().text = cardCost[i].cost.ToString();
                        break;
                    case CurrencyType.food:
                        berriesObj.SetActive(true);
                        berriesObj.GetComponentInChildren<TextMeshProUGUI>().text = cardCost[i].cost.ToString();
                        break;
                    case CurrencyType.currentVillagers:
                        villagersObj.SetActive(true);
                        villagersObj.GetComponentInChildren<TextMeshProUGUI>().text = cardCost[i].cost.ToString();
                        break;
                    case CurrencyType.gold:
                        goldObj.SetActive(true);
                        goldObj.GetComponentInChildren<TextMeshProUGUI>().text = cardCost[i].cost.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

    }

    public void SetCardPrefab()
    {
        nameText.text           = cardName;
        descriptionText.text    = description;
        cardImage.sprite        = dataCard.image;


        //if (chaos < mediumChaos && chaos < maxChaos)
        //{
        //    chaosImage[0].gameObject.SetActive(true);
        //}

        //if (chaos >= mediumChaos && chaos < maxChaos)
        //{
        //    chaosImage[0].gameObject.SetActive(true);
        //    chaosImage[1].gameObject.SetActive(true);
        //}

        //if (chaos >= maxChaos)
        //{
        //    chaosImage[0].gameObject.SetActive(true);
        //    chaosImage[1].gameObject.SetActive(true);
        //    chaosImage[2].gameObject.SetActive(true);
        //}
    }
}
