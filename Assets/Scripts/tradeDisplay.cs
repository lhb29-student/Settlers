using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tradeDisplay : MonoBehaviour
{
   // [SerializeField] private GameObject tDisplay;
    [SerializeField] private List<Button> tradeFor;
    [SerializeField] private int disableID;

    private void Start()
    {
        
        tradeFor[disableID].enabled = false;
        tradeFor[disableID].GetComponent<Image>().color = Color.gray;
    }
    

    public void displaySame(int i)
    {
        tradeFor[i].enabled = false;
    }
}
