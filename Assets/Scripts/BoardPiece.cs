using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    private Material material;


    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        
    }

    public void ClickEvent()
    {
        // null check
        if (meshRenderer != null)
        {
/*          how this works
            each piece is given a trigger that reacts when the user clicks within the collider
            check prefab to get a good idea of the triggers
            when clicked on, the game object still remains on the scene
            the mesh renderer is simply enable/disabled
            
            this method is called from the game manager
*/
            // shows or hides pieces
            meshRenderer.enabled = !meshRenderer.enabled;
        }
    }

    public void GetColor(int colorCode)
    {
        // color of set piece is changed based on input from game manager
        if (colorCode == 1)
        {
            material.color = Color.red;
        }
        else if (colorCode == 2)
        {
            material.color= Color.green;
        }
        else if (colorCode == 3)
        {
            material.color = Color.blue;
        }
        else if (colorCode == 4)
        {
            material.color = Color.yellow;
        }
    }
}
