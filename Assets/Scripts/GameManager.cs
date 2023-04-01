using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject testSphere;
    [SerializeField] private int colorCode = 0;


    void Start()
    {
        // finds all "assets" in the game and disables meshrenderer
        foreach (var assets in GameObject.FindGameObjectsWithTag("Asset"))
        {
            assets.GetComponent<MeshRenderer>().enabled = false;
        }

        Debug.Log("Press between 1~4 to change color of pieces you click on");
    }

    void Update()
    {
        // left click
        if (Input.GetMouseButtonDown(0))
        {
            // field for storing where the mouse is
            Vector3 clickPosition = Vector3.one;
            // this restricts the player to only be able to click at y position 0.15
            // not sure why it's negative
            Plane plane = new Plane(Vector3.up, -0.15f);
            // points mouse from the camera towards the board
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float distanceToPlane;

            if(plane.Raycast(ray, out distanceToPlane))
            {
                // the position that mouse is clicking
                clickPosition = ray.GetPoint(distanceToPlane);
                // creates a sphere that interacts with whatever is near the click position
                Collider[] colliders = Physics.OverlapSphere(clickPosition, 0.1f);

                // anything caught within the sphere created
                foreach (var collider in colliders)
                {
                    // very barebones board interaction
                    if (collider.tag == "Asset")
                    {
                        // changes color based on selected color
                        // in the future this will be based on whose turn it currently is
                        collider.GetComponent<BoardPiece>().GetColor(colorCode);
                        // basic code to check that settlements and roads can be interacted with
                        collider.GetComponent<BoardPiece>().ClickEvent();
                    }
                }
            }

            // since it's a board it should be fine restricting clicks to a single y coordinate
            Debug.Log(clickPosition);
        }


        // mostly for testing and changing color
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            colorCode = 1;
            Debug.Log("Color: red");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            colorCode = 2;
            Debug.Log("Color: green");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            colorCode = 3;
            Debug.Log("Color: blue");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            colorCode = 4;
            Debug.Log("Color: yellow");
        }
    }
}
