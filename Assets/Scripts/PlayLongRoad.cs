using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class PlayLongRoad : MonoBehaviour
{
    
    [SerializeField] private List<UserPlayer> players;
    [SerializeField] private UserPlayer leadPlayer;
    [SerializeField] private int currantLongRoad = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void findLongestPath()
    {
        foreach(var player in players)
        {
            if(player.getRoadLength() >= 5)
            {
                compareLongestPath(player);
            }
            
        }
    }

    public void compareLongestPath(UserPlayer p)
    {
        if (leadPlayer == null)
        {
            currantLongRoad = p.getRoadLength();
            leadPlayer = p;
            givePoints();
        }
        else if (p.getRoadLength() > currantLongRoad)
        {
            currantLongRoad = p.getRoadLength();
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
                player.ifLongRoad(true);
            }
            else
            {
                player.ifLongRoad(false);
            }
        }
    }
}
