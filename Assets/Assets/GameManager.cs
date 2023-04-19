using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject testSphere;
    [SerializeField] private int colorCode = 0;
    [SerializeField] private Color canPick;
    [SerializeField] private PlayLongRoad plr;
    [SerializeField] private tokenManager tm;
    [SerializeField] private BoardManager boardManager;
  
    
    void Start()
    {
        // finds all "assets" in the game and disables meshrenderer
        foreach (var assets in GameObject.FindGameObjectsWithTag("Asset"))
        {
            assets.GetComponent<MeshRenderer>().enabled = false;
            //assets.GetComponent<BoardPiece>().SetColor(canPick);
        }

        Debug.Log("Press between 1~4 to change color of pieces you click on");

        tm = GetComponent<tokenManager>();
        boardManager = GameObject.Find("Land").GetComponent<BoardManager>();
    }

    void Update()
    {
        // left click
        if (Input.GetMouseButtonDown(0) && boardManager.currentP == "p1")
        {
            // field for storing where the mouse is
            Vector3 clickPosition = Vector3.one;
            // this restricts the player to only be able to click at y position 0.15
            // not sure why it's negative
            Plane plane = new Plane(Vector3.up, -1.3f);
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

                        plr.findLongestPath();
                    }

                    if (collider.tag == "moveRob")
                    {
                        Debug.Log("Moved the robber here at " + collider.GetComponentInParent<Hex>().name);
                        tm.replaceRobber(collider.GetComponentInParent<Hex>());
                        tm.closeRobberSpace();
                        

                    }
                }
            }

            // since it's a board it should be fine restricting clicks to a single y coordinate
            //Debug.Log(clickPosition);
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

    public void AIClick(Vector3 clickPos)
    {
        // creates a sphere that interacts with whatever is near the click position
        Collider[] colliders = Physics.OverlapSphere(clickPos, 0.1f);

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

                plr.findLongestPath();
            }

            if (collider.tag == "moveRob")
            {
                Debug.Log("Moved the robber here at " + collider.GetComponentInParent<Hex>().name);
                tm.replaceRobber(collider.GetComponentInParent<Hex>());
                tm.closeRobberSpace();
            }
        }
    }

    public int getColorCode()
    {
        return colorCode;
    }
   
    // receives an input and sets colorCode to said input
    public void setColorCode(int codeNumber)
    {
        if(codeNumber > 4 || codeNumber <= 0)
        {
            Debug.Log("Invalid color code: " + codeNumber);
        }
        else
        {
            colorCode = codeNumber;
        }
    }
}
