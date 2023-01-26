using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    public void DoCardEffect(CardDisplay card, Vector2 position)
    {
        if (card.cardType == CardType.power || card.cardType == CardType.military || card.cardType == CardType.extractor)
        {
            switch (card.ID)
            {
                // Extra card
                case 8:
                    GameManager.Instance.Time.IncrementCardsOneDay(1);
                    break;
                // Extra Resources
                case 9:
                    GameManager.Instance.Time.ResourceIncrement(true, 3);
                    break;
                // Harvest
                case 10:
                    GameManager.Instance.CardManager.AddTerrainCards(CardType.terrain, 2);
                    break;
                // Missile
                case 11:
                    if (GameManager.Instance.Construction.constructionsDictionary.ContainsKey(position))
                    {
                        ConstructionDisplay tileConstruction = GameManager.Instance.Construction.ReturnConstruction(position);
                        if (tileConstruction != null)
                        {
                            switch (tileConstruction.constructionType)
                            {
                                case ConstructionsType.terrain:
                                    if (tileConstruction.isDroneInConstruction)
                                    {
                                        GameManager.Instance.ClearCellTileMap(GameManager.Instance.m_TileMapOther, position);
                                    }
                                    //VillagerManagement.Instance.DecrementVillagersInConstructions(tileConstruction.currencyType.ToString());
                                    //VillagerManagement.Instance.DecrementMaxVillagersInConstructions(tileConstruction.currencyType, tileConstruction.maxVillagersInConstruction);
                                    break;
                                case ConstructionsType.house:
                                    CurrencyProduction.Instance.AddCurrency(CurrencyType.maxVillagers, -tileConstruction.amount);
                                    break;
                                default:
                                    break;
                            }

                            if (tileConstruction.ID == 1)
                            {
                                BaseStats.Instance.RecieveDamage(999999999);
                            }

                            GameManager.Instance.Construction.RemoveConstruction(position);
                            GameManager.Instance.ClearCellTileMap(GameManager.Instance.m_TileMapBuilds, position);
                        }
                    }

                    GameManager.Instance.Grid.SetValue(position, 999);
                    break;
                // Drone
                case 5:
                    //int id                                = GameManager.Instance.Grid.GetValue(position);
                    if (GameManager.Instance.Construction.ReturnConstruction(position) != null)
                    {
                        ConstructionDisplay construction = GameManager.Instance.Construction.ReturnConstruction(position);
                        if (!construction.isDroneInConstruction)
                        {
                            Construction droneCons = card.dataBuild;
                            //droneCons.maxVillagersInConstruction = construction.maxVillagersInConstruction;
                            //droneCons.currencyType = construction.currencyType;
                            construction.modifier += droneCons.modifier;
                        }
                    }
                    break;
                // Explorer
                case 14:
                    GameManager.Instance.Time.daysMoreMeadows.Add(5);
                    GameManager.Instance.Time.isExplorerActive = true;
                    break;
                default:
                    break;
            }
        }
    }
}
