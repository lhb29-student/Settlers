using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    [SerializeField] private List<Cards> currantHand = new List<Cards>();

    public List<Cards> returnHand()
    {
        return currantHand;
    }

    public void addHand(Cards s)
    {
        currantHand.Add(s);
        if(s.returnImmediate() == true)
        {
            playCard(s);
        }
    }

    public void removeHand(Cards s)
    {
        currantHand.Remove(s);
    }

    public void playCard(Cards s)
    {
        int id = s.returnCardID();

        if(id == 0)
        {
            //play knight
            Debug.Log("play knight");
            GetComponent<UserPlayer>().addKnight();
        }
        else if(id == 6)
        {
            //play Monopoly
            Debug.Log("play Monopoly");
        }
        else if (id == 7)
        {
            //play YearOfPlenty
            Debug.Log("play YearOfPlenty");
        }
        else if (id == 8)
        {
            //play RoadBuilding
            Debug.Log("play RoadBuilding");
        }
        else
        {
            //Victory point
            Debug.Log("Victory point");
            GetComponent<UserPlayer>().addScore(1);

        }
    }

    
}
