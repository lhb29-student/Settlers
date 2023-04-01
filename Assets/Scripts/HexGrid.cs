using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    Dictionary<Vector3Int, Hex> hexsTileDict = new Dictionary<Vector3Int, Hex>();
    [SerializeField] private tokenManager tm;
    [Serializable]
    private class hexTiles
    {
        public Material material;
        public int number;
    }

    [SerializeField] private hexTiles[] m_tiles;

    private void Start()
    {
        foreach(Hex hex in FindObjectsOfType<Hex>())
        {
            hexsTileDict[hex.HexCoordnts] = hex;
        }

        foreach(KeyValuePair<Vector3Int,Hex> valuePair in hexsTileDict){

            int i = randomInt();
            valuePair.Value.GetComponent<Hex>().changeTerain(m_tiles[i].material);
            valuePair.Value.GetComponent<Hex>().changeType(i);


        }

        tm.placeDownTokens();
    }

    private int randomInt()
    {
        int i = -1;

        while (i < 0)
        {
            int a = UnityEngine.Random.Range(0, 6);
            if (m_tiles[a].number >= 1)
            {
                i = a;
                m_tiles[a].number--;
            }

        }
        return i;
    }

    public void resolveDiceRoll(int i)
    {
        if(i != 7)
        {
            Debug.Log("Not a 7");
            resolveResoure(i);
        }
        else if (i >= 2 || i <= 12)
        {
            Debug.Log("Is a 7");
            resolveRobber();
        }
        else
        {
            Debug.Log("Invalid roll");
        }
    }

    public void resolveResoure(int i)
    {
        foreach (KeyValuePair<Vector3Int, Hex> valuePair in hexsTileDict)
        {
            if (valuePair.Value.getTokenNumber() == i && valuePair.Value.isRobberHere() == false)
            {
                valuePair.Value.giveResoure();
            }

            if(valuePair.Value.isRobberHere() == true)
            {
                Debug.Log("Can't rececive resouces at " + valuePair.Value);
            }
        }
    }

    public void resolveRobber()
    {
        Debug.Log("The Robber is on the move");
    }

    public Hex GetTileAt(Vector3Int hexCoordnts)
    {
        Hex result = null;
        hexsTileDict.TryGetValue(hexCoordnts, out result);
        return result;
    }

    
}
