using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokenManager : MonoBehaviour
{
    [SerializeField] private placeToken[] pt;
    [SerializeField] private List<tokenData> tData;
    [SerializeField] private GameObject robber;
    [SerializeField] private Hex WhereRobber;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(pt[0].GetComponentInParent<Hex>().getType());
        //placeDownTokens();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeDownTokens()
    {
        int a = 0;
        for (int i = 0; i < pt.Length; i++)
        {
            
            if (pt[i].GetComponentInParent<Hex>().isDesert())
            {
                //Debug.Log("Desert is at " + (i + 1));

                placeRobber(pt[i].GetComponent<Transform>().position);
                WhereRobber = pt[i].GetComponentInParent<Hex>();
                WhereRobber.setRobber(true);
            }
            else
            {
                //Debug.Log("This is not desert at " + (i + 1));
                pt[i].GetComponentInParent<Hex>().setTokenNumber(tData[a].num);
                pt[i].setToken(tData[a].Sprite);
                a++;
            }
        }
    }

    public void placeRobber(Vector3 v)
    {
        robber.transform.position = v + new Vector3(0.0f, 0.6f, 0.0f);
    }

    public void replaceRobber(Hex newHex)
    {
        WhereRobber.setRobber(false);
        WhereRobber = newHex;
        WhereRobber.setRobber(true);
        robber.transform.position = newHex.GetComponent<Transform>().position + new Vector3(0.0f, 1.7f, 0.0f);
    }

    public void availableRobberSpace()
    {
        foreach(placeToken pt in pt)
        {
            pt.GetComponentInParent<Hex>().robberPlaceOptions();
        }
    }
    public void closeRobberSpace()
    {
        foreach (placeToken pt in pt)
        {
            pt.GetComponentInParent<Hex>().robberCloseOptions();
        }
    }
}
