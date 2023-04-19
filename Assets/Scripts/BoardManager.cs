using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    
    [SerializeField] private int currentPlayer;
    [SerializeField] private int playerTurn;
    [SerializeField] private int playerCount = 0;
    [SerializeField] private int p4Settlement = 0;
    [SerializeField] public int playersAllocated = 0;
    [SerializeField] private int totalRoadsPlaced = 0;
    [SerializeField] private int rotatePlayer;
    [SerializeField] private GameObject allRoads;

    public bool settlementPlaced = false;
    public bool roadPlaced = false;
    private bool firstRound = true;
    private bool settingUp = true;
    private bool repeatSetup = true;
    public bool gameStart = false;

    private GameManager gm;
    private BPmanager bPManager;
    public Vector3 housePos;

    public int diceNum;
    public List<Hex> hexes;
    public List<Road> roads;
    

    // accessed by ai script
    public string currentP;
    public bool canRoll = true;
    public bool moveBandit = false;


    void Start()
    {
        moveBandit = false;

        gm = GetComponent<GameManager>();
        bPManager = GetComponent<BPmanager>();
        hexes = new List<Hex>();
        
        // add all hex into list
        foreach (Transform child in transform)
        {
            if (child.tag == "Hex")
            {
                hexes.Add(child.GetComponent<Hex>());
            }
        }

        foreach (Transform child in allRoads.transform)
        {
            if (child.GetComponent<Road>() != null)
            {
                roads.Add(child.GetComponent<Road>());
            }
        }

        // checker to track how many players have placed their settlements
        playerCount++;

        // set starting player
        playerTurn = Random.Range(0, 4);
        // chosen player start
        StartCoroutine(PlaceGameObject());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextPlayer();
        }

        if (gameStart == true)
        {
            currentP = "p" + ((rotatePlayer % 4) + 1);
        }
        else
        {
            // debug find player
            currentP = "p" + ((currentPlayer % 4) + 1);
        }

        // for testing
        if (Input.GetKeyDown(KeyCode.P))
        {
            RollDice();
        }

        // current player should be same as player's turn
        if (currentPlayer != playerTurn)
        {
            //Debug.Log("Player: " + currentPlayer);
            //Debug.Log("Code: " + gm.getColorCode());
            // next player start
            StartCoroutine(PlaceGameObject());
        }

        if (settlementPlaced == true && roadPlaced == true && settingUp)
        {
            // first round
            if (firstRound)
            {
                settlementPlaced = false;
                roadPlaced = false;
                // next player
                playerTurn++;
                // alternate between 4 players
                playerTurn = playerTurn % 4;

                playerCount++;
            }

            // second round
            // last player places again and player's turns goes the other way round
            else
            {
                // for last player to place another settlement
                if (repeatSetup)
                {
                    settlementPlaced = false;
                    roadPlaced = false;
                    StartCoroutine(PlaceGameObject());

                    repeatSetup = false;

                    p4Settlement++;
                    if (p4Settlement == 2)
                    {
                        StartCoroutine(PlaceGameObject());
                    }
                }
                // player turns goes the other way round
                else
                {
                    settlementPlaced = false;
                    roadPlaced = false;
                    StartCoroutine(PlaceGameObject());
                    // next player
                    playerTurn--;
                    // alternate between 4 players
                    playerTurn = Mathf.Abs(playerTurn % 4);

                    AllocateStartingResource();
                    //playersAllocated++;
                }

                // tracker
                playerCount--;
            }
        }

        // first round placement over
        if (playerCount == 4)
        {
            firstRound = false;
        }

        // setup phase finish
        if (playerCount == 0)
        {
            settingUp = false;
        }

        // allocate resource for final player
        if (!settingUp && playersAllocated == 8)
        {
            rotatePlayer = currentPlayer;

            Debug.Log("final player");
            playersAllocated++;
            AllocateStartingResource();

            // game starts once starting roads are placed
            if (totalRoadsPlaced >= 8)
            {
                gameStart = true;
            }
        }

        if (gameStart == false)
        {
            // counts roads placed until all players are done placing
            RoadCheck();
        }

        // fail safe for second round
        if (currentPlayer == 0)
        {
            currentPlayer = 4;
            playerTurn = 4;
        }

        // testing
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FindPlayer();
        }
    }

    // called by aiscript
    public void NextPlayer()
    {
        gameStart = true;
        canRoll = true;
        rotatePlayer++;
        //Debug.Log("Next player");
    }

    public void RollDice()
    {
        // allocate resource when dice is rolled
        diceNum = GetDiceRoll();
        
        // move robber if rolled 7
        if (diceNum == 7)
        {
            //Debug.Log("Move bandit");
            moveBandit = true;
            List<PlayerResources> playResources = new List<PlayerResources>();
            List<AIScript> aIScripts = new List<AIScript>();
            GameObject allPlayers = GameObject.Find("Players");

            foreach(Transform child in allPlayers.transform)
            {
                playResources.Add(child.GetComponent<PlayerResources>());
                if (child.GetComponent<AIScript>() != null)
                {
                    aIScripts.Add(child.GetComponent<AIScript>());
                    child.GetComponent<AIScript>().checkCards = true;
                }

                Debug.Log("aiscript count: " + aIScripts.Count);
                Debug.Log("Resource count: " + child.GetComponent<PlayerResources>().GetTotalCards());
            }
        }
        else
        {
            AllocateResource();
        }
    }
    public void RoadCheck()
    {
        totalRoadsPlaced = 0;

        foreach (Road road in roads)
        {
            if(road.GetPlayer().ToString() != "np")
            {
                totalRoadsPlaced++;
            }
        }
    }

    IEnumerator PlaceGameObject()
    {
        // +1 since there is no colorcode for 0
        gm.setColorCode(playerTurn + 1);

        // failsafe for when colorcode is 5
        if (playerTurn + 1 == 5)
        {
            gm.setColorCode(1);
        }

        // next player to make a move
        currentPlayer = playerTurn;

        // pick settlement
        bPManager.setupSettlement();

        yield return null;
    }

    // called during setup phase
    public void AllocateStartingResource()
    {
        // create sphere to find adjacent tiles
        Collider[] collider = Physics.OverlapSphere(housePos, 1f);
        // find player
        GameObject chosenPlayer = GameObject.FindGameObjectWithTag(FindPlayer());

        if (playersAllocated == 4)
        {
            chosenPlayer = GameObject.FindGameObjectWithTag(FinalPlayer());
            playersAllocated++;
        }
        //Debug.Log(chosenPlayer);

        // each tile gives corrensponding resource
        foreach (var col in collider)
        {
            // finds only the hexes within the collider radius
            if (col.tag == "Hex")
            {
                // access hex script
                var hex = col.GetComponent<Hex>();
                // allocate appropriate resource
                FindResource(hex.GetTerrainType(), chosenPlayer);
            }
        }
    }

    // allocate resource based on number rolled by dice
    public void AllocateResource()
    {
        // find hex with corresponding number
        foreach (Hex hex in hexes)
        {
            // find hexes with same dice number
            if (hex.getTokenNumber() == diceNum)
            {
                // find all intersects on hex
                foreach (Intersect intersect in hex.GetIntersectList())
                {
                    // find owned intersects and check robber
                    if (intersect.GetPlayer().ToString() != "np" && hex.isRobberHere() == false)
                    {
                        // find player
                        GameObject chosenPlayer = GameObject.FindGameObjectWithTag(intersect.GetPlayer().ToString());
                        FindResource(hex.GetTerrainType(), chosenPlayer);
                    }
                }
            }
        }
    }

    // determines which resource to allocate
    // parameters is the type of terrain and which player to allocate
    public void FindResource(string terrainType, GameObject chosenPlayer)
    {
        if (terrainType == "Hills")
        {
            chosenPlayer.GetComponent<PlayerResources>().AddBrick();
        }
        else if (terrainType == "Forest")
        {
            chosenPlayer.GetComponent<PlayerResources>().AddWood();
        }
        else if (terrainType == "Mountains")
        {
            chosenPlayer.GetComponent<PlayerResources>().AddOre();
        }
        else if (terrainType == "Fields")
        {
            chosenPlayer.GetComponent<PlayerResources>().AddWheat();
        }
        else if (terrainType == "Pasture")
        {
            chosenPlayer.GetComponent<PlayerResources>().AddWool();
        }
    }

    // generate player tag
    public string FindPlayer()
    {
        string p = "p";
        string num = gm.getColorCode().ToString();
        //Debug.Log(p + num);
        return p + num;
    }

    // last player to place during setup
    public string FinalPlayer()
    {
        string p = "p";
        int colorNum = gm.getColorCode();
        colorNum++;
        string num = colorNum.ToString();
        return p + num;
    }

    // get dice roll
    public int GetDiceRoll()
    {
        return GameObject.Find("Canvas").GetComponent<DiceRoll>().rollDice();
    }
}
