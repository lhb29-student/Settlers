using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Players;

public class BPmanager : MonoBehaviour
{

    [SerializeField] private players playTurn;
    [SerializeField] private UserPlayer[] players;
    [SerializeField] private int playNum = 0;

    [SerializeField] private List<Intersect> allInters;
    [SerializeField] private List<Road> allRoads;

    // can pick
    [SerializeField] private bool pickInter;
    [SerializeField] private bool pickRoad;
    [SerializeField] private bool pickCity;

    [SerializeField] private bool addStartInter;
    [SerializeField] private bool isStartInter;
    [SerializeField] private HashSet<Intersect> startingInters = new HashSet<Intersect>();
    [SerializeField] private GameManager gm;


    void Start()
    {

    }

    void Update()
    {
        int turn = gm.getColorCode();
        playTurn = (players)turn;
        Enum enumPlay = playTurn;
        playNum = Convert.ToInt32(enumPlay) - 1;
    }

    public void AvailableInters()
    {
        setPickInter(true);
        foreach (Intersect inter in allInters)
        {
            if(inter.GetComponentInChildren<BoardPiece>().isUnUseable() == false)
            {
                inter.GetComponentInChildren<BoardPiece>().setCanPick(true);
            }
        }
    }

    public void AvailableRoads()
    {
        setPickRoad(true);
        foreach (Road road in allRoads)
        {
            if (road.GetComponentInChildren<BoardPiece>().isUnUseable() == false)
            {
                road.GetComponentInChildren<BoardPiece>().setCanPick(true);
            }
        }
    }

    public void setupSettlement()
    {
        addStartInter = true;
        if(startingInters.Count != 0)
        {
            foreach (Intersect inter in startingInters)
            {
                inter.GetComponentInChildren<BoardPiece>().setUnUseable(true);
                setupSettlementNearby(inter);
            }
        }

        AvailableInters();
    }

    public void setupSettlementNearby(Intersect i)
    {
        foreach (Intersect near in i.getNearInters())
        {
            near.GetComponentInChildren<BoardPiece>().setUnUseable(true);
        }
    }

    public void setupSettlementRoad(Intersect i)
    {
        setPickRoad(true);
        foreach (Road road in allRoads)
        {
            if(road.GetComponentInChildren<BoardPiece>().isUnUseable() == false &&
              (road.getInterA() == i || road.getInterB() == i))
            {
                playRoadPick(road);
            }
        }
    }

    public void resetUseable()
    {
        foreach(Intersect inter in startingInters)
        {
            foreach(Intersect near in inter.getNearInters())
            {
                near.GetComponentInChildren<BoardPiece>().setUnUseable(false);
            }
        }
    }
    public void AvailableIntersOff()
    {
        foreach (Intersect inter in allInters)
        {
            inter.GetComponentInChildren<BoardPiece>().setCanPick(false);
        }
    }

    public void AvailableRoadsOff()
    {
        foreach (Road road in allRoads)
        {
            road.GetComponentInChildren<BoardPiece>().setCanPick(false);
        }
    }

    public void addToStartInters(Intersect i)
    {
        startingInters.Add(i);
    }

    public void placeRoad()
    {
        setPickRoad(true);
        foreach (Road cRoad in players[playNum].returnRoads())
        {
            foreach (Road near in cRoad.GetRoadsNearby())
            {
                playRoadPick(near);
            }
        }
    }

    public void playRoadPick(Road near)
    {
        if(near.GetComponentInChildren<BoardPiece>().isUnUseable() == false)
        {
            near.GetComponentInChildren<BoardPiece>().setCanPick(true);
        }
    }

    public void setToCity()
    {
        setPickCity(true);
        foreach(Intersect inter in players[playNum].returnIntersect())
        {
            if(inter.getCity() == false)
            inter.GetComponentInChildren<BoardPiece>().setCanPick(true);
        }
    }

    public void buildSettlement()
    {
        //addStartInter = true;
        setPickInter(true);
        foreach (Road cRoad in players[playNum].returnRoads())
        {
            if(cRoad.getInterA().GetComponentInChildren<BoardPiece>().isUnUseable() == false &&
                startingInters.Contains(cRoad.getInterA()) == false)
            {
                cRoad.getInterA().GetComponentInChildren<BoardPiece>().setCanPick(true);
                setupSettlementNearby(cRoad.getInterA());
            }
            if (cRoad.getInterB().GetComponentInChildren<BoardPiece>().isUnUseable() == false &&
                startingInters.Contains(cRoad.getInterB()) == false)
            {
                cRoad.getInterB().GetComponentInChildren<BoardPiece>().setCanPick(true);
                setupSettlementNearby(cRoad.getInterB());
            }
        }
    }

    public bool getPickInter() { return pickInter; }
    public void setPickInter(bool b) { pickInter = b; }
    public bool getPickRoad() { return pickRoad; }
    public void setPickRoad(bool b) { pickRoad = b; }
    public bool getPickCity() { return pickCity; }
    public void setPickCity(bool b) { pickCity = b; }
    public bool getAddStartInter() { return addStartInter; }
    public void setAddStartInter(bool b) { addStartInter = b; }
    public UserPlayer GetPlayer(int i)
    {
        return players[i-1];
    }
    
    public int getPlayTurn()
    {
        return playNum + 1;
    }
}
