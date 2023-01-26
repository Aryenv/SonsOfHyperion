using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VillagerManagement : TemporalSingleton<VillagerManagement>
{
    private VillagersConstructions villagersConstructions;

    public int      maxVillagers;
    public int      currentVillagers;
    public int      currentVillagersWorking;

    public int      minFood;
    public int      consumFoodByVillagers;

    private bool    maxedVillagers;
    public bool     villagerInCard;

    private int maxVillagersInBerries;
    private int currentVillagersInBerries;
    private int maxVillagersInMinerals;
    private int currentVillagersInMinerals;
    private int maxVillagersInFiber;
    private int currentVillagersInFiber;

    private void Start()
    {
        SetStart();
    }

    public void IncrementMaxVillagersInConstruction(CurrencyType currencyType, int amount)
    {
        switch (currencyType)
        {
            case CurrencyType.minerals:
                MaxVillagersInMinerals += amount;
                GameManager.Instance.UIManager.SetManagementVillagers();
                if (currentVillagersWorking < maxVillagers)
                {
                    GameManager.Instance.UIManager.incrementVillagerMineralsButton.interactable = true;
                }
                break;
            case CurrencyType.fiber:
                MaxVillagersInFiber += amount;
                GameManager.Instance.UIManager.SetManagementVillagers();
                if (currentVillagersWorking < maxVillagers)
                {
                    GameManager.Instance.UIManager.incrementVillagerFiberButton.interactable = true;
                }
                break;
            case CurrencyType.food:
                MaxVillagersInBerries += amount;
                GameManager.Instance.UIManager.SetManagementVillagers();
                if (currentVillagersWorking < maxVillagers)
                {
                    GameManager.Instance.UIManager.incrementVillagerBerriesButton.interactable = true;
                }
                break;
            default:
                break;
        }
    }

    public void IncrementVillagersInConstructions    (string type)
    {
        CurrencyType currencyType = (CurrencyType)Enum.Parse(typeof(CurrencyType), type);

        // Hacer un Switch para ver cual tengo que incrementar con los tipos de construcciones
        switch (currencyType)
        {
            case CurrencyType.minerals:
                if (currentVillagersWorking < maxVillagers && CurrentVillagersInMinerals < MaxVillagersInMinerals && CurrencyProduction.Instance.GetCurrency(CurrencyType.currentVillagers) > 0)
                {
                    CurrentVillagersInMinerals++;
                    CurrencyProduction.Instance.AddCurrency(CurrencyType.currentVillagers, -1);
                    currentVillagersWorking++;
                    GameManager.Instance.Construction.IncrementVillagerInConstruction(currencyType);
                    GameManager.Instance.UIManager.SetManagementVillagers();

                    if (CurrentVillagersInMinerals >= MaxVillagersInMinerals)
                    {
                        GameManager.Instance.UIManager.incrementVillagerMineralsButton.interactable = false;
                    }

                    if (!GameManager.Instance.UIManager.decrementVillagersMineralsButton.IsInteractable())
                    {
                        GameManager.Instance.UIManager.decrementVillagersMineralsButton.interactable = true;
                    }
                }
                break;
            case CurrencyType.fiber:
                if (currentVillagersWorking < maxVillagers && CurrentVillagersInFiber < MaxVillagersInFiber && CurrencyProduction.Instance.GetCurrency(CurrencyType.currentVillagers) > 0)
                {
                    Debug.Log("Aumento");
                    CurrentVillagersInFiber++;
                    CurrencyProduction.Instance.AddCurrency(CurrencyType.currentVillagers, -1);
                    currentVillagersWorking++;
                    GameManager.Instance.Construction.IncrementVillagerInConstruction(currencyType);
                    GameManager.Instance.UIManager.SetManagementVillagers();

                    if (CurrentVillagersInFiber >= MaxVillagersInFiber || currentVillagersWorking >= maxVillagers)
                    {
                        GameManager.Instance.UIManager.incrementVillagerFiberButton.interactable = false;
                    }

                    if (!GameManager.Instance.UIManager.decrementVillagersFiberButton.IsInteractable())
                    {
                        GameManager.Instance.UIManager.decrementVillagersFiberButton.interactable = true;
                    }
                }
                break;
            case CurrencyType.food:
                if (currentVillagersWorking < maxVillagers && CurrentVillagersInBerries < MaxVillagersInBerries && CurrencyProduction.Instance.GetCurrency(CurrencyType.currentVillagers) > 0)
                {
                    Debug.Log("Aumento");
                    CurrentVillagersInBerries++;
                    CurrencyProduction.Instance.AddCurrency(CurrencyType.currentVillagers, -1);
                    currentVillagersWorking++;
                    GameManager.Instance.Construction.IncrementVillagerInConstruction(currencyType);
                    GameManager.Instance.UIManager.SetManagementVillagers();

                    if (CurrentVillagersInBerries >= MaxVillagersInBerries || currentVillagersWorking >= maxVillagers)
                    {
                        GameManager.Instance.UIManager.incrementVillagerBerriesButton.interactable = false;
                    }

                    if (!GameManager.Instance.UIManager.decrementVillagersBerriesButton.IsInteractable())
                    {
                        GameManager.Instance.UIManager.decrementVillagersBerriesButton.interactable = true;
                    }
                }
                break;
            default:
                break;
        }

        if (currentVillagersWorking >= maxVillagers && CurrencyProduction.Instance.GetCurrency(CurrencyType.currentVillagers) <= 0)
        {
            GameManager.Instance.UIManager.incrementVillagerMineralsButton.interactable = false;
            GameManager.Instance.UIManager.incrementVillagerFiberButton.interactable = false;
            GameManager.Instance.UIManager.incrementVillagerBerriesButton.interactable = false;
        }
        GameManager.Instance.UIManager.SetManagementVillagers();
    }

    public void DecrementVillagersInConstructions(string type)
    {
        CurrencyType currencyType = (CurrencyType)Enum.Parse(typeof(CurrencyType), type);
        // Hacer un Switch para ver cual tengo que reducir con los tipos de construcciones
        switch (currencyType)
        {
            case CurrencyType.minerals:
                if (currentVillagersWorking > 0 && CurrentVillagersInMinerals > 0 && CurrencyProduction.Instance.GetCurrency(CurrencyType.currentVillagers) < CurrencyProduction.Instance.GetCurrency(CurrencyType.maxVillagers))
                {
                    CurrentVillagersInMinerals--;
                    CurrencyProduction.Instance.AddCurrency(CurrencyType.currentVillagers, 1);
                    currentVillagersWorking--;
                    GameManager.Instance.Construction.DecrementVillagerInConstruction(currencyType);
                    GameManager.Instance.UIManager.SetManagementVillagers();

                    if (CurrentVillagersInMinerals <= 0)
                    {
                        GameManager.Instance.UIManager.decrementVillagersMineralsButton.interactable = false;
                    }

                    if (!GameManager.Instance.UIManager.incrementVillagerMineralsButton.IsInteractable())
                    {
                        GameManager.Instance.UIManager.incrementVillagerMineralsButton.interactable = true;
                    }
                }
                break;
            case CurrencyType.fiber:
                if (currentVillagersWorking > 0 && CurrentVillagersInFiber > 0 && CurrencyProduction.Instance.GetCurrency(CurrencyType.currentVillagers) < CurrencyProduction.Instance.GetCurrency(CurrencyType.maxVillagers))
                {
                    CurrentVillagersInFiber--;
                    CurrencyProduction.Instance.AddCurrency(CurrencyType.currentVillagers, 1);
                    currentVillagersWorking--;
                    GameManager.Instance.Construction.DecrementVillagerInConstruction(currencyType);
                    GameManager.Instance.UIManager.SetManagementVillagers();

                    if (CurrentVillagersInFiber <= 0)
                    {
                        GameManager.Instance.UIManager.decrementVillagersFiberButton.interactable = false;
                    }

                    if (!GameManager.Instance.UIManager.incrementVillagerFiberButton.IsInteractable())
                    {
                        GameManager.Instance.UIManager.incrementVillagerFiberButton.interactable = true;
                    }
                }
                break;
            case CurrencyType.food:
                if (currentVillagersWorking > 0 && CurrentVillagersInBerries > 0 && CurrencyProduction.Instance.GetCurrency(CurrencyType.currentVillagers) < CurrencyProduction.Instance.GetCurrency(CurrencyType.maxVillagers))
                {
                    CurrentVillagersInBerries--;
                    CurrencyProduction.Instance.AddCurrency(CurrencyType.currentVillagers, 1);
                    currentVillagersWorking--;
                    GameManager.Instance.Construction.DecrementVillagerInConstruction(currencyType);
                    GameManager.Instance.UIManager.SetManagementVillagers();

                    if (CurrentVillagersInBerries <= 0)
                    {
                        GameManager.Instance.UIManager.decrementVillagersBerriesButton.interactable = false;
                    }

                    if (!GameManager.Instance.UIManager.incrementVillagerBerriesButton.IsInteractable())
                    {
                        GameManager.Instance.UIManager.incrementVillagerBerriesButton.interactable = true;
                    }
                }
                break;
            default:
                break;
        }

        if (currentVillagersWorking < maxVillagers)
        {
            if (CurrentVillagersInBerries < MaxVillagersInBerries)
            {
                GameManager.Instance.UIManager.incrementVillagerBerriesButton.interactable = true;
            }
            if (CurrentVillagersInMinerals < MaxVillagersInMinerals)
            {
                GameManager.Instance.UIManager.incrementVillagerMineralsButton.interactable = true;
            }
            if (CurrentVillagersInFiber < MaxVillagersInFiber)
            {
                GameManager.Instance.UIManager.incrementVillagerFiberButton.interactable = true;
            }
        }
        if (currentVillagersWorking <= maxVillagers && CurrencyProduction.Instance.GetCurrency(CurrencyType.currentVillagers) >= 0)
        {
            if (MaxVillagersInMinerals > 0 && CurrentVillagersInMinerals < MaxVillagersInMinerals)
            {
                GameManager.Instance.UIManager.incrementVillagerMineralsButton.interactable = true;
            }

            if (MaxVillagersInFiber > 0 && CurrentVillagersInFiber < MaxVillagersInFiber)
            {
                GameManager.Instance.UIManager.incrementVillagerFiberButton.interactable = true;
            }

            if (MaxVillagersInBerries > 0 && CurrentVillagersInBerries < MaxVillagersInBerries)
            {
                GameManager.Instance.UIManager.incrementVillagerBerriesButton.interactable = true;
            }
        }
        GameManager.Instance.UIManager.SetManagementVillagers();
    }

    /// <summary>
    /// Incrementa el número máximo de aldeanos
    /// </summary>
    /// <param name="amount"></param>
    public void IncrementMaxVillagers                   (int amount)
    {
        maxVillagers += amount;
        maxedVillagers = true;
        CurrencyProduction.Instance.AddCurrency (CurrencyType.maxVillagers, amount);
    }

    public void DecrementMaxVillagers                   (int amount)
    {
        maxVillagers -= amount;
        CurrencyProduction.Instance.AddCurrency(CurrencyType.maxVillagers, -amount);
    }

    public void DecrementMaxVillagersInConstructions(CurrencyType currencyType, int amount)
    {
        switch (currencyType)
        {
            case CurrencyType.minerals:
                MaxVillagersInMinerals -= amount;
                GameManager.Instance.UIManager.SetManagementVillagers();
                if (currentVillagersWorking <= 0)
                {
                    GameManager.Instance.UIManager.incrementVillagerMineralsButton.interactable = false;
                }
                break;
            case CurrencyType.fiber:
                MaxVillagersInFiber -= amount;
                GameManager.Instance.UIManager.SetManagementVillagers();
                if (currentVillagersWorking <= 0)
                {
                    GameManager.Instance.UIManager.incrementVillagerFiberButton.interactable = false;
                }
                break;
            case CurrencyType.food:
                MaxVillagersInBerries -= amount;
                GameManager.Instance.UIManager.SetManagementVillagers();
                if (currentVillagersWorking <= 0)
                {
                    GameManager.Instance.UIManager.incrementVillagerBerriesButton.interactable = false;
                }
                break;
            default:
                break;
        }
    }

    // Llamar al inicio de un nuevo día
    public void AddVillagersInDay()
    {
        if (maxedVillagers || villagerInCard)
        {
            if (currentVillagers < maxVillagers)
            {
                int villagersToAdd = CurrencyProduction.Instance.GetCurrency(CurrencyType.maxVillagers) - currentVillagers;
                Debug.Log("Loas aldeanos a aplicar son: " + villagersToAdd);
                //currentVillagers += villagersToAdd;
                CurrencyProduction.Instance.AddCurrency(CurrencyType.currentVillagers, villagersToAdd);
                currentVillagers = CurrencyProduction.Instance.GetCurrency(CurrencyType.currentVillagers);
                GameManager.Instance.UIManager.SetManagementVillagers();

                if (currentVillagersWorking < maxVillagers)
                {
                    if (CurrentVillagersInBerries < VillagersConstructions.MaxVillagersInBerries)
                    {
                        if (!GameManager.Instance.UIManager.incrementVillagerBerriesButton.IsInteractable())
                        {
                            GameManager.Instance.UIManager.incrementVillagerBerriesButton.interactable = true;
                        }
                    }
                    if (CurrentVillagersInFiber < VillagersConstructions.MaxVillagersInFiber)
                    {
                        if (!GameManager.Instance.UIManager.incrementVillagerFiberButton.IsInteractable())
                        {
                            GameManager.Instance.UIManager.incrementVillagerFiberButton.interactable = true;
                        }
                    }
                    if (CurrentVillagersInMinerals < VillagersConstructions.MaxVillagersInMinerals)
                    {
                        if (!GameManager.Instance.UIManager.incrementVillagerMineralsButton.IsInteractable())
                        {
                            GameManager.Instance.UIManager.incrementVillagerMineralsButton.interactable = true;
                        }
                    }
                }

                if (currentVillagersWorking < maxVillagers)
                {
                    if (CurrentVillagersInBerries < MaxVillagersInBerries)
                    {
                        GameManager.Instance.UIManager.incrementVillagerBerriesButton.interactable = true;
                    }
                    if (CurrentVillagersInMinerals < MaxVillagersInMinerals)
                    {
                        GameManager.Instance.UIManager.incrementVillagerMineralsButton.interactable = true;
                    }
                    if (CurrentVillagersInFiber < MaxVillagersInFiber)
                    {
                        GameManager.Instance.UIManager.incrementVillagerFiberButton.interactable = true;
                    }
                }
                if (maxedVillagers == true)
                {
                    maxedVillagers = false;
                }
                if (villagerInCard == true)
                {
                    villagerInCard = false;
                }
            }
        }
        

        //if (CurrencyProduction.Instance.GetCurrency(CurrencyType.food) >= minFood)
        //{
        //    if (currentVillagers < maxVillagers)
        //    {
        //        currentVillagers =  maxVillagers - currentVillagersWorking;
        //        CurrencyProduction.Instance.AddCurrency(CurrencyType.currentVillagers, currentVillagers);
        //        GameManager.Instance.UIManager.SetManagementVillagers();

        //        if (currentVillagersWorking < maxVillagers)
        //        {
        //            if (CurrentVillagersInBerries < VillagersConstructions.MaxVillagersInBerries)
        //            {
        //                if (!GameManager.Instance.UIManager.incrementVillagerBerriesButton.IsInteractable())
        //                {
        //                    GameManager.Instance.UIManager.incrementVillagerBerriesButton.interactable = true;
        //                }
        //            }
        //            if (CurrentVillagersInFiber < VillagersConstructions.MaxVillagersInFiber)
        //            {
        //                if (!GameManager.Instance.UIManager.incrementVillagerFiberButton.IsInteractable())
        //                {
        //                    GameManager.Instance.UIManager.incrementVillagerFiberButton.interactable = true;
        //                }
        //            }
        //            if (CurrentVillagersInMinerals < VillagersConstructions.MaxVillagersInMinerals)
        //            {
        //                if (!GameManager.Instance.UIManager.incrementVillagerMineralsButton.IsInteractable())
        //                {
        //                    GameManager.Instance.UIManager.incrementVillagerMineralsButton.interactable = true;
        //                }
        //            }
        //        }
        //    }
        //}
    }

    /// <summary>
    /// Consume comida por ciudadano
    /// </summary>
    public void ConsumFoodByVillagers()
    {
        int consumedFood;
        // La comida que consumen en total los ciudadanos
        consumedFood = currentVillagers * consumFoodByVillagers;

        int actualVillagers = currentVillagers;

        for (int i = 0; i < actualVillagers; i++)
        {
            // Resto comida por ciudadano
            CurrencyProduction.Instance.AddCurrency(CurrencyType.food, -consumFoodByVillagers);

            // Comprueba si la comida actual es menor que la que se consume en total
            if (CurrencyProduction.Instance.GetCurrency(CurrencyType.food) < consumedFood)
            {
                // Quito aldeano
                currentVillagers--;
            }
        }
    }

    //public void ProductionVillagers()
    //{
    //    CurrencyType currencyType;
    //    int totalAmount;
    //}

    private void SetStart()
    {
        currentVillagers                                                                = maxVillagers;
        currentVillagersWorking                                                         = 0;
        CurrencyProduction.Instance.AddCurrency                                         (CurrencyType.maxVillagers, maxVillagers);
        CurrencyProduction.Instance.AddCurrency                                         (CurrencyType.currentVillagers, currentVillagers);
        VillagersConstructions                                                          = new VillagersConstructions(MaxVillagersInBerries, MaxVillagersInMinerals, MaxVillagersInFiber);
        GameManager.Instance.UIManager.SetManagementVillagers                           ();
        GameManager.Instance.UIManager.decrementVillagersBerriesButton.interactable     = false;
        GameManager.Instance.UIManager.decrementVillagersMineralsButton.interactable    = false;
        GameManager.Instance.UIManager.decrementVillagersFiberButton.interactable       = false;

        GameManager.Instance.UIManager.incrementVillagerBerriesButton.interactable      = false;
        GameManager.Instance.UIManager.incrementVillagerMineralsButton.interactable     = false;
        GameManager.Instance.UIManager.incrementVillagerFiberButton.interactable        = false;
    }

    public VillagersConstructions VillagersConstructions    { get => villagersConstructions;        set => villagersConstructions       = value; }
    public int MaxVillagersInBerries                        { get => maxVillagersInBerries;         set => maxVillagersInBerries        = value; }
    public int MaxVillagersInMinerals                       { get => maxVillagersInMinerals;        set => maxVillagersInMinerals       = value; }
    public int MaxVillagersInFiber                          { get => maxVillagersInFiber;           set => maxVillagersInFiber          = value; }
    public int CurrentVillagersInMinerals                   { get => currentVillagersInMinerals;    set => currentVillagersInMinerals   = value; }
    public int CurrentVillagersInFiber                      { get => currentVillagersInFiber;       set => currentVillagersInFiber      = value; }
    public int CurrentVillagersInBerries                    { get => currentVillagersInBerries;     set => currentVillagersInBerries    = value; }
}
