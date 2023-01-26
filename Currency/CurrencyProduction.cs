using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyProduction : PersistentSingleton<CurrencyProduction>
{
    [ArrayElementTitle("currencyType")]
    public List<Currency> actualCurrency;

    [ArrayElementTitle("currencyType")]
    [SerializeField]
    private List<Currency> currencyProtuct;

    //Set
    public void AddCurrency(CurrencyType currencyType, int quanty)
    {
        for (int i = 0; i < actualCurrency.Count; i++)
        {
            if (actualCurrency[i].currencyType == currencyType)
            {
                if (quanty > 0)
                {
                    if (currencyType != CurrencyType.currentVillagers)
                    {
                        SoundManager.Instance.PlayResourceObtain();
                    }
                }

                actualCurrency[i].amount += quanty;
                if (actualCurrency[i].amount < 0)
                {
                    actualCurrency[i].amount = 0;
                }
                GameManager.Instance.UIManager.UpdateResourcesText();
                break;
            }
        }
    }
    public void AddToProduction(CurrencyType currencyType, int quanty)
    {
        for (int i = 0; i < currencyProtuct.Count; i++)
        {
            if (currencyProtuct[i].currencyType == currencyType)
            {
                currencyProtuct[i].amount += quanty;
                break;
            }
        }
    }
    //Get
    public int GetCurrency(CurrencyType currencyType)
    {
        int c = 0;

        for (int i = 0; i < actualCurrency.Count; i++)
        {
            if (actualCurrency[i].currencyType == currencyType)
            {
                c = actualCurrency[i].amount;
                break;
            }
        }
        return c;
    }
    public int GetCurrencyProduction(CurrencyType currencyType)
    {
        int c = 0;

        for (int i = 0; i < currencyProtuct.Count; i++)
        {
            if (currencyProtuct[i].currencyType == currencyType)
            {
                c = currencyProtuct[i].amount;
                break;
            }
            else
            {
                Debug.Log("Currency no encontrado");
            }
        }
        return c;
    }
    //Produccion
    public void ProductionCurrency()
    {
        for (int i = 0; i < actualCurrency.Count; i++)
        {
            if (actualCurrency[i].currencyType == currencyProtuct[i].currencyType)
            {
                actualCurrency[i].amount += currencyProtuct[i].amount;
                GameManager.Instance.UIManager.UpdateResourcesText();
            }
        }
    }

    public void RestCardCostToCurrency(List<PayamentCost> costs)
    {
        if (costs != null)
        {
            for (int i = 0; i < costs.Count; i++)
            {
                if (GetCurrency(costs[i].currencyType) > 0)
                {
                    AddCurrency(costs[i].currencyType, -costs[i].cost);
                }
            }
        }
    }

    public bool CanPayAllCosts(List<PayamentCost> costs)
    {
        bool canPay = false;
        
        for (int i = 0; i < costs.Count; i++)
        {
            if (!CanPayCost(costs[i]))
            {
                canPay = false;
                break;
            }else
            {
                if (i >= costs.Count - 1)
                {
                    canPay = true;
                }
            }
        }

        return canPay;
    }

    public bool CanPayCost(PayamentCost cost)
    {
        bool canPay;
        if (GetCurrency(cost.currencyType) < cost.cost)
        {
            canPay = false;
        }
        else
        {
            canPay = true;
        }
        return canPay;
    }

    //public void     RefreshCurrency         ()
    //{
    //    for (int i = 0; i < actualCurrency.Count; i++)
    //    {
    //        if (actualCurrency[i].currencyType == CurrencyType.microStone)
    //        {
    //            if (actualCurrency[i].amount >= 10)
    //            {
    //                Debug.Log("Micro piedra");
    //                AddCurrency(CurrencyType.stone,1);
    //                actualCurrency[i].amount -= 10;
    //                GameManager.Instance.UIManager.UpdateResourcesText();
    //            }
    //        }
    //        if (actualCurrency[i].currencyType == CurrencyType.microWood)
    //        {
    //            if (actualCurrency[i].amount >= 10)
    //            {
    //                Debug.Log("Micro madera");
    //                AddCurrency(CurrencyType.wood, 1);
    //                actualCurrency[i].amount -= 10;
    //                GameManager.Instance.UIManager.UpdateResourcesText();
    //            }
    //        }

    //        if (actualCurrency[i].currencyType == CurrencyType.microWheat)
    //        {
    //            if (actualCurrency[i].amount >= 10)
    //            {
    //                Debug.Log("Micro trigo");
    //                AddCurrency(CurrencyType.wheat, 1);
    //                actualCurrency[i].amount -= 10;
    //                GameManager.Instance.UIManager.UpdateResourcesText();
    //            }
    //        }

    //    }
    //}
}
