using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    //Calls the rigidbody for collision detection
    Rigidbody rb;
    //Check if the dice has been thrown and if it has landed
    bool thrown;
    bool hasLanded;
    bool hasNum = false;
    
    //The initial position of the die
    Vector3 initialPos;

    //The value of the dice after it has been rolled
    public int diceResult;

    //Creates an array to store the dice sides
    public DiceSide[] diceSides;

    //To check total values of both dices
    public GameObject Dice1;
    public GameObject Dice2;

    void Start()
    {
        //Calls both the dice to be calculated later
        Dice1 = GameObject.Find("Dice1");
        Dice2 = GameObject.Find("Dice2");
        rb = GetComponent<Rigidbody>();
        //Store the initial position of the dice
        initialPos = transform.position;
        // to allow us to manually toggle the gravity
        rb.useGravity = false;

    }

    void Update()
    {
        //can remove the the get key down and add game rules condition here
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }
        //IsSleeping to remove the physics from the dice; this if statement is for diceReset class
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            //add both value of the dice here
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;
            SideValueCheck();
            totalRoll();
        }

        //if it has landed then roll again
        if(rb.IsSleeping() && hasLanded && diceResult == 0)
        {
            RollAgain();
        }
    }

    void RollDice()
    {
        //if dice has not been thown, spin the dice
        if(!thrown && !hasLanded)
        {
            DiceSpin();

        }
        else if(thrown && hasLanded)
        {
            //if dice has been thrown and has landed on the ground; reset the dice
            DiceReset();
        }
    }

    void DiceReset()
    {
        //resets the positon of the dice after it has been thrown to it's original positon
        //set all the condition to false and set the position to initilPos
        transform.position = initialPos;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    void RollAgain()
    {
        //repeat the roll process again
        DiceReset();
        DiceSpin();
    }

    void SideValueCheck()
    {
        diceResult = 0;
        //loops throught the array which contains the 6 sides of the dice
        foreach(DiceSide side in diceSides)
        {
            //check if any of the side is on the ground
            if(side.OnGround())
            {
                //the dice no. will the opposite of the side that touches the ground
                diceResult = side.sideValue;
                //Debug.Log(diceResult + " is the number you have been rolled");

            }
        }
    }

    void DiceSpin()
    {
        thrown = true;
        rb.useGravity = true;
        //Give the dice a little spin as the dice falls down
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }


    //returns the total value of both the dice that has been thrown
    void totalRoll()
    {
        Dice D1 = Dice1.GetComponent<Dice>();
        Dice D2 = Dice2.GetComponent<Dice>();
        //check if both the dice has already touched the ground
        if (D1.hasLanded && D2.hasLanded)
        {
            //calculate the total of the dice result
            int totalNo = D1.diceResult + D2.diceResult;
            Debug.Log(totalNo + " rolled");
        }
    }
}
