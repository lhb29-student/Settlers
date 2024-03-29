using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLargeArmy : MonoBehaviour
{
    [SerializeField] private List<UserPlayer> players;
    [SerializeField] private UserPlayer leadPlayer;
    [SerializeField] private int currantLargeArmy = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        findLargeArmy();
    }

    public void findLargeArmy()
    {
        foreach (var player in players)
        {
            if (player.getNumOfKnights() >= 3)
            {
                compareLargestArmy(player);
            }

        }
    }

    public void compareLargestArmy(UserPlayer p)
    {
        if (leadPlayer == null)
        {
            currantLargeArmy = p.getNumOfKnights();
            leadPlayer = p;
            givePoints();
        }
        else if (p.getNumOfKnights() > currantLargeArmy)
        {
            currantLargeArmy = p.getNumOfKnights();
            leadPlayer = p;
            givePoints();
        }

    }

    public void givePoints()
    {
        foreach (var player in players)
        {
            if (player == leadPlayer)
            {
                player.ifLargeArmy(true);
            }
            else
            {
                player.ifLargeArmy(false);
            }
        }
    }
}
