using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    private Material material;
    [SerializeField] private int playcontroll = 0;
    [SerializeField] private bool unUseable;
    enum pieceType {noBuild, noRoad, road, settlement, city}
    [SerializeField] pieceType pt;
    [SerializeField] private GameObject canPick;
    [SerializeField] private BPmanager bpm;

    void Start()
    {
        
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        material = GetComponentInChildren<MeshRenderer>().material;
        setCanPick(false);
        
        
    }

    private void Update()
    {
        
    }

    public void ClickEvent()
    {
        // null check
        if (meshRenderer != null)
        {
            BoardPiece boardPiece = this;
            /*          how this works
                        each piece is given a trigger that reacts when the user clicks within the collider
                        check prefab to get a good idea of the triggers
                        when clicked on, the game object still remains on the scene
                        the mesh renderer is simply enable/disabled

                        this method is called from the game manager
            */
            // shows or hides pieces
            //meshRenderer.enabled = !meshRenderer.enabled;
            if (pt == pieceType.noBuild && bpm.getPickInter() == true && unUseable == false)
            {
                meshRenderer.enabled = true;
                pt = pieceType.settlement;
                bpm.AvailableIntersOff();
                bpm.setPickInter(false);
                unUseable = true;
                startingBuild();
                bpm.GetPlayer(playcontroll).addIntersect(this.GetComponentInParent<Intersect>());
                bpm.GetPlayer(playcontroll).addScore(1);

            }

            if(pt == pieceType.noRoad && bpm.getPickRoad() == true && unUseable == false)
            {
                meshRenderer.enabled = true;
                pt = pieceType.road;
                bpm.AvailableRoadsOff();
                bpm.setPickRoad(false);
                unUseable = true;
                bpm.GetPlayer(playcontroll).addRoad(this.GetComponentInParent<Road>());
            }

            if(pt == pieceType.settlement && bpm.getPickCity() == true && this.playcontroll == bpm.getPlayTurn())
            {
                meshRenderer.enabled = false;
                pt = pieceType.city;
                bpm.setPickCity(false);
                this.GetComponentInParent<Intersect>().setCity(true);
                this.GetComponentInParent<Intersect>().setCityControll(material.color);
                bpm.AvailableIntersOff();
                bpm.GetPlayer(playcontroll).addScore(1);

            }
            
        }


    }

    public void startingBuild()
    {
        if (bpm.getAddStartInter() == true)
        {
            BoardPiece boardPiece = this;
            bpm.addToStartInters(boardPiece.GetComponentInParent<Intersect>());
            bpm.setAddStartInter(false);
            bpm.setupSettlementRoad(this.GetComponentInParent<Intersect>());
        }
        
    }

    public void GetColor(int colorCode)
    {
        

        // color of set piece is changed based on input from game manager
        if (colorCode == 1 && !unUseable)
        {
            material.color = Color.red; 
            playcontroll = colorCode;
        }
        else if (colorCode == 2 && !unUseable)
        {
            material.color= Color.green;
            playcontroll = colorCode;
        }
        else if (colorCode == 3 && !unUseable)
        {
            material.color = Color.blue;
            playcontroll = colorCode;
        }
        else if (colorCode == 4 && !unUseable)
        {
            material.color = Color.yellow;
            playcontroll = colorCode;
        }

        
    }

    public int getPlayColor()
    {
        return playcontroll;
    }

    public void SetColor(Color c)
    {
        material.color = c;
    }

    public void setCanPick(bool b)
    {
        canPick.GetComponent<Renderer>().enabled = b;
    }

    public bool isUnUseable()
    {
        return unUseable;
    }

    public void setUnUseable(bool b) { unUseable = b; }
}
