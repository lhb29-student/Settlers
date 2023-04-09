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
        if(Input.GetKeyDown(KeyCode.R))
        {
            rollDice();
        }
    }

    public void roll()
    {
        // range 2 ~ 12
        diceRoll = Random.Range(2, 13);
        Debug.Log("Dice roll: " + diceRoll);
        HexGrid.resolveDiceRoll(diceRoll);
    }


    // alternate dice code
    // random number generator
    public int rollDice()
    {
        // range 2 ~ 12
        diceRoll = Random.Range(2,13);
        Debug.Log("Dice roll: " + diceRoll);
        return diceRoll;
    }
}
