using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagersConstructions
{
    private int maxVillagersInBerries;
    private int maxVillagersInMinerals;
    private int maxVillagersInFiber;

    public VillagersConstructions(int maxVillagersInBerries, int maxVillagersInMinerals, int maxVillagersInFiber)
    {
        MaxVillagersInBerries = maxVillagersInBerries;
        MaxVillagersInMinerals = maxVillagersInMinerals;
        MaxVillagersInFiber = maxVillagersInFiber;
    }

    public int MaxVillagersInBerries { get => maxVillagersInBerries; set => maxVillagersInBerries = value; }
    public int MaxVillagersInMinerals { get => maxVillagersInMinerals; set => maxVillagersInMinerals = value; }
    public int MaxVillagersInFiber { get => maxVillagersInFiber; set => maxVillagersInFiber = value; }

    public void AddMaxVillagersInConstruction(CurrencyType currencyType, int amount)
    {
        switch (currencyType)
        {
            case CurrencyType.minerals:
                MaxVillagersInMinerals += amount;
                break;
            case CurrencyType.fiber:
                MaxVillagersInFiber += amount;
                break;
            case CurrencyType.food:
                MaxVillagersInBerries += amount;
                break;
            default:
                break;
        }
    }
}
