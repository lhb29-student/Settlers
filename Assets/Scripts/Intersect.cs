using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Players;

public class Intersect : MonoBehaviour
{

    [SerializeField] private players controlled;
    [SerializeField] private int buildScore = 1;
    [SerializeField] private bool isCity = false;
    [SerializeField] private BoardPiece bp;
    [SerializeField] private GameObject city;
    [SerializeField] private List<Intersect> nearInters;
    // Start is called before the first frame update
    void Start()
    {
        controlled = players.np;
        bp = gameObject.GetComponentInChildren<BoardPiece>();
        city.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        updateControll(bp.getPlayColor());
        if (isCity)
        {
            city.SetActive(true);
            buildScore = 2;
        }
    }

    public void updateControll(int i) {
        controlled = (players)i;
    }

    public bool isControlled()
    {
        return controlled != players.np;
    }

    public void getResource(string s)
    {
        if (isControlled())
        {
            Debug.Log(controlled + "has recieve resource from " + s);
        }
    }

    public List<Intersect> getNearInters()
    {
        return nearInters;
    }

    public void setCity(bool b)
    {
        isCity = b;
    }

    public bool getCity()
    {
        return isCity;
    }

    public void setCityControll(Color c)
    {
        city.GetComponent<Renderer>().material.color = c;
    }
}
