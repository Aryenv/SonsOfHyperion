using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawCellInfo : MonoBehaviour
{
    private GameManager gm;
    public TextMeshProUGUI m_name;
    public TextMeshProUGUI description;

    private int saveI = 0;

    void Start()
    {
        Set();
    }

    void Update()
    {
        SetInfo();
    }

    private void Set()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void SetInfo()
    {
        int value      = gm.Grid.GetValue(gm.GetGriddCell());
        int vCardInfo = 0;
        bool isOnCard = false;

        for (int i = 0; i < gm.cardsInHand.Count; i++)
        {
            if (gm.cardsInHand[i].GetComponent<Dragg>().isMouseInCard)
            {
                isOnCard  = true;
                vCardInfo = gm.cardsInHand[i].GetComponent<CardDisplay>().ID;
                break;
            }
        }
        if (!isOnCard)
        {
            if (value != saveI)
            {
                if (gm.TileListManager.dictionaryIdEnum.ContainsKey(value))
                {
                    string name = gm.TileListManager.dictionaryIdEnum[value].ToString().ToUpper();
                    this.m_name.text = name;
                }
                if (LoadCards.Instance.allCardsDictionary.ContainsKey(value))
                {
                    string description = LoadCards.Instance.allCardsDictionary[value].description;
                    this.description.text = description;
                }
            }
            if (value == 0)
            {
                this.description.text = null;
                this.m_name.text = null;

            }
        }
        else
        {
            string name = LoadCards.Instance.allCardsDictionary[vCardInfo].name;
            this.m_name.text = name;
            string description = LoadCards.Instance.allCardsDictionary[vCardInfo].description;
            this.description.text = description;
        }
        saveI = value;
    }
}
