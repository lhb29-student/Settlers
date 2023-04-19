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

    public void AddWood()
    {
        woodResource++;
    }
    public void AddWool()
    {
        woolResource++;
    }
    public void AddWheat()
    {
        wheatResource++;
    }
    public void AddOre()
    {
        oreResource++;
    }
    public void AddBrick()
    {
        brickResource++;
    }

    public void RemoveWood()
    {
        woodResource--;
    }
    public void RemoveWool()
    {
        woolResource--;
    }
    public void RemoveWheat()
    {
        wheatResource--;
    }
    public void RemoveOre()
    {
        oreResource--;
    }
    public void RemoveBrick()
    {
        brickResource--;
    }

    public int loseWood()
    {
        int lose = 0;
        totalCards = totalCards - woodResource;
        lose = woodResource;
        woodResource = 0;
        return lose;
    }
    public int loseWool()
    {
        int lose = 0;
        totalCards = totalCards - woolResource;
        lose = woolResource;
        woolResource = 0;
        return lose;
    }
    public int loseWheat()
    {
        int lose = 0;
        totalCards = totalCards - wheatResource;
        lose = wheatResource;
        wheatResource = 0;
        return lose;
    }
    public int loseOre()
    {
        int lose = 0;
        totalCards = totalCards - oreResource;
        lose = oreResource;
        oreResource = 0;
        return lose;
    }
    public int loseBrick()
    {
        int lose = 0;
        totalCards = totalCards - brickResource;
        lose = brickResource;
        brickResource = 0;
        return lose;
    }

    public void AddMoreWood(int i)
    {
        woodResource = woodResource + i;
        totalCards = totalCards + i;
    }
    public void AddMoreWool(int i)
    {
        woolResource = woolResource + i;
        totalCards = totalCards + i;
    }
    public void AddMoreWheat(int i)
    {
        wheatResource = wheatResource + i;
        totalCards = totalCards + i;
    }
    public void AddMoreOre(int i)
    {
        oreResource = oreResource + i;
        totalCards = totalCards + i;
    }
    public void AddMoreBrick(int i)
    {
        brickResource = brickResource + i;
        totalCards = totalCards + i;
    }
}
