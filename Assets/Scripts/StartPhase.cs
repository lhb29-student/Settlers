using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPhase : MonoBehaviour
{
    
    private int currentPlayer;
    private int playerTurn;
    private int playerCount = 0;

    public bool settlementPlaced = false;
    public bool roadPlaced = false;
    private bool firstRound = true;
    private bool settingUp = true;
    private bool repeatSetup = true;

    private GameManager gm;
    private BPmanager bp;


    void Start()
    {
        gm = GetComponent<GameManager>();
        bp = GetComponent<BPmanager>();

        // checker to track how many players have placed their settlements
        playerCount++;

        // set starting player
        playerTurn = UnityEngine.Random.Range(0, 4);
        // chosen player start
        StartCoroutine(PlaceGameObject());
    }

    void Update()
    {
        // current player should be same as player's turn
        if (currentPlayer != playerTurn)
        {
            Debug.Log("Player: " + currentPlayer);
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
                StartCoroutine(PlaceGameObject());

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
                    Debug.Log("playerTurn:"+playerTurn);
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


        // fail safe for second round
        if (currentPlayer == 0)
        {
            currentPlayer = 4;
            playerTurn = 4;
        }
    }

    IEnumerator PlaceGameObject()
    {
        // +1 since there is no colorcode for 0
        gm.setColorCode(playerTurn + 1);

        // next player to make a move
        currentPlayer = playerTurn;
        Debug.Log("Player " + playerTurn + "'s turn");

        // pick settlement
        bp.setupSettlement();

        yield return null;
    }
}
