using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPlayer : MonoBehaviour
{
    [SerializeField] private List<Intersect> cIntersect;
    [SerializeField] private List<Road> cRoad;
    [SerializeField] private int numOfKnights;
    [SerializeField] private int playerScore;
    [SerializeField] private int buildScore;
    [SerializeField] private int hasLongRoad;
    [SerializeField] private int hasLargeArmy;
    [SerializeField] private int discardAmount;
    [SerializeField] private bool hasDiscarded = true;

    [SerializeField] private PlayerResources playerResources;
    [SerializeField] private BPmanager bPmanager;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameObject roadButton;
    [SerializeField] private GameObject settlementButton;
    [SerializeField] private GameObject cityUpgradeButton;
    [SerializeField] private TMPro.TextMeshProUGUI woodButton;
    [SerializeField] private TMPro.TextMeshProUGUI brickButton;
    [SerializeField] private TMPro.TextMeshProUGUI woolButton;
    [SerializeField] private TMPro.TextMeshProUGUI wheatButton;
    [SerializeField] private TMPro.TextMeshProUGUI oreButton;
    [SerializeField] private TMPro.TextMeshProUGUI discardText;
    [SerializeField] private GameObject discardMessage;


    void Start()
    {
        // initialize fields
        playerResources = gameObject.GetComponent<PlayerResources>();
        bPmanager = GameObject.Find("Land").GetComponent<BPmanager>();
        boardManager = GameObject.Find("Land").GetComponent<BoardManager>();

        if (gameObject.tag == "p1")
        {
            discardMessage.SetActive(false);
        }
    }

    void Update()
    {
        // this prevents other ai players from affecting ui
        if (gameObject.tag == "p1")
        {
            UIControl();

            // if a 7 is rolled and player has more than 7
            if (boardManager.playerCheckCards == true && playerResources.GetTotalCards() > 7)
            {
                boardManager.playerCheckCards = false;
                CheckCards();
            }

            // continues to run until player discarded required amount
            if (hasDiscarded == false)
            {
                RemoveCards();
            }
        }
    }

    // called when player has more than 7 cards
    public void CheckCards()
    {
        // calculate discard amount
        discardAmount = playerResources.GetTotalCards() / 2;

        while (discardAmount > 0)
        {
            hasDiscarded = false;
            break;
        }
    }

    // controls display of discard message
    public void RemoveCards()
    {
        // stays active while discard amount is not met
        if (discardAmount > 0)
        {
            discardMessage.SetActive(true);
            discardText.text = "Resources to discard: " + discardAmount;
        }
        else
        {
            // hidden after playerhas finished discarding
            discardMessage.SetActive(false);
            hasDiscarded = true;
        }
    }

    // controls appearance of buttons
    public void UIControl()
    {
        // road button
        if (playerResources.CheckRoad() == true)
        {
            roadButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            roadButton.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }

        // settlement button
        if (playerResources.CheckSettlement() == true)
        {
            settlementButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            settlementButton.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }

        // city button
        if (playerResources.CheckCity() == true)
        {
            cityUpgradeButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            cityUpgradeButton.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }

        // resource display
        woodButton.text = playerResources.returnResource(0).ToString();
        brickButton.text = playerResources.returnResource(4).ToString();
        woolButton.text = playerResources.returnResource(1).ToString();
        wheatButton.text = playerResources.returnResource(2).ToString();
        oreButton.text = playerResources.returnResource(3).ToString();
    }

    // road button interaction
    public void BuildRoad()
    {
        // only allowed to call when it's player turn
        if (boardManager.currentP == "p1")
        {
            // builds road if player met requirements
            if (playerResources.CheckRoad() == true)
            {
                bPmanager.placeRoad();
                playerResources.RoadCost();
            }
            else
            {
                Debug.Log("lacking cost for road");
            }
        }
        else
        {
            Debug.Log("not player's turn");
        }
    }

    // settlement button interaction
    public void BuildSettlement()
    {
        // builds settlement if player met requirements
        if (playerResources.CheckSettlement() == true)
        {
            bPmanager.buildSettlement();
            playerResources.SettlementCost();
        }
        else
        {
            Debug.Log("lacking cost for settlement");
        }
    }

    // city button interaction
    public void BuildCity()
    {
        // builds city if player met requirements
        if (playerResources.CheckCity() == true)
        {
            bPmanager.setToCity();
            playerResources.CityCost();
        }
        else
        {
            Debug.Log("lacking cost for city");
        }
    }

    // remove 1 wood
    public void RemoveWood()
    {
        // discard 1 if available
        if (hasDiscarded == false && playerResources.returnResource(0) >= 1)
        {
            playerResources.RemoveWood(1);
            discardAmount--;
        }
        else
        {
            Debug.Log("cannot discard resource");
        }
    }

    // remove 1 brick
    public void RemoveBrick()
    {
        // discard 1 if available
        if (hasDiscarded == false && playerResources.returnResource(4) >= 1)
        {
            playerResources.RemoveBrick(1);
            discardAmount--;
        }
        else
        {
            Debug.Log("cannot discard resource");
        }
    }

    // remove 1 wool
    public void RemoveWool()
    {
        // discard 1 if available
        if (hasDiscarded == false && playerResources.returnResource(1) >= 1)
        {
            playerResources.RemoveWool(1);
            discardAmount--;
        }
        else
        {
            Debug.Log("cannot discard resource");
        }
    }

    // remove 1 wheat
    public void RemoveWheat()
    {
        // discard 1 if available
        if (hasDiscarded == false && playerResources.returnResource(2) >= 1)
        {
            playerResources.RemoveWheat(1);
            discardAmount--;
        }
        else
        {
            Debug.Log("cannot discard resource");
        }
    }

    // remove 1 ore
    public void RemoveOre()
    {
        // discard 1 if available
        if (hasDiscarded == false && playerResources.returnResource(3) >= 1)
        {
            playerResources.RemoveOre(1);
            discardAmount--;
        }
        else
        {
            Debug.Log("cannot discard resource");
        }
    }

    public void addIntersect(Intersect i)
    {
        cIntersect.Add(i);
    }

    public void addRoad(Road r)
    {
        cRoad.Add(r);
       
    }

    public List<Road> returnRoads()
    {
        return cRoad;
    }

    public List<Intersect> returnIntersect()
    {
        return cIntersect;
    }

    public int getRoadLength()
    {
        return cRoad.Count;
    }

    public int getNumOfKnights()
    {
        return numOfKnights;   
    }

    public void addScore(int i)
    {
        buildScore = buildScore + i;
        updateScore();
    }

    public void addKnight()
    {
        numOfKnights++;
    }

    public void ifLongRoad(bool b)
    {
        if(b){
            hasLongRoad = 2;
        }
        else
        {
            hasLongRoad = 0;
        }
        updateScore();
    }

    public void ifLargeArmy(bool b)
    {
        if (b)
        {
            hasLargeArmy = 2;
        }
        else
        {
            hasLargeArmy = 0;
        }
        updateScore();
    }


    public void updateScore()
    {
        playerScore = buildScore + hasLongRoad + hasLargeArmy;
    }
}
