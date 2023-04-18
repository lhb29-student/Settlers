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

    public void RemoveWood()
    {
        woodResource--;
        totalCards--;
    }
    public void RemoveWool()
    {
        woolResource--;
        totalCards--;
    }
    public void RemoveWheat()
    {
        wheatResource--;
        totalCards--;
    }
    public void RemoveOre()
    {
        oreResource--;
        totalCards--;
    }
    public void RemoveBrick()
    {
        brickResource--;
        totalCards--;
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
