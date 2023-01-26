using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using CodeMonkey.Utils;

public class GameManager : TemporalSingleton<GameManager>
{
    private TileListManager                     tileListManager;
    private CardEffect                          cardEffect;
    private TerrainDictionary                   terrainDictionary;
    private CardManager                         m_CardManager;
    private Hand                                handManager;
    private CustomGrid                          m_grid;
    private CardStructuresManager               cardsStructures;
    private ConstructionProduction              construction;
    private CaosSystem                          m_caos;
    private ProductionBuildingsFeedback         feedback;
    private UIManager                           uIManager;
    private Timer                               m_Time;
    public  Grid                                unityGrid;
    private Pathfinding                         pathFinding;
    private ZoneEfects                          zoneEfects;
    private InputCard                           cheatsCards;
    private MoveCamera                          moveCamera;

    public Tilemap      m_TileMapTerrain;
    public Tilemap      m_TileMapBuilds;
    public Tilemap      m_TileMapEffects;
    public Tilemap      m_TileMapOther;
    public Tilemap      m_TileMapBackgraund;
    public TileBase bacgraundBase;

    public TileBase     ActiveZones;

    private List<PathNode> walkeablesPath = new List<PathNode>();

    [Header("Hand Manager")]
    [Tooltip("Cantidad maxima de cartas en la mano")]
    public int                  m_maxCardsInHand;
    public int                  currentCardsInHand;
    public List<GameObject>     cardsInHand;
    public GameObject           currentCardSelected;
    [Space]
    public Transform            handZone;
    public Transform            graveryard;
    public Transform            canvas;
    [Space]
    [Header("Grid")]
    public int      gridWidth;
    public int      gridHeight;
    [Space]
    public int      selectdConstruction;

    private int     tempSave = 999;
    public float    gridCellSize;
    private float   timerClick;
    public bool     cardSelected = false;
    private bool    cheatsON = false;

