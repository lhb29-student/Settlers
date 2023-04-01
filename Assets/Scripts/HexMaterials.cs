using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMaterials : MonoBehaviour
{
    [Serializable]
    private class hexTiles
    {
        public Material material;
        public int number;
    }

    [SerializeField] private hexTiles[] m_tiles;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Material getMaterial(int i)
    {
        return m_tiles[i].material;
    }


    public void randomInt()
    {
        int i  = UnityEngine.Random.Range(0, 6);
    }
    public Material placeTerrain()
    {

        int i = 4;
        

        return m_tiles[i].material;
    }
    
}
