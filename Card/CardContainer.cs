//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CardContainer
//{
//    //private LoadCards cargaCartas;

//    ////base
//    //private bool baseIsSet;
//    //private float impuestos;
//    //private float totalGold;
//    //private bool impuestosUp;
//    //private float targetTime;
//    //private float targetTimeBuild;
//    ////terreno
//    //private bool chest;
//    //private int prob;
//    ////edificio
//    //private bool doBuild;
//    //private bool buildComplete;

//    //bool cardApplied;//BOOL EN EL IF CUANDO LA CARTA ESTA APLICANDOSE EN UNA CASILLA

//    //public CardContainer(bool baseIsSet, float impuestos, float totalGold, bool impuestosUp, float targetTime, float targetTimeBuild, bool chest, int prob, bool doBuild, bool buildComplete, bool cardApplied, LoadCards cargaCartas)
//    //{
//    //    this.baseIsSet = baseIsSet;
//    //    this.impuestos = impuestos;
//    //    this.totalGold = totalGold;
//    //    this.impuestosUp = impuestosUp;
//    //    this.targetTime = targetTime;
//    //    this.targetTimeBuild = targetTimeBuild;
//    //    this.chest = chest;
//    //    this.prob = prob;
//    //    this.doBuild = doBuild;
//    //    this.buildComplete = buildComplete;
//    //    this.cargaCartas = cargaCartas;
//    //    this.cardApplied = cardApplied;
//    //}

//    //private void ObtainMoney(float impuestos)
//    //{
//    //    impuestos += totalGold;
//    //}

//    ////en update
//    //public void GenerateImpuestos()
//    //{
//    //    if (impuestosUp)
//    //    {
//    //        targetTime -= Time.deltaTime;
//    //        if (targetTime <= 0.0f)
//    //        {
//    //            ObtainMoney(impuestos);
//    //        }
//    //    }
//    //}

//    ////en update
//    //public void Construir()
//    //{
//    //    if (doBuild)
//    //    {
//    //        targetTimeBuild -= Time.deltaTime;
//    //        if (targetTimeBuild <= 0.0f)
//    //        {
//    //            buildComplete = true;
//    //        }
//    //    }
//    //}

//    //public void ExecuteTypeOfCard()
//    //{
//    //    for (int i = 0; i < cargaCartas.allCards.Count; i++)
//    //    {
//    //        switch (cargaCartas.allCards[i].cardTypeEffect)
//    //        {
//    //            case 1:
//    //                baseIsSet = true;
//    //                impuestosUp = true;
//    //                break;

//    //            case 2:
//    //                if (cardApplied)
//    //                {
//    //                    prob = UnityEngine.Random.Range(1, 5);
//    //                    if (prob == 2)
//    //                    {
//    //                        chest = true;
//    //                    }
//    //                    mejoras ?
//    //                }
//    //                break;

//    //            case 3:
//    //                gamemanager restas coste de mana
//    //                totalGold -= cargaCartas.allCards[i].goldCost;
//    //                doBuild = true;
//    //                break;

//    //            case 4:
//    //                Spawner, no hay enemigos(?
//    //                break;

//    //            case 5:
//    //                poderes no hay
//    //                break;

//    //            case 6:
//    //                aldeanos
//    //                break;

//    //            default:
//    //                break;

//    //        }
//    //    }
//    //}

//    #region GETTERS AND SETTERS
//    public bool GetBaseIsSet()
//    {
//        return baseIsSet;
//    }

//    public void SetBaseIsSet()
//    {
//        baseIsSet = false;
//    }

//    public float GetImpuestos()
//    {
//        return impuestos;
//    }

//    public void SetImpuestos()
//    {
//        impuestos = 2f;
//    }

//    public float GetTotalGold()
//    {
//        return totalGold;
//    }

//    public void SetTotalGold()
//    {
//        totalGold = 0f;
//    }

//    public bool GetImpuestosUp()
//    {
//        return impuestosUp;
//    }

//    public void SetImpuestosUp()
//    {
//        impuestosUp = false;
//    }

//    public float GetTargetTime()

//    {
//        return targetTime;

//    }

//    public void SetTargetTime()
//    {
//        targetTime = 10f;
//    }

//    public float GetTargetTimeBuild()
//    {
//        return targetTimeBuild;
//    }

//    public void setTargetTimeBuild()
//    {
//        targetTimeBuild = 12f;
//    }

//    public bool GetChest()
//    {
//        return chest;
//    }

//    public void SetChest()
//    {
//        chest = false;
//    }

//    public int GetProb()
//    {
//        return prob;
//    }

//    public void SetProb()
//    {
//        //prob = false;
//    }

//    public bool GetDoBuild()
//    {
//        return doBuild;
//    }

//    public void SetDoBuild()
//    {
//        doBuild = false;
//    }

//    public bool GetBuildComplete()
//    {
//        return buildComplete;
//    }

//    public void SetBuildComplete()
//    {
//        buildComplete = false;
//    }
//    #endregion
//}
