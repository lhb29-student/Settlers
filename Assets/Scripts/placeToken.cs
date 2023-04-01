using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeToken : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Sprite Sprite;
    public Hex hex;


    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
      
    }


    public void setToken(Sprite s)
    {
        SpriteRenderer.sprite = s;
    }

    

    
}