    public override void Awake              ()
    {
        SetReferences();
    }
    //-------START-------
    private void Start                      ()
    {
        Debug.Log("START-GAMEMANAGER");
        GlobalSet();
        CreateFisicGrid();
        CreatePathfinding();
        //SetTileMapToGridTerrain();
        SetTileMapToGridBuilding();
        CreateBase();
        CreateZones();
        CreateEnemyBase(3);

        CreateTileInZone(1, 1, IDConstants.MIYSTERIOUS_PLACE_ID);
        CreateTileInZone(2, 2, IDConstants.MIYSTERIOUS_PLACE_ID);
        CreateTileInZone(3, 3, IDConstants.MIYSTERIOUS_PLACE_ID);

        //CreateRandomCell(24);
        LoadBackgraund();
        moveCamera.SetCamera();
    }
    //-------UPDATE-------
    private void Update()
    {
        SetInput                ();
        AnaliceGrid             ();
        Cheats                  ();
    }
    //-------Trucos-------
    private void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            cheatsON = !cheatsON;
            cheatsCards.enabled = !cheatsCards.isActiveAndEnabled;
        }
        if (cheatsON)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.F))
                {
                    Vector2 center = new Vector2(Grid.GetWidth() / 2, Grid.GetHeight() / 2);
                    GetFreeCell(center, gridWidth / 2, IDConstants.MEADOW_ID, 1);
                    Debug.Log("Print arround");
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Vector2 center = new Vector2(Grid.GetWidth() / 2, Grid.GetHeight() / 2);
                    GetFreeCell(center, gridWidth / 2, IDConstants.MEADOW_ID, 1);
                    Debug.Log("Print arround");
                }
            }
        }      
    }
    //-----FUNCTIONS------

    private bool IsDrone(Vector2 node)
    {
        if (Construction.ReturnConstruction(node) != null)
        {
            return Construction.ReturnConstruction(node).isDroneInConstruction;
        }
        else
        {
            return false;
        }
    }
    private void LoadBackgraund()
    {
        for (int x = 0; x < Grid.GetWidth(); x++)
        {
            for (int y = 0; y < Grid.GetHeight(); y++)
            {
                m_TileMapBackgraund.SetTile(new Vector3Int(x, y,0), bacgraundBase);
            }
        }
    }
    private void CreateRandomCell(int ammount)
    {
        Vector2 center = new Vector2(Grid.GetWidth() / 2, Grid.GetHeight() / 2);
        GetFreeCell(center, gridWidth / 2, IDConstants.MEADOW_ID, ammount);
    }
    private void GlobalSet                  ()
    {
        CreateGrid(ref m_grid, gridWidth, gridHeight, gridCellSize);
        CreateHandManager();
        UIManager.UpdateResourcesText();
    }
    private void SetReferences              ()
    {
        CardsStructures         = GetComponent<CardStructuresManager>();
        Construction            = GetComponent<ConstructionProduction>();
        Caos                    = GetComponent<CaosSystem>();
        Time                    = GetComponent<Timer>();
        UIManager               = GetComponent<UIManager>();
        CardManager             = GetComponent<CardManager>();
        Feedback                = GetComponent<ProductionBuildingsFeedback>();
        TileListManager         = FindObjectOfType<TileListManager>();
        zoneEfects              = GetComponent<ZoneEfects>();
        cheatsCards             = GetComponent<InputCard>();
        CardEffect              = GetComponent<CardEffect>();
        terrainDictionary       = GetComponent<TerrainDictionary>();
        moveCamera              = FindObjectOfType<MoveCamera>();
    }
    private void SetInput                   ()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Valor de celda: "+ Grid.GetValue(GetGriddCell()));
        }
        //Seleccionar
        if (Input.GetMouseButtonDown(0))
        {
            if (!cardSelected)
            {
                if (HandManager.ReturnCardSelected())
                {
                    currentCardSelected = HandManager.ReturnCardSelected();
                    CardDisplay tempCDisplay = HandManager.ReturnCardSelected().GetComponent<CardDisplay>();

                    if (tempCDisplay)
                    {
                        cardSelected = true;
                        timerClick = 0;
                        ActiveConstructionZones(tempCDisplay);
                        timerClick = UnityEngine.Time.time + 0.3f;
                    }
                }
            }
            else
            {
                if (HandManager.ReturnCardSelected())
                {
                    currentCardSelected = HandManager.ReturnCardSelected();
                    CardDisplay tempCDisplay = HandManager.ReturnCardSelected().GetComponent<CardDisplay>();
                    if (tempCDisplay)
                    {
                        //Comprobar celda
                        if (timerClick <= UnityEngine.Time.time)
                        {
                            //Tiles
                            TileInteraction(GetGriddCell(), tempCDisplay);                           
                        }
                    }
                }              
            }         
        }
        //Deselecionar
        if (Input.GetMouseButtonDown(1))
        {
            DeseleccionateCard();
            ClearAllTileMap(m_TileMapEffects);
        }
    }
    public void ClearCellTileMap(Tilemap map, Vector2 node)
    {
        map.SetTile(new Vector3Int((int)node.x,(int)node.y, 0), null);
    }
    private void ClearAllTileMap(Tilemap map)
    {
        Tilemap tilemap = map;
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                map.SetTile(new Vector3Int(x, y, 0), null);
            }
        }
    }
    public Vector2 GetGriddCell             ()
    {
        return Grid.WorldPositionToCell(UtilsClass.GetMouseWorldPosition());
    }
    private void IncreaseCaos               (CardDisplay card)
    {
        Caos.IncrementCaos(card.chaos);
    }
    private void DeseleccionateCard         ()
    {
        cardSelected = false;
        currentCardSelected = null;
    }
    private void ReturnCardToHandAndDeseleccionate()
    {
        Dragg tempDragg = HandManager.ReturnCardSelected().GetComponent<Dragg>();

        if (tempDragg)
        {   tempDragg.ReturnCardToHand();
            DeseleccionateCard();
        }

    }
    private void CreateGrid                 (ref CustomGrid grid,int width, int height, float cellSize)
    {
        grid = new CustomGrid(width, height, cellSize, new Vector3(0, 0),unityGrid, m_TileMapBuilds, m_TileMapOther, m_TileMapTerrain, TileListManager.dictionaryIdTiles);
    }
    private void CreatePathfinding          ()
    {
        PathFinding = new Pathfinding(Grid);
    }
    private void CreateFisicGrid            ()
    {
        unityGrid.cellSize = new Vector3(Grid.GetCellSize(), Grid.GetCellSize(), Grid.GetCellSize());
    }
    private void CreateHandManager          ()
    {
        HandManager = new Hand(handZone, graveryard, canvas, m_maxCardsInHand, cardsInHand, currentCardsInHand);
        //CardsStructures.DrawStartCards();
        CardManager.AddRandomCard(6);
    }
    private void CreateZones                ()
    {
        Vector2 center = new Vector2(Grid.GetWidth() / 2, Grid.GetHeight() / 2);
        zoneEfects.CreateZone(SetCirclePosition(center, 0, 3),   "zona1_Neutra");
        zoneEfects.CreateZone(SetCirclePosition(center, 3, 8),   "zona2_Basica");
        zoneEfects.CreateZone(SetCirclePosition(center, 8, 14),  "zona3_Media");
        zoneEfects.CreateZone(SetCirclePosition(center, 14,19),  "zona4_Avanzada");
    }
    private void CreateEnemyBase            (int Zone)
    {
       List<Vector2> position = zoneEfects.GetZone(Zone).ZonePosition;
       Vector2 randomPos = position[Random.Range(0, position.Count)];
       Grid.SetValue(randomPos, IDConstants.BIG_SPAWNER_ID);
       construction.AddConstrucion(LoadCards.Instance.constructionsDictionary[IDConstants.BIG_SPAWNER_ID], randomPos);
    }
    private void CreateTileInZone     (int Zone,int ammount,int ID)
    {
        List<Vector2> position = zoneEfects.GetZone(Zone).ZonePosition;
        for (int i = 0; i < ammount; i++)
        {
            Vector2 randomPos = position[Random.Range(0, position.Count)];
            Grid.SetValue(randomPos, ID);
            //construction.AddConstrucion(LoadCards.Instance.constructionsDictionary[ID], randomPos);
        }
    }
    public  void SetConstruction            (int id)
    {
        selectdConstruction = id;
    }
    private void ReolvCard                  (Vector2 node, CardDisplay card)
    {
        ////Constructions
        //if (card.cardType == CardType.terrain)
        //{
        //    TerrainDictionary.AddTerrain(card.dataBuild, GetGriddCell());
        //}
        //else
        //{
        if (card.cardType != CardType.extractor)
        {
            Construction.AddConstrucion(card.dataBuild, GetGriddCell());
            if (card.unitData != null)
            {
                Construction.InstantiateAlly(card, GetGriddCell());
            }
        }
        //}
        //Card
        IncreaseCaos(card);
        if (card.feedUseCard != null)
        {
            Instantiate(card.feedUseCard, Grid.GetCenterCell(node), Quaternion.identity);
        }
        //StartCoroutine(CameraShake.Instance.Shake(.15f, .4f));
        CurrencyProduction.Instance.RestCardCostToCurrency(card.cardCost);
        cardSelected = false;
        HandManager.DiscardSelectedCard();
    }
    private void SetAdyacentTiles           (Vector2 node)
    {
        List<Vector2> Pos = Grid.GetCrosNeighbourList(node);

        for (int i = 0; i < Pos.Count; i++)
        {
            if (Grid.GetValue(Pos[i]) == 0)
            {
                Grid.SetValue(Pos[i], 999, false);
            }
        }
    }
    private void TileInteraction            (Vector2 node, CardDisplay card)
    {
        CardEffect.DoCardEffect(card, GetGriddCell());
        switch (card.myPlaceType)
        {
            case PlacementType.anyPlace:
                Debug.Log("Modo de colocacion: "+ card.myPlaceType);
                if (card.cardType != CardType.power)
                {
                    Grid.SetValue(node, card.ID,card.cardType);
                    SetAdyacentTiles(node);
                    ClearAllTileMap(m_TileMapEffects);
                }
                SoundManager.Instance.PlayPlayCard();
                ReolvCard(node, card);
                break;
            case PlacementType.onlyInMeadow:
                Debug.Log("Modo de colocacion: " + card.myPlaceType);
                if (CanBuild(card.cardType, node, card.ID))
                {
                    SoundManager.Instance.PlayPlayCard();
                    if (card.cardType != CardType.power)
                    {
                        Grid.SetValue(node, card.ID, card.cardType);
                        SetAdyacentTiles(node);
                        ClearAllTileMap(m_TileMapEffects);
                    }
                    ReolvCard(node, card);
                }
                break;
            case PlacementType.inMeadowAndAdyacent:
                Debug.Log("Modo de colocacion: " + card.myPlaceType);
                if (CanBuild(card.cardType, node, card.ID) && Grid.isNeighbours(node))
                {
                    SoundManager.Instance.PlayPlayCard();
                    if (card.cardType != CardType.power)
                    {
                        Grid.SetValue(node, card.ID, card.cardType);
                        Debug.Log(card.ID);
                        SetAdyacentTiles(node);
                        ClearAllTileMap(m_TileMapEffects);
                    }
                    ReolvCard(node, card);
                }
                break;
            case PlacementType.onlyInTerrain:
                Debug.Log("Modo de colocacion: " + card.myPlaceType);
                if (CanBuild(card.cardType, node, card.ID) && !IsDrone(node))
                {
                    SoundManager.Instance.PlayPlayCard();
                    if (card.cardType != CardType.power)
                    {
                        Grid.SetValue(node, card.ID, card.cardType);
                        SetAdyacentTiles(node);
                        if(Construction.ReturnConstruction(node) != null)
                        Construction.ReturnConstruction(node).isDroneInConstruction = true;
                        ClearAllTileMap(m_TileMapEffects);
                    }
                    ReolvCard(node, card);
                }
                break;
        }
    }
    public int   CheckAdyacentConstruction  (Vector2 node,int targetID, int adyacentDistance)
    {
        List<Vector2> Positions =  Grid.GetNeighbours(node, adyacentDistance);
        List<int> IDs = new List<int>();
        int n = 0;

        for (int i = 0; i < Positions.Count; i++)
        {
            IDs.Add(Grid.GetValue(Positions[i]));
        }

        for (int i = 0; i < IDs.Count; i++)
        {
            if (IDs[i] == targetID)
            {
                n++;
            }
        }

        return n;
    }
    public void  FindActiveZones            (int ID,TileBase icon)
    {
        List<Vector2> Positions = Grid.FindItemsInGrid(ID);

        for (int i = 0; i < Positions.Count; i++)
        {
            m_TileMapEffects.SetTile(new Vector3Int((int)Positions[i].x, (int)Positions[i].y, 0), icon);
        }
        if (!cardSelected)
        {
            for (int x = 0; x < Grid.GetWidth(); x++)
            {
                for (int y = 0; y < Grid.GetWidth(); y++)
                {
                    if (Grid.GetValue(new Vector2(x, y)) != ID)
                    {
                        m_TileMapEffects.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
            }
        }
    }
    public void  ActiveConstructionZones    (CardDisplay card)
    {
        Debug.Log("CARD IS SELECTED AND WORK");
        int vCardInfo = card.ID;
        int  place = 0;
        bool isInList = false;

        List<int> canBuildZones = new List<int>();

        for (int i = 0; i < TileListManager.RuleListTiles.Count; i++)
        {
            for (int b = 0; b < TileListManager.RuleListTiles[i].Tiles.Count; b++)
            {
                if (tileListManager.dictionaryIdEnum.ContainsKey(vCardInfo) && vCardInfo != 0 && TileListManager.RuleListTiles[i].Tiles[b] == tileListManager.dictionaryIdEnum[vCardInfo])
                {
                    isInList = true;
                    place = i;
                    break;
                }
            }
            if (isInList)
            {
                //Comprobar si tiene alguna norma de colocacion
                for (int d = 0; d < TileListManager.RuleListTiles[place].PlaceOn.Count; d++)
                {
                    int tileID = TileListManager.ReturnKeyID(TileListManager.RuleListTiles[place].PlaceOn[d]);

                    canBuildZones.Add(tileID);
                }
            }
        }

        for (int i = 0; i < canBuildZones.Count; i++)
        {
            FindActiveZones(canBuildZones[i], ActiveZones);
        }
    }
    public void  CreateBase                 ()
    {
        Vector2 center = new Vector2(Grid.GetWidth() / 2, Grid.GetHeight() / 2);
        SoundManager.Instance.PlayPlayCard();
        List<Vector2> pos = Grid.GetConstructionCells(center, 1, 1);
        List<Vector2> cros = new List<Vector2>();
        Grid.SetValue(center, IDConstants.BASE_ID);
        for (int i = 0; i < pos.Count; i++)
        {
            Grid.SetValue(pos[i], IDConstants.BASE_ID, false);
            PathNode baseNode = Grid.GetGridNode((int)pos[i].x, (int)pos[i].y);
            //baseNode.isOcuped = true;
            List<Vector2> cros2 = Grid.GetCrosNeighbourList(pos[i]);
            for (int C = 0; C < cros2.Count; C++)
            {
                cros.Add(cros2[C]);
            }
        }
        for (int b = 0; b < cros.Count; b++)
        {
            if (Grid.GetValue(cros[b]) == 0)
            {
                Grid.SetValue(cros[b], IDConstants.MEADOW_ID);
            }
        }
        for (int f = 0; f < cros.Count; f++)
        {
            SetAdyacentTiles(cros[f]);
        }
        Construction.AddConstrucion(LoadCards.Instance.constructionsDictionary[IDConstants.BASE_ID],center);
    }
    public Vector2 ReturnBasePos            ()
    {
        return Construction.ReturnConstructionPosition(IDConstants.BASE_ID);
    }
    private void SetTileMapToGridTerrain    ()
    {
        Tilemap     tilemap     = m_TileMapTerrain;
        BoundsInt   bounds      = tilemap.cellBounds;
        TileBase[]  allTiles    = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile)
                {
                    int num = int.Parse(tile.name);
                    Grid.SetValue(new Vector2(x, y), num);
                    SetAdyacentTiles(new Vector2(x, y));
                    walkeablesPath.Add(Grid.GetGridNode(x,y));                 
                }
            }
        }

        for (int i = 0; i < walkeablesPath.Count; i++)
        {
            walkeablesPath[i].SetIsWalkable(true);
            walkeablesPath[i].SetIsOcuped(false);
        }
    }
    private void SetTileMapToGridBuilding   ()
    {
        Tilemap tilemap = m_TileMapBuilds;
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile)
                {
                    //BASE
                    int num = int.Parse(tile.name);
                    if (num == 1)
                    {
                        Grid.SetValue(new Vector2(x, y), num);
                        construction.AddConstrucion(LoadCards.Instance.constructionsDictionary[IDConstants.BASE_ID], new Vector2(x, y));
                        Grid.GetGridNode(x, y).SetIsWalkable(false);
                        Grid.SetValue(Grid.GetConstructionCells(new Vector2(x, y),2,2),IDConstants.BASE_ID);
                    }
                }
            }
        }
    }
    private void AnaliceGrid                ()
    {
        //Recorrer la gird
        for (int x = 0; x < Grid.GetWidth(); x++)
        {
            for (int y = 0; y < Grid.GetHeight(); y++)
            {
                //Posibles combos
                for (int i = 0; i < TileListManager.listAdyacenciesCombinatory.Count; i++)
                {
                    //He encontrado un trigger
                    int ID = TileListManager.ReturnKeyID(TileListManager.listAdyacenciesCombinatory[i].tilesRequirement[0]);
                    if (ID == Grid.GetValue(new Vector2(x, y)))
                    {
                        //Comprobar si trigger cumple las condiciones
                        if (IsCombo(new Vector2(x, y), ID))
                        {
                            //Ejecutar resolucin
                            SetComboTiles(m_TileMapTerrain, new Vector2(x, y));
                            Grid.SetValue(Grid.GetConstructionCells(new Vector2(x, y), 2,2), TileListManager.ReturnKeyID(TileListManager.listAdyacenciesCombinatory[i].returnTile));
                            if (TileListManager.listAdyacenciesCombinatory[i].isComboConstruction)
                            {
                                //AQUI VA LA GORDA
                                if (!construction.constructionsDictionary.ContainsKey(new Vector2(x, y)))
                                {
                                    construction.AddConstrucion(LoadCards.Instance.constructionsDictionary[TileListManager.ReturnKeyID(TileListManager.listAdyacenciesCombinatory[i].returnTile)], new Vector2(x, y));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void SetComboTiles              (Tilemap map,Vector2 startPos)
    {
        List<Vector2> Row = Grid.GetConstructionCells(new Vector2(startPos.x, startPos.y), 2, 2);

        for (int i = 0; i < Row.Count; i++)
        {
            map.SetTile(new Vector3Int((int)Row[i].x, (int)Row[i].y, 0),null);
        }
    }
    private bool IsCombo                    (Vector2 startPos,int targetID)
    {
        bool combo = false;

        List<Vector2> Row = Grid.GetConstructionCells(new Vector2(startPos.x, startPos.y), 2, 2);

        for (int i = 0; i < Row.Count; i++)
        {
            if (Grid.GetValue(new Vector2(Row[i].x, Row[i].y)) == targetID)
            {
                combo = true;
            }
            else
            {
                combo = false;
                break;
            }
        }
        return combo;
    }
    private bool CanBuild                   (CardType tileType, Vector2 node, int usedID)
    {
        bool can = true;
        bool isInList = false;
        //Comprobaciones
        bool isAdyacent     = false;
        bool isNotAdyacent  = true;
        bool isPlaceOn      = false;
        int  saceIndex = 0;

        for (int i = 0; i < TileListManager.RuleListTiles.Count; i++)
        {
            if (TileListManager.RuleListTiles[i].ListType == tileType)
            {
                //Comprobar si esta en las reglas el tile que voy a poner
                for (int f = 0; f < TileListManager.RuleListTiles[i].Tiles.Count; f++)
                {
                    int cardID = TileListManager.ReturnKeyID(TileListManager.RuleListTiles[i].Tiles[f]);

                    if (usedID == cardID)
                    {
                        isInList = true;
                        saceIndex = i;
                        break;
                    }
                }

                if (isInList)
                {
                    //Comprobar si tiene alguna norma de colocacion
                    for (int d = 0; d < TileListManager.RuleListTiles[i].PlaceOn.Count; d++)
                    {
                        int tileID = TileListManager.ReturnKeyID(TileListManager.RuleListTiles[i].PlaceOn[d]);
                        if (Grid.GetValue(node) == tileID)
                        {
                            isPlaceOn = true;
                            Debug.Log("Hay una norma de colocacion: " + tileID + " / " + "El tile clickado es: "+Grid.GetValue(node));
                            break;
                        }
                    }
                    if (TileListManager.RuleListTiles[i].PlaceOn.Count == 0)
                    {
                        isPlaceOn = true;
                    }
                    //Comprobar si tengo alguna proibicion de adyacencia
                    for (int b = 0; b < TileListManager.RuleListTiles[i].NotAdyacents.Count; b++)
                    {
                        int A = TileListManager.ReturnKeyID(TileListManager.RuleListTiles[i].NotAdyacents[b]);
                        if (ChekAdyacentNode(node, A))
                        {
                            isNotAdyacent = false;
                            Debug.Log("Hay una norma de adyacencia: "+ A);
                            break;
                        }
                    }
                    //Comprobar si se esta poniendo en adyacente
                    if (TileListManager.RuleListTiles[saceIndex].isAdyacentType)
                    {
                        if (Grid.GetValue(node) == 999)
                        {
                            isAdyacent = true;
                            Debug.Log("El tiele se esta intentado poner encima de un adyacente");
                            break;
                        }
                    }
                }
            }
        }
        if (isNotAdyacent && (isPlaceOn || isAdyacent))
        {
            can = true;
        }
        else
        {
            can = false;
        }
        Debug.Log("NO: " + isNotAdyacent +" ON; "+ isPlaceOn + " AD: " + isAdyacent);
        return can;
    }
    public List<Vector2> SetCirclePosition  (Vector2 node, int minDistance, int maxDistance)
    {
        List<Vector2> range = Grid.GetNeighbours(node, (maxDistance));
        List<Vector2> circleRange = new List<Vector2>();
        //Logica
        int minDistX  = (int)node.x + minDistance;
        int maxDistX = (int)node.x + (maxDistance);

        int minDistNX = (int)node.x - minDistance;
        int maxDistNX = (int)node.x - (maxDistance);

        int minDistY  = (int)node.y + minDistance;
        int maxDistY = (int)node.y + (maxDistance);

        int minDistNY = (int)node.y - minDistance;
        int maxDistNY = (int)node.y - (maxDistance);

        for (int i = 0; i < range.Count; i++)
        {
            if ((range[i].x >= minDistX  && range[i].x <= maxDistX)  ||
                (range[i].x <= minDistNX && range[i].x >= maxDistNX) || 
                (range[i].y >= minDistY  && range[i].y <= maxDistY)  || 
                (range[i].y <= minDistNY && range[i].y >= maxDistNY)) 
            {
                circleRange.Add(new Vector2(range[i].x, range[i].y));
            }
        }

        return circleRange;

    }
    public void  CreateInCircle             (Vector2 node, int minDistance,int maxDistance,int spawnID,int ammount)
    {
        List<Vector2> pos = SetCirclePosition(node,minDistance, maxDistance);
        List<Vector2> randomPos = new List<Vector2>();

        for (int i = 0; i < ammount;)
        {
            if (ammount > pos.Count)
            {
                Debug.Log("El numero de objetos a invocar es mayor que el numero de casillas disponible");
                break;
            }
            else
            {
                Vector2 getRandom = pos[Random.Range(0, pos.Count)];
                if (!randomPos.Contains(getRandom))
                {
                    randomPos.Add(getRandom);
                    i++;
                }
            }
        }

    }
    public void  CreateInCircle             (List<Vector2> node, int spawnID, int ammount)
    {
        List<Vector2> pos = node;

        for (int i = 0; i < ammount;)
        {
            if (ammount > pos.Count)
            {
                Debug.Log("El numero de objetos a invocar es mayor que el numero de casillas disponible");
                break;
            }
            else
            {
                Vector2 getRandom = pos[Random.Range(0, pos.Count)];
                if (Grid.GetValue(getRandom) == 0)
                {
                    Grid.SetValue(getRandom, spawnID);
                }
            }
        }

    }
    //ESTO YA EXISTE BORRAR MAS TARDE
    private bool ChekAdyacentNode           (Vector2 node, int targetID)
    {
        List<Vector2> pos = Grid.GetCrosNeighbourList(node);
        bool r = false;
        for (int i = 0; i < pos.Count; i++)
        {
            if (Grid.GetValue(pos[i]) == targetID)
            {
                r = true;
                break;
            }
        }
        return r;
    }
    public void CreateMeadow(int amount)
    {
        Vector2 center = new Vector2(Grid.GetWidth() / 2, Grid.GetHeight() / 2);
        GetFreeCell(center, gridWidth / 2, IDConstants.MEADOW_ID, amount);
    }
    private void GetFreeCell                (Vector2 node, int Distance, int spawnID,int amount)
    {
        List<Vector2> pos = Grid.GetNeighbours(node, Distance);
        List<Vector2> freePos = new List<Vector2>();

        for (int i = 0; i < amount; i++)
        {
            for (int b = 0; b < pos.Count; b++)
            {
                if (Grid.GetValue(pos[b]) == 999)
                {
                    freePos.Add(pos[b]);
                }
            }
            if(freePos.Count > 0)
            {
                Vector2 getRandom = freePos[Random.Range(0, freePos.Count)];
                if (pos.Contains(getRandom))
                {
                    Grid.SetValue(getRandom, spawnID);
                    SetAdyacentTiles(getRandom);
                }
            }
        }
    }
    //-----GET/SET------
    public CardManager                  CardManager             { get => m_CardManager;         set => m_CardManager            = value; }
    public CardStructuresManager        CardsStructures         { get => cardsStructures;       set => cardsStructures          = value; }
    public ConstructionProduction       Construction            { get => construction;          set => construction             = value; }
    public CaosSystem                   Caos                    { get => m_caos;                set => m_caos                   = value; }
    public ProductionBuildingsFeedback  Feedback                { get => feedback;              set => feedback                 = value; }
    public UIManager                    UIManager               { get => uIManager;             set => uIManager                = value; }
    public Timer                        Time                    { get => m_Time;                set => m_Time                   = value; }
    public Hand                         HandManager             { get => handManager;           set => handManager              = value; }
    public Pathfinding                  PathFinding             { get => pathFinding;           set => pathFinding              = value; }
    public CustomGrid                   Grid                    { get => m_grid;                set => m_grid                   = value; }
    public CardEffect                   CardEffect              { get => cardEffect;            set => cardEffect               = value; }
    public TerrainDictionary            TerrainDictionary       { get => terrainDictionary;     set => terrainDictionary        = value; }
    public TileListManager              TileListManager         { get => tileListManager;       set => tileListManager          = value; }
}