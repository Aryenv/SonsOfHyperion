using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Nueva Carta", menuName = "Assets/Nueva Carta")]
public class Card : ScriptableObject
{
    public int                              ID;
    public CardType                         cardType;
    public PlacementType                    placementType;
    public PayamentCost[]                   cardCost;
    public string                           cardName;
    [TextArea]
    public string                           description;
    public Construction                     constructionScriptable;
    public ConstructionsType                productionType;
    public int                              chaos;
    public bool                             adyacencia;
    public int                              adyacenciaDist;
    public int                              basePriority;
    public int                              currentPriority;
    public string                           imageName;
    public bool                             isPlayed;
    public Sprite                           image;
    public UnitData                         unit;
    public bool                             IsUnlocked;
    public bool                             isInDeck;
    public GameObject                       feedUseCard;
}
