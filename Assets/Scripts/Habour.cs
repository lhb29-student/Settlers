using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Players;

public class Habour : MonoBehaviour
{
    [SerializeField] private Intersect[] Harbours = new Intersect[16];
    [SerializeField] private List<Intersect> LumberHarbour;
    [SerializeField] private List<Intersect> WoolHarbour;
    [SerializeField] private List<Intersect> GrainHarbour;
    [SerializeField] private List<Intersect> OreHarbour;
    [SerializeField] private List<Intersect> BrickHarbour;
    [SerializeField] private List<Intersect> AnyHarbour;

    

    void Start()
    {
       

    }

    void Update()
    {
      
    
    }

   

    public bool GetLumberHarbour(players p)
    {
        foreach (Intersect intersect in LumberHarbour)
        {
           if(intersect.GetPlayer() == p) { return true; }
        }
        return false;
    }

    public bool GetWoolHarbour(players p)
    {
        foreach (Intersect intersect in WoolHarbour)
        {
            if (intersect.GetPlayer() == p) { return true; }
        }
        return false;
    }

    public bool GetGrainHarbour(players p)
    {
        foreach (Intersect intersect in GrainHarbour)
        {
            if (intersect.GetPlayer() == p) { return true; }
        }
        return false;
    }

    public bool GetOreHarbour(players p)
    {
        foreach (Intersect intersect in OreHarbour)
        {
            if (intersect.GetPlayer() == p) { return true; }
        }
        return false;
    }

    public bool GetBrickHarbour(players p)
    {
        foreach (Intersect intersect in BrickHarbour)
        {
            if (intersect.GetPlayer() == p) { return true; }
        }
        return false;
    }

    public bool GetAnyHarbour(players p)
    {
        foreach (Intersect intersect in AnyHarbour)
        {
            if (intersect.GetPlayer() == p) { return true; }
        }
        return false;
    }
}
