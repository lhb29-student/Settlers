using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AIScript : MonoBehaviour
{

    [SerializeField] private string playerTag; // player tag
    [SerializeField] private int playerColor; // player color code
    [SerializeField] private int chooseInter; // ai choose random intersect

    [SerializeField] private bool isBusy = false; // can ai run setup script
    [SerializeField] private bool diceRolled = false; // rolled dice
    [SerializeField] private bool placedRoads = false; // placed roads
    [SerializeField] private bool placedSettlements = false; // placed settlements
    [SerializeField] private bool upgradedCity = false; // upgraded city
    [SerializeField] private bool routineStart = false; // is ai coroutine running
    // scripts
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private BPmanager bPmanager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerResources playerResources;
    [SerializeField] private List<GameObject> intersects; // list of intersects
    [SerializeField] private GameObject allIntersects; // all intersects

    public bool checkCards = false; // modified by board manager

    public BoardPiece bp; 
    public GameObject inter;


    void Start()
    {
        // player tag determined by user tag
        playerTag = gameObject.tag;
        allIntersects = GameObject.Find("IntersectPoints");

        // set player color code
        playerColor = int.Parse(playerTag.Substring(1));

        // initialise managers
        boardManager = GameObject.FindGameObjectWithTag("Land").GetComponent<BoardManager>();
        gameManager = GameObject.FindGameObjectWithTag("Land").GetComponent<GameManager>();
        bPmanager = GameObject.FindGameObjectWithTag("Land").GetComponent<BPmanager>();
        playerResources = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerResources>();

        intersects = new List<GameObject>(); // list to store all available intersects, this is for the ai to determine settlement locations

        // all intersects are added into list
        foreach (Transform child in allIntersects.transform)
        {
            intersects.Add(child.gameObject);
        }
    }

    void FixedUpdate()
    {
        // not sure if fixedupdate works but update running too frequently sometimes causes bugs

        // detect if ai player's turn
        if (boardManager.currentP == playerTag)
        {
            //Debug.Log(playerTag + "'s turn");

            // prevent coroutine from running x times
            if (isBusy == false)
            {
                // ai setup routine
                StartCoroutine(ChooseStarter());
            }
            // ai runs game routine after setup
            if (routineStart == false && boardManager.gameStart == true)
            {
                StartCoroutine(AIRoutine());
            }
        }

        // if 7 is rolled ai is prompted to check cards
        if (checkCards == true)
        {
            CheckCards();

            // if it's currently the ai's turn it then moves the bandit
            if (boardManager.currentP == playerTag)
            {
                // ai move bandit
                if (boardManager.moveBandit == true)
                {
                    // move bandit
                    PickRobberLocation();
                }
            }
        }
    }

    // a coroutine so that each move doesnt happen in an instant
    IEnumerator AIRoutine()
    {
        routineStart = true;

        // roll dice
        if (boardManager.canRoll && boardManager.gameStart == true)
        {
            boardManager.canRoll = false;
            StartCoroutine(RollDice());
        }

        // padding added so that everything doesnt happen in an instant
        yield return new WaitForSeconds(0.5f);

        // move bandit
        float waitTime = 0.5f;
        if (boardManager.moveBandit == true)
        {
            boardManager.moveBandit = false;
            //Debug.Log("Bandit moved by " + playerTag);
        }
        // this wait time is skipped if bandit is not required to be moved
        else
        {
            waitTime = 0;
        }

        yield return new WaitForSeconds(waitTime);

        // ai checks requirements to build settlement
        CheckSettlements();

        yield return new WaitForSeconds(0.5f);

        // ai checks requirements to upgrade to city
        UpgradeCity();

        yield return new WaitForSeconds(0.5f);

        // ai attempts to build road with remaining resources
        CheckRoads();

        yield return new WaitForSeconds(0.5f);

        // round end
        // all moves finished, ai tells board manager to end turn
        if (diceRolled == true && placedRoads == true && placedSettlements == true && upgradedCity == true)
        {
            //Debug.Log(playerTag + " ended turn");
            boardManager.NextPlayer();
            ResetStates();
        }

        yield return new WaitForSeconds(0.5f);

        routineStart = false;
    }

    // reset ai bool states
    public void ResetStates()
    {
        diceRolled = false;
        placedRoads = false;
        placedSettlements = false;
        upgradedCity = false;
    }

    // place roads if enough resource
    public void CheckRoads()
    {
        // set color code
        gameManager.setColorCode(((playerColor) + 1) % 4);

        // build road if requirements are met
        if (playerResources.CheckRoad() == true)
        {
            // placeroad if available
            bPmanager.placeRoad();
        }
        else
        {
            Debug.Log(playerTag + " not enough resources for road");
        }

        placedRoads = true;
    }

    // place new settlements
    public void CheckSettlements()
    {

        // bug: settlement color doesnt change according to player color


        // set color code
        gameManager.setColorCode(((playerColor) + 1) % 4);

        // build settlement if requirements are met
        if (playerResources.CheckSettlement() == true)
        {
            // buildsettlement if available
            bPmanager.buildSettlement();
            // possible addition of aiclick
        }
        else
        {
            Debug.Log(playerTag + " not enough resources for settlement");
        }

        // change state
        placedSettlements = true;
    }

    // upgrade settlement to city
    public void UpgradeCity()
    {
        // set color code
        gameManager.setColorCode(((playerColor) + 1) % 4);

        // upgrade city if requirements are met
        if (playerResources.CheckCity() == true)
        {
            // determine random settlement to upgrade
            Debug.Log(playerTag + " upgraded a settlement");
            bPmanager.setToCity();

            // create list to store available locations
            List<GameObject> availableLocations = new List<GameObject>();

            // find all active cylinders
            foreach (GameObject availablePick in GameObject.FindGameObjectsWithTag("Intersect"))
            {
                // only get player controlled settlements
                if (availablePick.GetComponent<Intersect>().GetPlayer().ToString() == playerTag)
                {
                    // add to list
                    availableLocations.Add(availablePick);
                }
            }

            if (availableLocations.Count > 0)
            {
                Debug.Log("available city locations: " + availableLocations.Count);
                // choose random hex
                int choosePick = UnityEngine.Random.Range(0, availableLocations.Count);
                // find chosen hex position
                Vector3 chooseLocation = availableLocations[choosePick].transform.position;
                // simulate click at position
                gameManager.AIClick(chooseLocation);
            }
            else
            {
                Debug.Log("no locations available for city");
            }
        }

        // change state
        upgradedCity = true;
    }

    // check if cards are more than 7
    public void CheckCards()
    {
        checkCards = false;
        var resources = GameObject.FindWithTag(playerTag).GetComponent<PlayerResources>(); // finds resource script for player

        // remove cards if more than 7
        if (resources.GetTotalCards() > 7)
        {
            //Debug.Log(playerTag + " has more than 7 cards");
            resources.DiscardResource();
        }
        // do nothing
        else
        {
            //Debug.Log(playerTag + " card amount: " + resources.GetTotalCards());
        }
    }

    // ai picks random location for robber
    // index out of range errors present but the code works
    public void PickRobberLocation()
    {
        // create list to store available locations
        List<GameObject> availableLocations = new List<GameObject>();

        // find all active cylinders
        foreach (GameObject availableRob in GameObject.FindGameObjectsWithTag("moveRob"))
        {
            // add to list
            availableLocations.Add(availableRob);
        }

        // choose random hex
        int chooseRob = UnityEngine.Random.Range(0, availableLocations.Count);
        // find chosen hex position
        Vector3 chooseLocation = availableLocations[chooseRob].transform.position;
        // simulate click at position
        gameManager.AIClick(chooseLocation);
    }
     
    // dice rolling
    IEnumerator RollDice()
    {
        // roll dice from board manager
        diceRolled = true;
        boardManager.RollDice();

        yield return null;
    }

    // ai picks starter settlements
    IEnumerator ChooseStarter()
    {
        isBusy = true;

        // choose a random intersect
        chooseInter = UnityEngine.Random.Range(0, intersects.Count);
        // pick position of chosen intersect
        Vector3 chooseLocation = intersects[chooseInter].transform.position;

        // simulate click at chosen location
        gameManager.AIClick(chooseLocation);

        // list to store roads
        List<GameObject> nearbyRoads = new List<GameObject>();

        // collider to detect nearby gameobjects
        Collider[] colliders = Physics.OverlapSphere(chooseLocation, 0.5f);

        // anything caught within the sphere created
        foreach (var collider in colliders)
        {
            if (collider.tag == "Road")
            {
                // add road into list
                nearbyRoads.Add(collider.gameObject);
            }
        }

        // choose random adjacent road
        int chooseRoad = UnityEngine.Random.Range(0, nearbyRoads.Count);

        // next click location at chosen road
        chooseLocation = nearbyRoads[chooseRoad].transform.position;

        // simulate ai click at chosen location
        gameManager.AIClick(chooseLocation);

        // failsafe since someitmes ai picks player's settlement
        yield return new WaitForSeconds(0.5f);

        isBusy = false;
    }
}
