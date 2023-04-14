using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AIScript : MonoBehaviour
{

    [SerializeField] private string playerTag;
    [SerializeField] private int playerColor;
    [SerializeField] private int chooseInter;

    [SerializeField] private bool isBusy = false;

    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<GameObject> intersects;
    [SerializeField] private GameObject allIntersects;

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
            Debug.Log(playerTag + "'s turn");

            // prevent coroutine from running x times
            if (isBusy == false)
            {
                StartCoroutine(ChooseStarter());
            }
        }
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
