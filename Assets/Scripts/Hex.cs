using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    [SerializeField] private HexCoords hexCoords;
    [SerializeField] private TerrainType terrainType;
    [SerializeField] private int tokenNumber;
    [SerializeField] private List<Intersect> iPoints;
    [SerializeField] private bool robber;
    

    public Vector3Int HexCoordnts => hexCoords.GetHexCoords();

    private void Awake()
    {
        hexCoords = GetComponent<HexCoords>();
    }

    public void changeTerain(Material m)
    {
        GetComponentInChildren<MeshRenderer>().material = m;
        
    }

    public void setTokenNumber(int i)
    {
        tokenNumber = i;
    }

    public int getTokenNumber()
    {
        return tokenNumber;
    }

    public void changeType(int i)
    {
        terrainType = (TerrainType)i;
    }

    public bool isDesert()
    {
        return terrainType == TerrainType.Desert;
    }

    public bool isRobberHere()
    {
        return robber;
    }

    public void setRobber(bool b)
    {
        robber = b;
    }


    public Vector3Int GetVector()
    {
        return HexCoordnts;
    }

    public void giveResoure()
    {
        foreach(Intersect i in iPoints)
        {
            i.getResource((int)terrainType.GetTypeCode() + ". " + terrainType.ToString());
        }
    }
}
