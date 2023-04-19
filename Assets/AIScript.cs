using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AIScript : MonoBehaviour
{

    [SerializeField] private string playerTag;
    [SerializeField] private int playerColor;
    [SerializeField] private int chooseInter;

    [SerializeField] private bool isBusy = false;
    [SerializeField] private bool diceRolled = false;
    [SerializeField] private bool routineStart = false;

    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<GameObject> intersects;
    [SerializeField] private GameObject allIntersects;

    public bool checkCards = false;

    public BoardPiece bp;
    public GameObject inter;


    void Start()
    {
        playerTag = gameObject.tag;
        allIntersects = GameObject.Find("IntersectPoints");

        playerColor = int.Parse(playerTag.Substring(1));

        // initialise managers
        boardManager = GameObject.FindGameObjectWithTag("Land").GetComponent<BoardManager>();
        gameManager = GameObject.FindGameObjectWithTag("Land").GetComponent<GameManager>();

        intersects = new List<GameObject>();

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
                StartCoroutine(ChooseStarter());
            }
            if (routineStart == false)
            {
                StartCoroutine(AIRoutine());
            }
        }

        if (checkCards == true)
        {
            CheckCards();
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

        yield return new WaitForSeconds(0.5f);

        // move bandit
        float waitTime = 0.5f;
        if (boardManager.moveBandit == true)
        {
            boardManager.moveBandit = false;
            //Debug.Log("Bandit moved by " + playerTag);
        }
        else
        {
            waitTime = 0;
        }

        yield return new WaitForSeconds(waitTime);

        // all moves finished
        if (diceRolled == true)
        {
            Debug.Log(playerTag + " ended turn");
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
    }

    // check if cards are more than 7
    public void CheckCards()
    {
        checkCards = false;
        var resources = GameObject.FindWithTag(playerTag).GetComponent<PlayerResources>();

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

    IEnumerator RollDice()
    {
        // roll dice from board manager
        diceRolled = true;
        boardManager.RollDice();

        yield return null;
    }

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
