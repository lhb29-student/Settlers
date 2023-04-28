using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    
    [SerializeField] private int currentPlayer; // id of current player
    [SerializeField] private int playerTurn; // id of current player
    [SerializeField] private int playerCount = 0; // players that already placed starting settlement
    [SerializeField] private int p4Settlement = 0; // used for allowing final player to place twice
    [SerializeField] private int totalRoadsPlaced = 0; // total starting roads
    [SerializeField] private GameObject allRoads; // all roads
    // UI fields
    [SerializeField] private GameObject playerIndicator;
    [SerializeField] private GameObject nextPlayerButton;
    [SerializeField] private GameObject diceButton;
    [SerializeField] private GameObject debugMenu;
    [SerializeField] private TMPro.TextMeshProUGUI diceText;
    
    private bool firstRound = true; // first round of setup
    private bool settingUp = true; // is setup phase
    private bool repeatSetup = true; // second round of setup
    // scripts
    private GameManager gm;
    private BPmanager bPManager;

    public int playersAllocated = 0; // players already placed starting settlement
    public int rotatePlayer; // rotate between 4 players
    public int diceNum; // dice num
    public bool settlementPlaced = false; // has placed starting settlement
    public bool roadPlaced = false; // has placed starting road
    public bool gameStart = false; // setup finish
    public bool getDice = false; // has dice number been generated
    public Vector3 housePos; // location of starting settlement placed
    public List<Hex> hexes; // list of all hexes
    public List<Road> roads; // list of all roads

    // accessed by ai script
    public string currentP; // current player, tag
    public bool canRoll = true; // can ai roll dice
    public bool moveBandit = false; // can ai movebandit
    public bool playerCheckCards = false; // is 7 rolled


    void Start()
    {
        Debug.Log("debug menu has been hidden by default, press f1 to hide/unhide menu");

        // init
        moveBandit = false;
        // init
        gm = GetComponent<GameManager>();
        bPManager = GetComponent<BPmanager>();
        debugMenu = GameObject.Find("Debug");
        hexes = new List<Hex>();
        diceText.text = diceNum.ToString();
        // hide debug menu on start
        debugMenu.SetActive(false);

        // add all hex into list
        foreach (Transform child in transform)
        {
            if (child.tag == "Hex")
            {
                hexes.Add(child.GetComponent<Hex>());
            }
        }
        // add all roads into list
        foreach (Transform child in allRoads.transform)
        {
            if (child.GetComponent<Road>() != null)
            {
                roads.Add(child.GetComponent<Road>());
            }
        }

        // checker to track how many players have placed their settlements
        playerCount++;

        // set starting player, a random number is chosen and the matching player starts
        playerTurn = Random.Range(0, 4);
        // chosen player start
        StartCoroutine(PlaceGameObject());
    }

    void Update()
    {
        // display dice number on ui
        diceText.text = diceNum.ToString();

        // has setup finished
        if (gameStart == true)
        {
            // generate current player tag
            currentP = "p" + ((rotatePlayer % 4) + 1);
        }
        else
        {
            // debug find player
            currentP = "p" + ((currentPlayer % 4) + 1);
        }

        // current player should be same as player's turn
        if (currentPlayer != playerTurn)
        {
            //Debug.Log("Player: " + currentPlayer);
            //Debug.Log("Code: " + gm.getColorCode());
            // next player start
            StartCoroutine(PlaceGameObject());
        }

        // setup algorithm
        if (settlementPlaced == true && roadPlaced == true && settingUp)
        {
            // first round
            // players rotate normally during the first round
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
                // if last player has not placed second settlement
                if (repeatSetup)
                {
                    settlementPlaced = false;
                    roadPlaced = false;
                    // place settlement coroutine
                    StartCoroutine(PlaceGameObject());

                    repeatSetup = false;

                    p4Settlement++;
                    // last player gets to place twice
                    if (p4Settlement == 2)
                    {
                        StartCoroutine(PlaceGameObject());
                    }
                }
                // player turns goes the other way round, instead of going up it goes down to rotate backwards
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
        // counts roads as long as setup has not finished
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

        MoveIndicator();
        UIController();

        // toggle debug menu
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (debugMenu.activeSelf == true)
            {
                debugMenu.SetActive(false);
            }
            else
            {
                debugMenu.SetActive(true);
            }
        }

        // testing
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject.Find("Land").GetComponent<tokenManager>().availableRobberSpace();
            //FindPlayer();
        }
        
        // testing
        if (Input.GetKeyDown(KeyCode.P))
        {
            canRoll = true;
            RollDice();
        }
    }

    // this method controls ui behaviour
    public void UIController()
    {
        // dice button is set to half transparent if player cannot roll
        if (currentP == "p1")
        {
            // player can roll
            if (canRoll == true && totalRoadsPlaced >= 8)
            {
                diceButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            // player cannot roll
            else
            {
                diceButton.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            }
        }
        // player cannot roll during setup
        else
        {
            diceButton.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }

        // skip turn button set to normal transparency when player allowed to end turn
        if (canRoll == false && totalRoadsPlaced >= 8)
        {
            // player can end turn
            if (currentP == "p1")
            {
                nextPlayerButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }
        // player still has moves, e.g. roll dice
        else
        {
            nextPlayerButton.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }

    // moves indicator to show which player's taking their turn, white knob beside player indicator
    public void MoveIndicator()
    {
        // definitely not a good idea to have fixed coordinates
        if (currentP == "p1")
        {
            playerIndicator.transform.position = new Vector3(1150, 655, 0);
        }
        else if (currentP == "p2")
        {
            playerIndicator.transform.position = new Vector3(1150, 615, 0);
        }
        else if (currentP == "p3")
        {
            playerIndicator.transform.position = new Vector3(1150, 575, 0);
        }
        else if (currentP == "p4")
        {
            playerIndicator.transform.position = new Vector3(1150, 535, 0);
        }
    }

    // called by aiscript
    public void NextPlayer()
    {
        // only allowed if dice is rolled, mainly for user since ai routine guarantees a roll before ending
        if (canRoll == false && moveBandit == false)
        {
            Debug.Log(currentP + " ended their turn");
            gameStart = true;
            canRoll = true;
            rotatePlayer++;
            //Debug.Log("Next player");
        }
        else
        {
            Debug.Log("Dice not rolled");
        }
    }

    // called by player, through dice roll button
    public void PlayerRoll()
    {
        // this prevents player from rolling multiple times in one round
        if (canRoll == true)
        {
            canRoll = false;

            // allocate resource when dice is rolled
            diceNum = GetDiceRoll();

            // move robber if rolled 7
            if (diceNum == 7)
            {
                //Debug.Log("Move bandit");
                // init
                List<PlayerResources> playResources = new List<PlayerResources>(); // access to check all player resources
                List<AIScript> aIScripts = new List<AIScript>(); // if player is ai
                GameObject allPlayers = GameObject.Find("Players"); // all players
                tokenManager tokenM = GameObject.Find("Land").GetComponent<tokenManager>(); // token manager

                // move robber method
                tokenM.availableRobberSpace();
                moveBandit = true;
                playerCheckCards = true; // check user cards

                foreach (Transform child in allPlayers.transform)
                {
                    // get each player's resources
                    playResources.Add(child.GetComponent<PlayerResources>());
                    // separate script is called if player is an ai
                    if (child.GetComponent<AIScript>() != null)
                    {
                        // for debug to check if ai script is properly found and accessed
                        aIScripts.Add(child.GetComponent<AIScript>());
                        // call ai checkcards script
                        child.GetComponent<AIScript>().checkCards = true;
                    }
                    //Debug.Log("aiscript count: " + aIScripts.Count);
                    //Debug.Log("Resource count: " + child.GetComponent<PlayerResources>().GetTotalCards());
                }
            }
            else
            {
                // allocate appropriate resource
                AllocateResource();
            }
        }
        else
        {
            Debug.Log("Already rolled dice");
        }
    }

    public void RollDice()
    {
        // allocate resource when dice is rolled
        diceNum = GetDiceRoll();

        // move robber if rolled 7
        if (diceNum == 7)
        {
            //Debug.Log("Move bandit");
            // initialize fields and lists
            List<PlayerResources> playResources = new List<PlayerResources>(); // access to check all player resources
            List<AIScript> aIScripts = new List<AIScript>(); // if player is ai
            GameObject allPlayers = GameObject.Find("Players"); // all players
            tokenManager tokenM = GameObject.Find("Land").GetComponent<tokenManager>(); // token manager

            // move robber method
            tokenM.availableRobberSpace();
            moveBandit = true;

            foreach (Transform child in allPlayers.transform)
            {
                // get each player's resources
                playResources.Add(child.GetComponent<PlayerResources>());
                // separate script is called if player is an ai
                if (child.GetComponent<AIScript>() != null)
                {
                    // for debug to check if ai script is properly found and accessed
                    aIScripts.Add(child.GetComponent<AIScript>());
                    // call ai checkcards script
                    child.GetComponent<AIScript>().checkCards = true;
                }
                //Debug.Log("aiscript count: " + aIScripts.Count);
                //Debug.Log("Resource count: " + child.GetComponent<PlayerResources>().GetTotalCards());
            }
        }
        else
        {
            // allocate appropriate resource
            AllocateResource();
        }
    }

    // count how many roads have been placed, helps with determining setup phase
    public void RoadCheck()
    {
        // reinitialise field
        totalRoadsPlaced = 0;

        // finds all placed roads and add to count
        foreach (Road road in roads)
        {
            // finds all owned roads, np as in no player owns
            if(road.GetPlayer().ToString() != "np")
            {
                totalRoadsPlaced++;
            }
        }
    }

    // place settlement
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

        // sets final player to place during first round of setup
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
            // find all hexes with same dice number
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
            chosenPlayer.GetComponent<PlayerResources>().AddBrick(1);
        }
        else if (terrainType == "Forest")
        {
            chosenPlayer.GetComponent<PlayerResources>().AddWood(1);
        }
        else if (terrainType == "Mountains")
        {
            chosenPlayer.GetComponent<PlayerResources>().AddOre(1);
        }
        else if (terrainType == "Fields")
        {
            chosenPlayer.GetComponent<PlayerResources>().AddWheat(1);
        }
        else if (terrainType == "Pasture")
        {
            chosenPlayer.GetComponent<PlayerResources>().AddWool(1);
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
        return GameObject.Find("Land").GetComponent<DiceRoll>().rollDice();
    }
}
