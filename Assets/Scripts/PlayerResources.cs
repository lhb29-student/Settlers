using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{

    [SerializeField] private int totalCards = 0;
    [SerializeField] private int woodResource = 0;
    [SerializeField] private int woolResource = 0;
    [SerializeField] private int wheatResource = 0;
    [SerializeField] private int oreResource = 0;
    [SerializeField] private int brickResource = 0;

    public List<int> resources;

    void Start()
    {
        
    }

    void Update()
    {
        totalCards = woodResource + woolResource + wheatResource + oreResource + brickResource;
    }

    public bool CheckRoad()
    {
        // check resource requirement
        if (woodResource >= 1 && brickResource >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // remove cost for road
    public void RoadCost()
    {
        woodResource--;
        brickResource--;
    }

    public bool CheckSettlement()
    {
        // check resource requirement
        if (woodResource >= 1 && brickResource >= 1 && woolResource >= 1 && wheatResource >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // remove cost for settlement
    public void SettlementCost()
    {
        woodResource--;
        brickResource--;
        woolResource--;
        wheatResource--;
    }

    public bool CheckCity()
    {
        // check resource requirement
        if (wheatResource >= 2 && oreResource >= 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // remove cost for city
    public void CityCost()
    {
        wheatResource -= 2;
        oreResource -= 3;
    }

    public void DiscardResource()
    {
        // store resource count
        resources = new List<int>();
        resources.Add(woodResource);
        resources.Add(woolResource);
        resources.Add(wheatResource);
        resources.Add(oreResource);
        resources.Add(brickResource);

        // determine discard amount
        int discardAmount = totalCards / 2;

        // loop until removed half of cards
        for (int i = 0; i < discardAmount; i++)
        {
            // choose random resource
            int chooseDiscard = Random.Range(0, 5);

            if (resources[chooseDiscard] == 0)
            {
                // ensures only resource player has is discarded
                while (true)
                {
                    chooseDiscard = Random.Range(0, 5);
                    if (resources[chooseDiscard] != 0)
                    {
                        break;
                    }
                }
                
            }
            //Debug.Log("discarding resource");
            resources[chooseDiscard]--;
        }

        // new resource count
        woodResource = resources[0];
        woolResource = resources[1];
        wheatResource = resources[2];
        oreResource = resources[3];
        brickResource = resources[4];

        //Debug.Log("removed " + discardAmount + " cards");
    }

    public int GetTotalCards()
    {
        return totalCards;
    }
    //reduce the number of necessary methods used for adding/subtracting resources
    //Adding resources
    public void AddWood(int i)
    {
        woodResource = woodResource + i;
    }
    public void AddWool(int i)
    {
        woolResource = woolResource + i;
    }
    public void AddWheat(int i)
    {
        wheatResource = wheatResource + i;
    }
    public void AddOre(int i)
    {
        oreResource = oreResource + i;
    }
    public void AddBrick(int i)
    {
        brickResource = brickResource + i;
    }

    //Subtracting resources
    public void RemoveWood(int i)
    {
        woodResource = woodResource - i;
    }
    public void RemoveWool(int i)
    {
        woolResource = woodResource - i;
    }
    public void RemoveWheat(int i)
    {
        wheatResource = wheatResource - i;
    }
    public void RemoveOre(int i)
    {
        oreResource = oreResource - i;
    }
    public void RemoveBrick(int i)
    {
        brickResource = brickResource - i;
    }

    //Returing the type of resource; 
    public int returnResource(int i)
    {
        if (i == 0) { return woodResource; }
        else if (i == 1) { return woolResource; }
        else if (i == 2) { return wheatResource; }
        else if (i == 3) { return oreResource; }
        else { return brickResource; }
    }

    //Used for the monopoly event;
    public int loseWood()
    {
        int lose = 0;
        lose = woodResource;
        woodResource = 0;
        return lose;
    }
    public int loseWool()
    {
        int lose = 0;
        lose = woolResource;
        woolResource = 0;
        return lose;
    }
    public int loseWheat()
    {
        int lose = 0;
        lose = wheatResource;
        wheatResource = 0;
        return lose;
    }
    public int loseOre()
    {
        int lose = 0;
        lose = oreResource;
        oreResource = 0;
        return lose;
    }
    public int loseBrick()
    {
        int lose = 0;
        lose = brickResource;
        brickResource = 0;
        return lose;
    }

}
