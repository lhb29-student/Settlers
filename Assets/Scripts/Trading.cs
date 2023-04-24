using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Players;

public class Trading : MonoBehaviour
{
    [SerializeField] private UserPlayer uPlayer;
    [SerializeField] private players playerEnum;
    [SerializeField] private PlayerResources uResources;

    //uHarTrades[0] = tradeWithLumber
    //uHarTrades[1] = tradeWithWool
    //uHarTrades[2] = tradeWithGrain
    //uHarTrades[3] = tradeWithOre
    //uHarTrades[4] = tradeWithBrick
    //uHarTrades[5] = tradeWithAny
    [SerializeField] private bool[] uHarTrades = new bool[6];
    [SerializeField] private Habour Habour;

    [SerializeField] private int tradeWith;
    [SerializeField] private int tradeFor;
    [SerializeField] private int tradeWithAmount;
    [SerializeField] private int tradeForAmount;


    // Start is called before the first frame update
    void Start()
    {
        uPlayer = GetComponent<UserPlayer>();
        uResources = GetComponent<PlayerResources>();

    }

    // Update is called once per frame
    void Update()
    {
        //To trade with the lumber harbour
        if (uResources.returnResource(0) >= 2 && Habour.GetLumberHarbour(playerEnum))
        {
            uHarTrades[0] = true;
        }
        else
        {
            uHarTrades[0] = false;
        }

        //To trade with the wool harbour
        if (uResources.returnResource(1) >= 2 && Habour.GetWoolHarbour(playerEnum))
        {
            uHarTrades[1] = true;
        }
        else
        {
            uHarTrades[1] = false;
        }

        //To trade with the grain harbour
        if (uResources.returnResource(2) >= 2 && Habour.GetGrainHarbour(playerEnum))
        {
            uHarTrades[2] = true;
        }
        else
        {
            uHarTrades[2] = false;
        }

        //To trade with the ore harbour
        if (uResources.returnResource(3) >= 2 && Habour.GetOreHarbour(playerEnum))
        {
            uHarTrades[3] = true;
        }
        else
        {
            uHarTrades[3] = false;
        }

        //To trade with the brick harbour
        if (uResources.returnResource(4) >= 2 && Habour.GetBrickHarbour(playerEnum))
        {
            uHarTrades[4] = true;
        }
        else
        {
            uHarTrades[4] = false;
        }

        //To trade with the Any harbour
        if (Habour.GetAnyHarbour(playerEnum))
        {
            uHarTrades[5] = true;
        }
        else
        {
            uHarTrades[5] = false;
        }

    }

    public void confirmTrade()
    {
        makeTrade(tradeWith, tradeFor);
    }

    public void makeTrade(int tWith, int tFor)
    {
        if (tWith == tFor || tWith == -1 || tFor == -1)
        {
            return;
        }
        if (tWith == 0) { tradeInLumber(tFor); }
        else if (tWith == 1) { tradeInWool(tFor); }
        else if (tWith == 2) { tradeInGrain(tFor); }
        else if (tWith == 3) { tradeInOre(tFor); }
        else if (tWith == 4) { tradeInBrick(tFor); }
    }

