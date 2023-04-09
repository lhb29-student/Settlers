using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    
    private int currentPlayer;
    private int playerTurn;
    private int playerCount = 0;
    private int p4Settlement = 0;
    public int playersAllocated = 0;

    public bool settlementPlaced = false;
    public bool roadPlaced = false;
    private bool firstRound = true;
    private bool settingUp = true;
    private bool repeatSetup = true;

    private GameManager gm;
    private BPmanager bPManager;
    public Vector3 housePos;

    public int diceNum;
    public List<Hex> hexes;


    void Start()
    {
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

        // checker to track how many players have placed their settlements
        playerCount++;

        // set starting player
        playerTurn = Random.Range(0, 4);
        // chosen player start
        StartCoroutine(PlaceGameObject());
    }

    void Update()
    {
        // for testing
        if (Input.GetKeyDown(KeyCode.P))
        {
            // allocate resource when dice is rolled
            diceNum = GetDiceRoll();
            AllocateResource();
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
            Debug.Log("final player");
            playersAllocated++;
            AllocateStartingResource();
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
                    // find owned intersects
                    if (intersect.GetPlayer().ToString() != "np")
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
