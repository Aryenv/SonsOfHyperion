using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCard : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(2, 1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(3, 1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(4, 1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(5, 1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(6, 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(7, 1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(8, 1);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(9, 1);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(10, 1);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(11, 1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(12, 1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.CardManager.GiveCardAndDraw(14, 1);
        }
    }
}
