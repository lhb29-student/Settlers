using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : MonoBehaviour
{
    [SerializeField] private List<Intersect> cIntersect;
    [SerializeField] private List<Road> cRoad;
    [SerializeField] private int numOfKnights;
    [SerializeField] private int playerScore;
    [SerializeField] private int buildScore;
    [SerializeField] private int hasLongRoad;
    [SerializeField] private int hasLargeArmy;
    void Start()
    {
        
    }

    void Update()
    {
        
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
