using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    //check if the dice is on the ground
    bool onGround;
    public int sideValue;

    //if the dice collides with the ground set it to true
    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Ground")
        {
            onGround = true;
        }
    }
    //set the bool OnGround back to false for reset purposes
    void OnTriggerExit( Collider col)
    {
        if(col.tag == "Ground")
        {
            onGround = false;
        }
    }

    //returns the bool OnGround
    public bool OnGround()
    {
        return onGround;
    }
}