    public void tradeInLumber(int i)
    {
        if (i == 0)
        {
            uResources.RemoveWood(tradeWithAmount);
            uResources.AddWood(1);
        }
        else if (i == 1)
        {
            uResources.RemoveWood(tradeWithAmount);
            uResources.AddWool(1);
        }
        else if (i == 2)
        {
            uResources.RemoveWood(tradeWithAmount);
            uResources.AddWheat(1);
        }
        else if (i == 3)
        {
            uResources.RemoveWood(tradeWithAmount);
            uResources.AddOre(1);
        }
        else if (i == 4)
        {
            uResources.RemoveWood(tradeWithAmount);
            uResources.AddBrick(1);
        }
    }
    public void tradeInWool(int i)
    {
        if (i == 0)
        {
            uResources.RemoveWool(tradeWithAmount);
            uResources.AddWood(1);
        }
        else if (i == 1)
        {
            uResources.RemoveWool(tradeWithAmount);
            uResources.AddWool(1);
        }
        else if (i == 2)
        {
            uResources.RemoveWool(tradeWithAmount);
            uResources.AddWheat(1);
        }
        else if (i == 3)
        {
            uResources.RemoveWool(tradeWithAmount);
            uResources.AddOre(1);
        }
        else if (i == 4)
        {
            uResources.RemoveWool(tradeWithAmount);
            uResources.AddBrick(1);
        }
    }
    public void tradeInGrain(int i)
    {
        if (i == 0)
        {
            uResources.RemoveWheat(tradeWithAmount);
            uResources.AddWood(1);
        }
        else if (i == 1)
        {
            uResources.RemoveWheat(tradeWithAmount);
            uResources.AddWool(1);
        }
        else if (i == 2)
        {
            uResources.RemoveWheat(tradeWithAmount);
            uResources.AddWheat(1);
        }
        else if (i == 3)
        {
            uResources.RemoveWheat(tradeWithAmount);
            uResources.AddOre(1);
        }
        else if (i == 4)
        {
            uResources.RemoveWheat(tradeWithAmount);
            uResources.AddBrick(1);
        }
    }
    public void tradeInOre(int i)
    {
        Debug.Log("Traded in " + tradeWithAmount + " ore");
        if (i == 0)
        {
            uResources.RemoveOre(tradeWithAmount);
            uResources.AddWood(1);
        }
        else if (i == 1)
        {
            uResources.RemoveOre(tradeWithAmount);
            uResources.AddWool(1);
        }
        else if (i == 2)
        {
            uResources.RemoveOre(tradeWithAmount);
            uResources.AddWheat(1);
        }
        else if (i == 3)
        {
            uResources.RemoveOre(tradeWithAmount);
            uResources.AddOre(1);
        }
        else if (i == 4)
        {
            uResources.RemoveOre(tradeWithAmount);
            uResources.AddBrick(1);
        }
    }
    public void tradeInBrick(int i)
    {
        if (i == 0)
        {
            uResources.RemoveBrick(tradeWithAmount);
            uResources.AddWood(1);
        }
        else if (i == 1)
        {
            uResources.RemoveBrick(tradeWithAmount);
            uResources.AddWool(1);
        }
        else if (i == 2)
        {
            uResources.RemoveBrick(tradeWithAmount);
            uResources.AddWheat(1);
        }
        else if (i == 3)
        {
            uResources.RemoveBrick(tradeWithAmount);
            uResources.AddOre(1);
        }
        else if (i == 4)
        {
            uResources.RemoveBrick(tradeWithAmount);
            uResources.AddBrick(1);
        }
    }

    //Returns bools from the uHarTrades array
    public bool getLumberBool()
    {
        return uHarTrades[0];
    }
    public bool getWoolBool()
    {
        return uHarTrades[1];
    }
    public bool getGrainBool()
    {
        return uHarTrades[2];
    }
    public bool getOreBool()
    {
        return uHarTrades[3];
    }
    public bool getBrickBool()
    {
        return uHarTrades[4];
    }
    public bool getAnyBool()
    {
        return uHarTrades[5];
    }

    //sets or gets the number id for which resource to trade with
    public void setTradeWith(int i)
    {
        tradeWith = i;
    }
    public int getTradeWith()
    {
        return tradeWith;
    }
    //sets or gets the number id for which resource to trade for
    public void setTradeFor(int i)
    {
        tradeFor = i;
    }
    public int getTradeFor()
    {
        return tradeWith;
    }
    //sets or gets the number of resources to trade with
    public void setTradeWithAmount(int i)
    {
        tradeWithAmount = i;
    }
    public int getTradeWithAmount()
    {
        return tradeWithAmount;
    }
    //sets or gets the number of resources to trade for
    public void setTradeForAmount(int i)
    {
        tradeForAmount = i;
    }
    public int getTradeForAmount()
    {
        return tradeForAmount;
    }


    public bool got3Lumber()
    {
        return uResources.returnResource(0) >= 3;
    }
    public bool got3Wool()
    {
        return uResources.returnResource(1) >= 3;
    }
    public bool got3Grain()
    {
        return uResources.returnResource(2) >= 3;
    }
    public bool got3Ore()
    {
        return uResources.returnResource(3) >= 3;
    }
    public bool got3Brick()
    {
        return uResources.returnResource(4) >= 3;
    }

    public bool got4Lumber()
    {
        return uResources.returnResource(0) >= 4;
    }
    public bool got4Wool()
    {
        return uResources.returnResource(1) >= 4;
    }
    public bool got4Grain()
    {
        return uResources.returnResource(2) >= 4;
    }
    public bool got4Ore()
    {
        return uResources.returnResource(3) >= 4;
    }
    public bool got4Brick()
    {
        return uResources.returnResource(4) >= 4;
    }
}
