using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    [SerializeField] private Sprite Sprite;
    [SerializeField] private int cardID;
    [SerializeField] private bool isImmediate;

    public Sprite grabSprite()
    {
        return Sprite;
    }
 
    public int returnCardID()
    {
        return cardID;
    }

    public bool returnImmediate() { return isImmediate; }
}
