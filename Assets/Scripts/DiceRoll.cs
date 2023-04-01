using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public int diceRoll = 11;
    [SerializeField]
    private HexGrid HexGrid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void roll()
    {
        Debug.Log("Start rolling " + diceRoll);
        HexGrid.resolveDiceRoll(diceRoll);
    }


}
