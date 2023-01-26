using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dragg : EventTrigger, IPointerEnterHandler, IPointerExitHandler
{
    private CardDisplay         card;
    private DraggRotator        rotator;

    private bool                startDragging;
    public bool                 isInteractable;

    public RectTransform        rt;
    private Vector3             originalScale;
    private Vector3             maxScale = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector3             minScale = new Vector3(0.5f, 0.5f, 0.5f);
    private float               speed = 2f;
    private float               duration = 2f;
    public bool                 isMouseInCard;


    private void Update()
    {
        if (startDragging)
        {
            MovingCard();
        }

        if (CurrencyProduction.Instance.CanPayAllCosts(card.cardCost))
        {
            card.baseImage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            card.baseImage.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
        }
    }

    private void Start()
    {
        StartCard();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (isInteractable)
        {
            SelectCard();
            startDragging = true;
            isMouseInCard = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ReturnCardToHand();
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        // Ampliar
        if (card.cardCost.Count > 0)
        {
            if (CurrencyProduction.Instance.CanPayAllCosts(card.cardCost))
            {
                if (!startDragging)
                {
                    card.cardOutline.SetActive(true);
                    SoundManager.Instance.PlaySelectCard();
                    transform.localScale = Vector3.Lerp(originalScale, maxScale, duration);
                    isMouseInCard = true;
                    isInteractable = true;
                }
            }
        }
        else
        {
            if (!startDragging)
            {
                card.cardOutline.SetActive(true);
                SoundManager.Instance.PlaySelectCard();
                transform.localScale = Vector3.Lerp(originalScale, maxScale, duration);
                isMouseInCard = true;
                isInteractable = true;
            }
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        // Hacer chikito
        if (!startDragging)
        {
            card.cardOutline.SetActive(false);
            transform.localScale = Vector3.Lerp(maxScale, originalScale, duration);
            isMouseInCard = false;
            isInteractable = false;
        }
    }


    void SelectCard()
    {
        GameManager.Instance.HandManager.SelectCard(this.gameObject);
        transform.localScale = minScale;
    }

    public void MovingCard()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 wantedPos = new Vector3(mousePos.x, mousePos.y, 100f);
        transform.position = Camera.main.ScreenToWorldPoint(wantedPos);
        //transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        rotator.UpdateRotator();
    }

    public void ReturnCardToHand()
    {
        startDragging = false;
        isMouseInCard = false;
        transform.localScale = originalScale;
        rotator.Reset();
        GameManager.Instance.HandManager.DeselectCardAndReturnToHand();
    }

    public void DiscardCard()
    {
        startDragging = false;
        isMouseInCard = false;
        GameManager.Instance.HandManager.DiscardSelectedCard();
    }

    void StartCard()
    {
        rotator         = GetComponent<DraggRotator>();
        card            = GetComponent<CardDisplay>();

        rotator.SetInfo(card.draggInfo);
        isInteractable = false;
        isMouseInCard = false;
        rt = GetComponent<RectTransform>();
        rt.localScale = new Vector3(1, 1, 1);
        originalScale = rt.localScale;
        //card.transform.localScale = Vector3.Lerp(maxScale, originalScale, duration);
    }
}
