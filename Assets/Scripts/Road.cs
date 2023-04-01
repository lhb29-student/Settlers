using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Players;


public class Road : MonoBehaviour
{
    [SerializeField] private players controlled;
    [SerializeField] BoardPiece bp;
    [SerializeField] Intersect intersectA;
    [SerializeField] Intersect intersectB;
    [SerializeField] List<Road> nearRoads; 
    // Start is called before the first frame update
    void Start()
    {
        bp = gameObject.GetComponentInChildren<BoardPiece>();
    }

    // Update is called once per frame
    void Update()
    {
        updateControll(bp.getPlayColor());
    }

    public void updateControll(int i)
    {
        controlled = (players)i;
    }

    public players GetPlayertype()
    {
        return controlled;
    }
    public List<Road> GetRoadsNearby()
    {
        return nearRoads;
    }

    public int checkNearby()
    {
        int num = 0;
        foreach(Road road in nearRoads)
        {
            if(this.GetPlayertype() == road.GetPlayertype())
            {
                num++;
            }
        }
        return num;
    }

    public bool isRoadEnd()
    {
        return checkNearby() == 1;
    }

    public string nearbyRoads()
    {
        string mainRoad = gameObject.name;
        string nRoads = "";
        foreach (var road in nearRoads)
        {
            nRoads += road.name + ", ";
        }
        return mainRoad + " is next to " + nRoads;
    }
    public Intersect getInterA()
    {
        return intersectA;
    }
    public Intersect getInterB()
    {
        return intersectB;
    }
}
