using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    //public delegate void OnTimerCompleteDelegate();

    //public event OnTimerCompleteDelegate OnTimerComplete;

    public float            cycleTime;
    private float           currentTime;
    public int              daysCount = 0;

    public int meadowsInDay = 0;
    public List<int> daysMoreMeadows = new List<int>();
    public bool isExplorerActive;

    public float DayDuration;
    public float NightDuration;

    public TimerStats myActualTime;

    private bool isTimeStop;

    private bool isEnemySpawned;

    public int cardsPerDay;
    public int currentCardsPerDay;
    private bool cardsGiven;

    public bool isResourceIncrementNextDay;
    public int incrementResources = 0;

    private void Start()
    {
        GameManager.Instance.UIManager.SetTimeSliderUI(cycleTime);
        currentCardsPerDay = cardsPerDay;
    }

    private void Update()
    {
        CheckTimer();
    }


    private void CheckTimer()
    {
        if (!isTimeStop)
        {
            TimerUpdate();
        }
    }

    private void TimerUpdate()
    {
        currentTime += Time.deltaTime;
        CheckDayAndNight();

        GameManager.Instance.UIManager.UpdateTimeslider(currentTime);
        //CheckConstructionsProduction();

        if (currentTime > cycleTime)
        {
            CompletedCycle();
            cardsGiven = false;
            //OnTimerComplete();
        }
    }

    private void CheckDayAndNight()
    {
        //Duracion del dia
        if (currentTime <= DayDuration)
        {
            myActualTime = TimerStats.Day;
            if (isEnemySpawned)
            {
                isEnemySpawned = false;
            }
        }
        //Duracion de la noche
        else
        {
            myActualTime = TimerStats.Night;
            if (!isEnemySpawned)
            {
                NightStartFunctions();
            }
        }
    }

    private void NightStartFunctions()
    {
        isEnemySpawned = true;
        // Hacer spawn de enemigos de la base enemiga
        CheckConstructionsProduction();
    }

    private void CheckConstructionsProduction()
    {
        GameManager.Instance.Construction.CheckProductions();
        Debug.Log("Noche");
    }

    private void CompletedCycle()
    {
        FunctionsOnComplete();
        currentTime = currentTime - cycleTime;
    }

    private void FunctionsOnComplete()
    {
        SoundManager.Instance.PlayNewDay();
        BaseStats.Instance.HealBaseInCycle();
        GameManager.Instance.Construction.TerrainProduction();
        CurrencyProduction.Instance.ProductionCurrency();
        VillagerManagement.Instance.AddVillagersInDay();
        MeadowsInDayAndExplorer();
        GiveCards();


        CurrencyProduction.Instance.AddCurrency(CurrencyType.gold, VillagerManagement.Instance.maxVillagers);

        CurrencyProduction.Instance.AddCurrency(CurrencyType.food, -VillagerManagement.Instance.maxVillagers);


        if (currentCardsPerDay > cardsPerDay || currentCardsPerDay < cardsPerDay)
        {
            currentCardsPerDay = cardsPerDay;
        }

        if (isResourceIncrementNextDay)
        {
            CurrencyProduction.Instance.AddCurrency(CurrencyType.fiber, incrementResources);
            CurrencyProduction.Instance.AddCurrency(CurrencyType.food, incrementResources);
            CurrencyProduction.Instance.AddCurrency(CurrencyType.minerals, incrementResources);

            isResourceIncrementNextDay = false;
            incrementResources = 0;
        }

        daysCount++;
        GameManager.Instance.UIManager.UpdateDaysText(daysCount);
        GameManager.Instance.UIManager.SetTimeSliderUI(cycleTime);
    }

    public void IncrementCardsOneDay(int amount)
    {
        currentCardsPerDay += amount;
    }

    public void IsTimerStop(bool stop)
    {
        isTimeStop = stop;
        if (stop == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void GiveCards()
    {
        if (cardsGiven == false)
        {
            GameManager.Instance.CardManager.AddRandomCard(currentCardsPerDay);
            Debug.Log(currentCardsPerDay + " Cartas");
            cardsGiven = true;
        }
    }

    public void ResourceIncrement(bool b, int amountIncrement)
    {
        isResourceIncrementNextDay = b;
        incrementResources += amountIncrement;
    }

    public void MeadowsInDayAndExplorer()
    {
        if (daysMoreMeadows != null)
        {

            if (isExplorerActive)
            {
                for (int i = 0; i < daysMoreMeadows.Count; i++)
                {
                    int rnd = Random.Range(0, 100);
                    if (rnd <= 40)
                    {
                        meadowsInDay++;
                        Debug.Log("Incremento en uno los tiles");
                    }
                }
            }

            for (int i = 0; i < daysMoreMeadows.Count; i++)
            {
                daysMoreMeadows[i]--;
                Debug.Log("Resto un día");
                if (daysMoreMeadows[i] <= 0)
                {
                    daysMoreMeadows.Remove(daysMoreMeadows[i]);
                }
            }

            if (daysMoreMeadows.Count <= 0)
            {
                isExplorerActive = false;
            }
            else
            {
                isExplorerActive = true;
            }
        }

        GameManager.Instance.CreateMeadow(meadowsInDay);

        if (meadowsInDay > 1)
        {
            meadowsInDay = 1;
        }
    }
}
