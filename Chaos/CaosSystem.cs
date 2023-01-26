using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaosSystem : MonoBehaviour
{
    public int currentCaos;
    public int  maxCaos;

    private void Start()
    {
        GameManager.Instance.UIManager.SetSliderChaos(maxCaos);
    }

    public void IncrementCaos(int value)
    {
        if (currentCaos <= maxCaos)
        {
            currentCaos += value;
            GameManager.Instance.UIManager.UpdateChaosSlider(currentCaos);
            //InstanceBoss();
        }
    }

    public void DecrementCaos(int value)
    {
        currentCaos -= value;
        GameManager.Instance.UIManager.UpdateChaosSlider(currentCaos);
    }

    private void InstanceBoss()
    {
        if (currentCaos >= maxCaos)
        {
            // Hacer que el boss aparezca
            GameManager.Instance.UIManager.ActivateMaxChaosUI();
        }
    }
}
