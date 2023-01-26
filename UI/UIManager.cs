using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider timeSlider;
    public Slider chaosSlider;
    public Slider baseLife;

    public GameObject maxChaosPanel;
    public GameObject gameOverPanel;

    public TextMeshProUGUI currentGoldText;

    public TextMeshProUGUI maxVillagersBerries;
    public TextMeshProUGUI currentVillagersBerries;
    public Button incrementVillagerBerriesButton;
    public Button decrementVillagersBerriesButton;

    public TextMeshProUGUI maxVillagersMinerals;
    public TextMeshProUGUI currentVillagersMinerals;
    public Button incrementVillagerMineralsButton;
    public Button decrementVillagersMineralsButton;

    public TextMeshProUGUI maxVillagersFiber;
    public TextMeshProUGUI currentVillagersFiber;
    public Button incrementVillagerFiberButton;
    public Button decrementVillagersFiberButton;

    public TextMeshProUGUI villagerText;
    public TextMeshProUGUI currentVillagerText;
    public TextMeshProUGUI fiberText;
    public TextMeshProUGUI mineralsText;
    public TextMeshProUGUI daysText;
    public TextMeshProUGUI baseAttackText;
    public TextMeshProUGUI foodText;

    public void SetTimeSliderUI(float timeCycle)
    {
        timeSlider.maxValue = timeCycle;
        timeSlider.value = 0;
    }

    public void SetManagementVillagers()
    {
        maxVillagersBerries.text = VillagerManagement.Instance.MaxVillagersInBerries.ToString();
        currentVillagersBerries.text = VillagerManagement.Instance.CurrentVillagersInBerries.ToString();

        maxVillagersMinerals.text = VillagerManagement.Instance.MaxVillagersInMinerals.ToString();
        currentVillagersMinerals.text = VillagerManagement.Instance.CurrentVillagersInMinerals.ToString();

        maxVillagersFiber.text = VillagerManagement.Instance.MaxVillagersInFiber.ToString();
        currentVillagersFiber.text = VillagerManagement.Instance.CurrentVillagersInFiber.ToString();

        currentVillagerText.text = CurrencyProduction.Instance.GetCurrency(CurrencyType.currentVillagers).ToString();
    }

    public void UpdateTimeslider(float time)
    {
        timeSlider.value = time;
    }

    public void UpdateChaosSlider(int currentChaos)
    {
        chaosSlider.value = currentChaos;
    }

    public void UpdateBaseAttackText()
    {
        baseAttackText.text = BaseStats.Instance.baseAttack.ToString();
    }

    public void SetSliderChaos(int maxChaos)
    {
        chaosSlider.maxValue = maxChaos;
        chaosSlider.value = 0;
    }

    public void SetBaseLifeSlider()
    {
        baseLife.maxValue = BaseStats.Instance.maxLife;
        baseLife.value = baseLife.maxValue;
    }

    public void UpdateBaseLifeSlider()
    {
        baseLife.value = BaseStats.Instance.currentLife;
    }

    public void UpdateResourcesText()
    {
        villagerText.text               = CurrencyProduction.Instance.GetCurrency(CurrencyType.maxVillagers).ToString();
        currentVillagerText.text        = CurrencyProduction.Instance.GetCurrency(CurrencyType.currentVillagers).ToString();
        fiberText.text                  = CurrencyProduction.Instance.GetCurrency(CurrencyType.fiber).ToString();
        mineralsText.text               = CurrencyProduction.Instance.GetCurrency(CurrencyType.minerals).ToString();
        foodText.text                   = CurrencyProduction.Instance.GetCurrency(CurrencyType.food).ToString();
        currentGoldText.text            = CurrencyProduction.Instance.GetCurrency(CurrencyType.gold).ToString();
    }

    public void UpdateDaysText(int value)
    {
        daysText.text = value.ToString();
    }

    public void ActivateMaxChaosUI()
    {
        maxChaosPanel.SetActive(true);
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAactivado");
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
