using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConstructionProduction : MonoBehaviour
{
    public static int           N_ENEMY = 1;
    public static int           N_ALLIE = 1;
    public ConstructionDisplay  m_constructPrefab;
    public GameObject           m_EnemyPrefab;
    public GameObject           m_AlliePrefab;
    public GameObject           turretPrefab;

    [Header("ScriptablesEnemy")]
    public UnitData gobloData;
    public UnitData gruntData;

    private bool spawnedBoss;

    private List<Vector2> removeList = new List<Vector2>();

    public List<ConstructionDisplay> constructions;

    public Dictionary<Vector2, ConstructionDisplay> constructionsDictionary = new Dictionary<Vector2, ConstructionDisplay>();

    public void AddConstrucion(Construction construction, Vector2 node)
    {
        if (!constructionsDictionary.ContainsKey(node))
        {
            if (construction != null)
            {
                Vector2 centerNode = GameManager.Instance.Grid.GetCenterCell(node);
                ConstructionDisplay tempConst = Instantiate(m_constructPrefab, centerNode, Quaternion.identity);
                tempConst.dataConstruction = construction;
                tempConst.name = $"{construction.name}";

                Debug.Log(construction + " " + node);

                constructions.Add(tempConst);
                //constructionsDictionary.Add(node, tempConst);

                switch (construction.constructionType)
                {
                    case ConstructionsType.house:
                        //VillagerManagement.Instance.IncrementMaxVillagers(construction.amount);
                        CurrencyProduction.Instance.AddCurrency(CurrencyType.currentVillagers, construction.amount);
                        CurrencyProduction.Instance.AddCurrency(CurrencyType.maxVillagers, construction.amount);
                        break;
                    case ConstructionsType.military:
                        GameObject go = Instantiate(turretPrefab, centerNode, Quaternion.identity);
                        break;
                    case ConstructionsType.terrain:
                        tempConst.workers.SetActive(true);
                        tempConst.currentVillagersInConstruction++;
                        tempConst.currentWorkersText.text = tempConst.currentVillagersInConstruction.ToString();
                        //VillagerManagement.Instance.IncrementMaxVillagersInConstruction(construction.currencyType, construction.maxVillagersInConstruction);
                        //GameManager.Instance.UIManager.SetManagementVillagers();
                        break;
                    case ConstructionsType.extractor:
                        break;
                    default:
                        break;
                }

                tempConst.daystoCrop = construction.daysToCropped;

                constructionsDictionary.Add(node, tempConst);
                Debug.Log(constructionsDictionary[node].name);
                Debug.Log(constructionsDictionary[node].daystoCrop);
            }
        }
    }

    public ConstructionDisplay ReturnConstruction(int ID)
    {
        foreach (KeyValuePair<Vector2, ConstructionDisplay> construction in constructionsDictionary)
        {
            if (construction.Value.ID == ID)
            {
                return construction.Value;
            }
        }
        return null;
    }

    public ConstructionDisplay ReturnConstruction(Vector2 ID)
    {
        foreach (KeyValuePair<Vector2, ConstructionDisplay> construction in constructionsDictionary)
        {
            if (construction.Key == ID)
            {
                return construction.Value;
            }
        }
        return null;
    }

    public Vector2 ReturnConstructionPosition(int ID)
    {
        foreach (KeyValuePair<Vector2, ConstructionDisplay> construction in constructionsDictionary)
        {
            if (construction.Value.ID == ID)
            {
                return construction.Key;
            }
        }
        return new Vector2(0, 0);
    }

    public void RemoveConstruction(Vector2 node)
    {
        ConstructionDisplay construction;
        constructionsDictionary.TryGetValue(node, out construction);

        if (construction != null)
        {
            construction.gameObject.SetActive(false);
        }

        constructionsDictionary[node].daystoCrop = 0;

        constructions.Remove(construction);
        Debug.Log(constructionsDictionary[node].name);
        constructionsDictionary.Remove(node);
    }

    public void CheckProductions()
    {
        DoProduction();
    }

    private void DoProduction()
    {
        if (constructionsDictionary != null)
        {
            foreach (KeyValuePair<Vector2, ConstructionDisplay> construction in constructionsDictionary)
            {
                if (/*construction.Value.time != 0 && construction.Value.timeToProduce <= Time.time && */construction.Value.currencyType != CurrencyType.Null)
                {
                    if (construction.Value.constructionType == ConstructionsType.spawner && !construction.Value.isProduction)
                    {
                        construction.Value.isProduction = true;
                        Vector2 spawnPos = construction.Key;
                        GameObject tempEnemy = m_EnemyPrefab;
                        int amountToSpawn = construction.Value.amount;

                        List<PathNode> postionsSpawnNodes = new List<PathNode>();

                        for (int i = 0; i < amountToSpawn; i++)
                        {
                            PathNode position = GameManager.Instance.Grid.GetFreeNode(spawnPos, 1);
                            if (position != null)
                            {
                                postionsSpawnNodes.Add(position);
                                if (position.isOcuped == true)
                                {
                                    position = GameManager.Instance.Grid.GetFreeNode(spawnPos, 2);
                                }
                                if (position != null && position.isOcuped == false)
                                {
                                    if (GameManager.Instance.Caos.currentCaos >= GameManager.Instance.Caos.maxCaos && spawnedBoss == false)
                                    {
                                        tempEnemy.GetComponent<UnitDisplay>().dataEnemy = gruntData;
                                        spawnedBoss = true;
                                    }
                                    else
                                    {
                                        tempEnemy.GetComponent<UnitDisplay>().dataEnemy = gobloData;
                                    }
                                    Vector2 targetPos = TranformNodeToCell(new Vector2(position.x, position.y));
                                    position.isOcuped = true;
                                    GameObject go = Instantiate(tempEnemy, targetPos, Quaternion.identity);
                                    go.name = $"Enemy {N_ENEMY++}";
                                }
                            }
                        }
                        if (postionsSpawnNodes != null)
                        {
                            for (int i = 0; i < postionsSpawnNodes.Count; i++)
                            {
                                if (postionsSpawnNodes[i].isOcuped == true)
                                {
                                    postionsSpawnNodes[i].isOcuped = false;
                                }
                            }
                        }

                        if (construction.Value.ID == IDConstants.BIG_SPAWNER_ID)
                        {
                            if (GameManager.Instance.Time.daysCount % 3 == 0)
                            {
                                construction.Value.amount++;
                            }
                            construction.Value.timeToProduce = construction.Value.time + 5 + Time.time;
                        }
                        else
                        {
                            construction.Value.timeToProduce = construction.Value.time + Time.time;
                        }
                    }
                    construction.Value.isProduction = false;
                    if (spawnedBoss == true)
                    {
                        construction.Value.currencyType = CurrencyType.Null;
                    }
                }
            }
        }
    }

    public void TerrainProduction()
    {
        if (constructionsDictionary != null)
        {
            foreach (KeyValuePair<Vector2, ConstructionDisplay> construction in constructionsDictionary)
            {
                if (construction.Value.constructionType != ConstructionsType.spawner && construction.Value.currencyType != CurrencyType.Null)
                {
                    //Check Adyacent tiles
                    int extra = GameManager.Instance.CheckAdyacentConstruction(construction.Key, construction.Value.targetResourceID, construction.Value.actioResourceDis);
                    //ADD CURRENCY
                    AddCurrency(construction, extra);
                    if (construction.Value.isCrop && construction.Value.currentVillagersInConstruction > 0)
                    {
                        construction.Value.currentDays++;
                        if (construction.Value.currentDays >= construction.Value.daystoCrop)
                        {
                            //VillagerManagement.Instance.DecrementVillagersInConstructions(construction.Value.currencyType.ToString());
                            //VillagerManagement.Instance.DecrementMaxVillagersInConstructions(construction.Value.currencyType, construction.Value.maxVillagersInConstruction);
                            //DecrementVillagerInConstruction(construction.Value.currencyType);
                            CurrencyProduction.Instance.AddCurrency(CurrencyType.currentVillagers, construction.Value.maxVillagersInConstruction);
                            removeList.Add(construction.Key);
                        }
                    }
                }
            }

            if (removeList.Count > 0)
            {
                for (int i = 0; i < removeList.Count; i++)
                {
                    GameManager.Instance.Grid.SetValue(removeList[i], IDConstants.MEADOW_ID);
                    RemoveConstruction(removeList[i]);
                }

                removeList.Clear();
            }
        }
    }

    public void IncrementVillagerInConstruction(CurrencyType currencyType)
    {
        int i = 0;
        foreach (KeyValuePair<Vector2, ConstructionDisplay> construction in constructionsDictionary)
        {
            if (construction.Value.modifier > 1)
            {
                if (construction.Value.currencyType == currencyType)
                {
                    if (construction.Value.currentVillagersInConstruction < construction.Value.maxVillagersInConstruction)
                    {
                        construction.Value.currentVillagersInConstruction++;
                        construction.Value.currentWorkersText.text = construction.Value.currentVillagersInConstruction.ToString();
                        i++;
                        break;
                    }
                }
            }
        }

        if (i == 0)
        {
            foreach (KeyValuePair<Vector2, ConstructionDisplay> construction in constructionsDictionary)
            {
                if (construction.Value.currencyType == currencyType)
                {
                    if (construction.Value.currentVillagersInConstruction < construction.Value.maxVillagersInConstruction)
                    {
                        construction.Value.currentVillagersInConstruction++;
                        construction.Value.currentWorkersText.text = construction.Value.currentVillagersInConstruction.ToString();
                        break;
                    }
                }
            }
        }
    }

    public void DecrementVillagerInConstruction(CurrencyType currencyType)
    {
        int i = 0;
        foreach (KeyValuePair<Vector2, ConstructionDisplay> construction in constructionsDictionary)
        {
            if (construction.Value.modifier < 2)
            {
                if (construction.Value.currencyType == currencyType)
                {
                    if (construction.Value.currentVillagersInConstruction > 0)
                    {
                        construction.Value.currentVillagersInConstruction--;
                        construction.Value.currentWorkersText.text = construction.Value.currentVillagersInConstruction.ToString();
                        i++;
                        break;
                    }
                }
            }
        }

        if (i == 0)
        {
            foreach (KeyValuePair<Vector2, ConstructionDisplay> construction in constructionsDictionary)
            {
                if (construction.Value.currencyType == currencyType)
                {
                    if (construction.Value.currentVillagersInConstruction > 0)
                    {
                        construction.Value.currentVillagersInConstruction--;
                        construction.Value.currentWorkersText.text = construction.Value.currentVillagersInConstruction.ToString();
                        break;
                    }
                }
            }
        }
    }

    private void AddStartCurrency(Vector2 key, CurrencyType currency, int amount)
    {
        CurrencyProduction.Instance.AddCurrency(currency, amount);
        GameManager.Instance.Feedback.ShowFloatingText(currency, amount, key, GameManager.Instance.m_TileMapTerrain);
        //CurrencyProduction.Instance.RefreshCurrency();
        GameManager.Instance.UIManager.UpdateResourcesText();
    }

    private void AddCurrency(KeyValuePair<Vector2, ConstructionDisplay> construction, int extra)
    {
        int totalAmount;
        switch (construction.Value.currencyType)
        {
            case CurrencyType.minerals:
                totalAmount = construction.Value.amount * construction.Value.modifier;
                break;
            case CurrencyType.fiber:
                totalAmount = construction.Value.amount * construction.Value.modifier;
                break;
            case CurrencyType.food:
                totalAmount = construction.Value.amount * construction.Value.modifier;
                break;
            default:
                totalAmount = 0;
                break;
        }
        if (totalAmount > 0)
        {
            //totalAmount = construction.Value.amount + extra;
            CurrencyProduction.Instance.AddCurrency(construction.Value.currencyType, totalAmount);
            GameManager.Instance.Feedback.ShowFloatingText(construction.Value.currencyType, totalAmount, construction.Key, GameManager.Instance.m_TileMapTerrain);
            //CurrencyProduction.Instance.RefreshCurrency();
            GameManager.Instance.UIManager.UpdateResourcesText();
        }
        construction.Value.timeToProduce = construction.Value.time + Time.time;
        //construction.Value.currentDays++;
        //if (construction.Value.currentDays >= construction.Value.daystoCrop)
        //{
        //    GameManager.Instance.Grid.SetValue(construction.Key, IDConstants.MEADOW_ID);
        //    DecrementVillagerInConstruction(construction.Value.currencyType);
        //    RemoveConstruction(construction.Key);
        //}
    }

    private Vector2 TranformNodeToCell(Vector2 node)
    {
        Vector2 pos = GameManager.Instance.Grid.GetCenterCell(node);
        return pos;
    }

    public void InstantiateAlly(CardDisplay card, Vector2 pos)
    {
        GameObject tempAlly = m_AlliePrefab;
        tempAlly.GetComponent<UnitDisplay>().dataEnemy = card.unitData;
        Vector2 targetPos = TranformNodeToCell(new Vector2(pos.x, pos.y));

        VillagerManagement.Instance.currentVillagers--;
        VillagerManagement.Instance.villagerInCard = true;

        GameObject go = Instantiate(tempAlly, targetPos, Quaternion.identity);
    }

    public UnitData data;

    public void SpawnEnemyInNode(Vector2 pos)
    {
        PathNode node = GameManager.Instance.Grid.GetGridNode((int)pos.x, (int)pos.y);
        Vector2 targetPos = GameManager.Instance.Grid.GetCenterCell(new Vector2(node.x, node.y));
        GameObject tempEnemy = m_EnemyPrefab;
        tempEnemy.GetComponent<UnitDisplay>().dataEnemy = data;
        Instantiate(tempEnemy, targetPos, Quaternion.identity);
    }
}
