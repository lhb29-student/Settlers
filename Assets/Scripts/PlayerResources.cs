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


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddWood()
    {
        woodResource++;
        totalCards++;
    }
    public void AddWool()
    {
        woolResource++;
        totalCards++;
    }
    public void AddWheat()
    {
        wheatResource++;
        totalCards++;
    }
    public void AddOre()
    {
        oreResource++;
        totalCards++;
    }
    public void AddBrick()
    {
        brickResource++;
        totalCards++;
    }
}
