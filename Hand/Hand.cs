using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    private int m_maxCardsInHand;
    private int m_currentCardsInHand;

    private List<GameObject> cardsInHand;

    private GameObject cardSelected = new GameObject("Null");

    private Transform handZone;
    private Transform graveryard;
    private Transform canvas;

    public Hand(Transform HandZone, Transform Graveyard, Transform canvass, int maxCards, List<GameObject> cardsHand, int currentCardsInHand)
    {
        this.handZone = HandZone;
        this.graveryard = Graveyard;
        this.canvas = canvass;
        this.m_maxCardsInHand = maxCards;
        this.cardsInHand = cardsHand;
        this.m_currentCardsInHand = currentCardsInHand;
    }

    #region Base Methods

    /// <summary>
    /// Función para robar cartas
    /// </summary>
    /// <param name="cards"></param>
    public void DrawCards(List<GameObject> cards)
    {
        Draw(cards);
    }

    /// <summary>
    /// Selecciona la carta que se le pase por parámetro
    /// </summary>
    /// <param name="card"></param>
    public void SelectCard(GameObject card)
    {
        cardSelected = card;
        ReturnCardSelected();
        MoveCardToCanvas(card);
    }

    /// <summary>
    /// Devuelve el valor del efecto
    /// </summary>
    /// <returns></returns>
    public int ReturnCardID()
    {
        if (cardSelected != null)
        {
            CardDisplay tempCard = cardSelected.GetComponent<CardDisplay>();
            int idCard = tempCard.ID;
            return idCard;
        }
        else
        {
            return 1;
        }
    }

    /// <summary>
    /// Descarta la carta seleccionada
    /// </summary>
    public void DiscardSelectedCard()
    {
        RemoveCardInHand(cardSelected);
        DeselectCurrentCard();
    }

    /// <summary>
    /// Deselecciona la carta y la devuelve a la mano
    /// </summary>
    public void DeselectCardAndReturnToHand()
    {
        MoveCardToHand(cardSelected);
        cardSelected = null;
    }

    public GameObject ReturnCardSelected()
    {
        return cardSelected;
    }

    #endregion

    #region Internal Logic

    /// <summary>
    /// Recorre un array de cartas y las agrega a la mano
    /// </summary>
    /// <param name="cards"></param>
    private void Draw(List<GameObject> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            //ClearHand();

            AddCardInHand(cards[i]);
        }

    }

    public IEnumerator DrawCard(List<GameObject> cards)
    {
        int count = cards.Count;

        while (count > 0)
        {
            AddCardInHand(cards[0]);
            cards.Remove(cards[0]);
            count--;
            yield return new WaitForSeconds(.3f);
        }
    }

    /// <summary>
    /// Descarta una carta de la mano si al robar el número de cartas en manos el mayor que el máximo
    /// </summary>
    public void ClearHand()
    {
        //if (m_currentCardsInHand >= m_maxCardsInHand)
        //{
        //    RemoveCardInHand(cardsInHand[0]);
        //}
        int count = cardsInHand.Count;

        for (int i = 0; i < count; i++)
        {
            if (cardsInHand != null)
            {
                RemoveCardInHand(cardsInHand[0]);
            }
        }
    }

    /// <summary>
    /// Añade una carta a la mano y la instancia
    /// </summary>
    /// <param name="card"></param>
    private void AddCardInHand(GameObject card)
    {
        cardsInHand.Add(card);
        MoveCardToHand(card);
        m_currentCardsInHand++;
    }

    /// <summary>
    /// Quita una carta de la mano
    /// </summary>
    /// <param name="card"></param>
    public void RemoveCardInHand(GameObject card)
    {
        cardsInHand.Remove(card);
        DiscardHandCard(card);
        m_currentCardsInHand--;
    }

    /// <summary>
    /// Desactiva una carta
    /// </summary>
    /// <param name="card"></param>
    private void DiscardHandCard(GameObject card)
    {
        MoveCardToGraveryard(card);
        card.SetActive(false);
    }

    /// <summary>
    /// Deselecciona la carta
    /// </summary>
    private void DeselectCurrentCard()
    {
        if (cardSelected != null)
        {
            cardSelected = null;
        }
    }

    /// <summary>
    /// Setea el parent al canvas
    /// </summary>
    /// <param name="card"></param>
    private void MoveCardToCanvas(GameObject card)
    {
        card.transform.SetParent(canvas);
    }

    /// <summary>
    /// Mueve la carta al cementerio
    /// </summary>
    /// <param name="card"></param>
    private void MoveCardToGraveryard(GameObject card)
    {
        card.transform.position = graveryard.position;
        card.transform.SetParent(graveryard);
    }

    /// <summary>
    /// Setea el parent a la mano
    /// </summary>
    /// <param name="card"></param>
    private void MoveCardToHand(GameObject card)
    {
        card.transform.SetParent(handZone, false);
    }

    #endregion
}
