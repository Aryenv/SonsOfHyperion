using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Construcción", menuName = "Assets/Nueva Construcción")]
public class Construction : ScriptableObject
{
    public int                              ID;
    public ConstructionsType                constructionType;
    public CurrencyType                     currencyType;
    public UnitData                         enemyData;
    public int                              amount;
    public int                              timeToProduct;
    public int                              accionDist;
    public int                              width;
    public int                              height;
    public int                              maxVillagersInConstruction;
    public int                              modifier;
    public int                              daysToCropped;

    [Tooltip("La ID de recurso que busca")]
    public int                              targetResourceID;
    public bool                             isProductionDo;
    public bool                             isCrop;
}
