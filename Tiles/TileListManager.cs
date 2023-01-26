using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileListManager : MonoBehaviour
{
    public Dictionary<int, AdyacencyEnum> dictionaryIdEnum = new Dictionary<int, AdyacencyEnum>();

    public Dictionary<AdyacencyEnum, TileBase> dictionaryEnumTile = new Dictionary<AdyacencyEnum, TileBase>();

    public Dictionary<int, TileBase> dictionaryIdTiles = new Dictionary<int, TileBase>();

    [Header("Lista de todos los tiles")]
    [SerializeField]
    public List<TileBase> listAllTiles = new List<TileBase>();
    [Header("Estructura de no adyacentes")]
    public List<TileStruct> RuleListTiles;

    [Header("Estructura de adyacentes y combinatoria")]
    [ArrayElementTitle("tilesRequirement")]
    public List<AdyacencyStruct> listAdyacenciesCombinatory;

    public Dictionary<int, AdyacencyStruct> dictionaryAdyacency = new Dictionary<int, AdyacencyStruct>();

    private void Awake()
    {
        LoadDictionaries();
    }

    #region DictionariesAndFunctions

    public void LoadDictionaries()
    {
        if (listAllTiles != null)
        {
            for (int i = 1; i < listAllTiles.Count; i++)
            {
                int id = int.Parse(listAllTiles[i].name);

                AdyacencyEnum myEnum;
                switch (id)
                {
                    case 97:
                        myEnum = AdyacencyEnum.meadow;
                        break;
                    case 1:
                        myEnum = AdyacencyEnum.Settlement;
                        break;
                    case 2:
                        myEnum = AdyacencyEnum.fiber;
                        break;
                    case 3:
                        myEnum = AdyacencyEnum.minerals;
                        break;
                    case 4:
                        myEnum = AdyacencyEnum.food;
                        break;
                    case 5:
                        myEnum = AdyacencyEnum.arcaneMagic;
                        break;
                    case 6:
                        myEnum = AdyacencyEnum.house;
                        break;
                    case 7:
                        myEnum = AdyacencyEnum.guardTower;
                        break;
                    case 98:
                        myEnum = AdyacencyEnum.mysteriousPlace;
                        break;
                    case 99:
                        myEnum = AdyacencyEnum.enemySpawner;
                        break;
                    case 100:
                        myEnum = AdyacencyEnum.bigPortal;
                        break;
                    case 999:
                        myEnum = AdyacencyEnum.adyacency;
                        break;
                    default:
                        myEnum = AdyacencyEnum.Null;
                        break;
                }
                if (id != 0)
                {
                    dictionaryIdTiles.Add(id, listAllTiles[i]);
                    dictionaryIdEnum.Add(id, myEnum);
                    dictionaryEnumTile.Add(myEnum, listAllTiles[i]);
                }
            }
        }

        SetDictionary();
    }

    public int ReturnKeyID(AdyacencyEnum thisEnum)
    {
        foreach (var item in dictionaryIdEnum)
        {
            if (thisEnum == item.Value)
            {
                return item.Key;
            }
        }
        return 0;
    }

    public TileBase ReturnTileID(int id)
    {
        TileBase tile;
        tile = dictionaryIdTiles[id];
        return tile;
    }

    public AdyacencyEnum ReturnEnum(int key)
    {
        AdyacencyEnum adyacency;
        adyacency = dictionaryIdEnum[key];
        return adyacency;
    }

    public AdyacencyEnum ReturnEnum(TileBase tile)
    {
        foreach (var item in dictionaryEnumTile)
        {
            if (tile == item.Value)
            {
                return item.Key;
            }
        }
        return AdyacencyEnum.Null;
    }

    public TileBase ReturnTile(AdyacencyEnum adyacency)
    {
        TileBase tile;
        tile = dictionaryEnumTile[adyacency];
        return tile;
    }

    #endregion

    #region adyacencias
    private void SetDictionary()
    {
        if (listAdyacenciesCombinatory != null)
        {
            int i = 0;
            foreach (AdyacencyStruct adyacency in listAdyacenciesCombinatory)
            {
                dictionaryAdyacency.Add(i, adyacency);
                i++;
            }
        }
    }
    #endregion
}
