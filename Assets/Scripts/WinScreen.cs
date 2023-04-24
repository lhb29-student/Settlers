using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WinScreen : MonoBehaviour
{
    [SerializeField] private List<UserPlayer> userPlayers;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI WinText;

    private void Start()
    {
        panel.gameObject.SetActive(false);
    }
    void Update()
    {
        for(int i = 0; i < userPlayers.Count; i++)
        {
            if (userPlayers[i].returnPlayerScore() >= 10)
            {
                winSceen(i);
            }
        }
    }

    private void winSceen(int i)
    {
        WinText.text = "Congratulations player " + (i + 1) + "!";
        panel.gameObject.SetActive(true);
        
    }
}
