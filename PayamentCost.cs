using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PayamentCost
{
    public CurrencyType currencyType;
    public int cost;

    public PayamentCost(CurrencyType type, int amount)
    {
        this.currencyType = type;
        this.cost = amount;
    }
}
