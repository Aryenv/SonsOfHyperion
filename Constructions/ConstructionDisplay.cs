using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConstructionDisplay : MonoBehaviour
{
    public Construction                     dataConstruction;
    public UnitData                         enemydata;
    public GameObject                       workers;
    public TextMeshProUGUI                  currentWorkersText;
    public TextMeshProUGUI                  maxWorkersText;

    public ConstructionsType                constructionType;
    public CurrencyType                     currencyType;
    public int                              amount;
    public float                            time;
    public float                            timeToProduce;
    public int                              actioResourceDis;
    public int                              targetResourceID;
    public int                              modifier;
    public int                              maxVillagersInConstruction;
    public int                              currentVillagersInConstruction;
    public int                              ID;
    public bool                             isDroneInConstruction;
    public int daystoCrop;
    public int currentDays;
    public bool isProduction;
    public bool isCrop;


    private void Start()
    {
        SetConstruction();
    }

    public void SetConstruction()
    {
        if (dataConstruction.enemyData != null)
        {
            enemydata = dataConstruction.enemyData;
        }
        ID                                  = dataConstruction.ID;
        constructionType                    = dataConstruction.constructionType;
        currencyType                        = dataConstruction.currencyType;
        amount                              = dataConstruction.amount;
        actioResourceDis                    = dataConstruction.accionDist;
        targetResourceID                    = dataConstruction.targetResourceID;
        maxVillagersInConstruction          = dataConstruction.maxVillagersInConstruction;
        currentVillagersInConstruction      = maxVillagersInConstruction;
        modifier                            = dataConstruction.modifier;
        time                                = dataConstruction.timeToProduct;
        timeToProduce                       = time + Time.time;
        maxWorkersText.text                 = maxVillagersInConstruction.ToString();
        daystoCrop                          = dataConstruction.daysToCropped;
        isCrop                              = dataConstruction.isCrop;
        isProduction = dataConstruction.isProductionDo;
    }
}
