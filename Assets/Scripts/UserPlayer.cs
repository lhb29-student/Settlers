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

    [SerializeField] private PlayerResources playerResources;
    [SerializeField] private BPmanager bPmanager;
    [SerializeField] private GameObject roadButton;
    [SerializeField] private GameObject settlementButton;
    [SerializeField] private GameObject cityUpgradeButton;


    void Start()
    {
        // initialize fields
        playerResources = gameObject.GetComponent<PlayerResources>();
        bPmanager = GameObject.Find("Land").GetComponent<BPmanager>();
    }

    void Update()
    {
        if (gameObject.tag == "p1")
        {
            UIControl();
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
    }

    // road button interaction
    public void BuildRoad()
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
