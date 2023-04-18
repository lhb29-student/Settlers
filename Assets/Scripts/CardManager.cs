using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static resources;

public class CardManager : MonoBehaviour
{
    [SerializeField] private int currantPlayer;

    [SerializeField] private List<Cards> Deck = new List<Cards>();
    [SerializeField] private List<CardHand> CardHands = new List<CardHand>();
    [SerializeField] private List<PlayerResources> allResources;
    [SerializeField] private Image display;

    [SerializeField] private Cards currantCard;
    [SerializeField] private int numberCard;

    [SerializeField] private GameObject cardViewer;
    [SerializeField] private GameObject resourceViewer;
    [SerializeField] private TextMeshProUGUI resourceText;
    [SerializeField] private bool yopOrMonop;
    [SerializeField] private int yopCount;
    [SerializeField] private PlayerResources inPlayResources;

    [SerializeField] private GameObject nextBtn;
    [SerializeField] private GameObject prevBtn;
    [SerializeField] private GameObject useBtn;
    


    void Start()
    {
        cardViewer.SetActive(false);
        resourceViewer.SetActive(false);
    }

    public Cards drawCard()
    {
        if(Deck.Count >= 1)
        {
            Cards c = Deck[Random.Range(0, Deck.Count)];
            Deck.Remove(c);
            return c;
        }
        return null;
    }

    public void setDisplay(Sprite s)
    {
        display.GetComponent<Image>().sprite = s;
    }

    public void openDisplay()
    {
        if (CardHands[0].returnHand().Count >= 1)
        {
            cardViewer.SetActive(true);
            List<Cards> playerCards = CardHands[0].returnHand();
            numberCard = 0;
            viewOnly(false);
            currantCard = playerCards[numberCard];
            display.sprite = currantCard.grabSprite();
            if (playerCards[numberCard].returnImmediate() == true)
            {
                useBtn.SetActive(false);
            }
            else
            {
                useBtn.SetActive(true);
            }
        }
    }

   
    public void prevDevCard()
    {
        if (numberCard > 0)
        {
            List<Cards> playerCards = CardHands[0].returnHand();
            numberCard--;
            currantCard = playerCards[numberCard];
            display.sprite = currantCard.grabSprite();
            if (playerCards[numberCard].returnImmediate() == true)
            {
                useBtn.SetActive(false);
            }
            else
            {
                useBtn.SetActive(true);
            }
        }
    }

    public void nextDevCard() 
    {
        List<Cards> playerCards = CardHands[0].returnHand();
        if (numberCard < playerCards.Count-1)
        {
            numberCard++;
            currantCard = playerCards[numberCard];
            display.sprite = currantCard.grabSprite();
            if (playerCards[numberCard].returnImmediate() == true)
            {
                useBtn.SetActive(false);
            }
            else
            {
                useBtn.SetActive(true);
            }
        }
    }

    public void closeDisplay()
    {
        cardViewer.SetActive(false);
    }

    public void cardToPlayHand()
    {
        Cards c = drawCard();
        CardHands[currantPlayer].addHand(c);
        openDisplay();
        viewOnly(true);
        display.sprite = c.grabSprite();
    }
    public void playCard()
    {
        CardHands[0].playCard(currantCard);
        closeDisplay();
    }

    public void viewOnly(bool b)
    {
        if(b == true)
        {
            useBtn.SetActive(false);
            prevBtn.SetActive(false);
            nextBtn.SetActive(false);
        }
        else
        {
            useBtn.SetActive(true);
            prevBtn.SetActive(true);
            nextBtn.SetActive(true);
        }
    }
    public void openResourseDisplay()
    {
        resourceViewer.SetActive(true);
    }
    public void closeResourseDisplay()
    {
        resourceViewer.SetActive(false);
    }

    public void setYopOrMonop(bool b)
    {
        yopOrMonop = b;
    }

    public void callResource(int rt)
    {
        if(yopOrMonop == true)
        {
            monopoly(rt);
        }
        else
        {
            yearOfPlenty(rt);
        }
    }

    public void setInPlayResources(PlayerResources pl)
    {
        inPlayResources = pl;
    }

    public void setResourceText(string s)
    {
        resourceText.text = s;
    }

    public void yearOfPlenty(int rt)
    {
        
        if (rt == 0)
        {
            inPlayResources.AddBrick();
            yopCount++;
        }
        else if (rt == 1)
        {
            inPlayResources.AddWood();
            yopCount++;
        }
        else if (rt == 2)
        {
            inPlayResources.AddOre();
            yopCount++;
        }
        else if (rt == 3)
        {
            inPlayResources.AddWheat();
            yopCount++;
        }
        else if (rt == 4)
        {
            inPlayResources.AddWool();
            yopCount++;
        }

        resourceText.text = "Select second resource";
        if (yopCount == 2) {
            yopCount = 0;
            closeResourseDisplay();
            inPlayResources = null;
        }      
    }

    public void monopoly(int rt)
    {
        if (rt == 0)
        {
            int grabBrick = 0;
            foreach (PlayerResources pl in allResources)
            {
                if (pl != inPlayResources)
                {
                    grabBrick = grabBrick + pl.loseOre();
                }
            }
            inPlayResources.AddMoreBrick(grabBrick);
            
        }
        else if (rt == 1)
        {
            int grabWood = 0;
            foreach (PlayerResources pl in allResources)
            {
                if (pl != inPlayResources)
                {
                    grabWood = grabWood + pl.loseBrick();
                }
            }
            inPlayResources.AddMoreWood(grabWood);
            
        }
        else if (rt == 2)
        {
            int grabOre = 0;
            foreach (PlayerResources pl in allResources)
            {
                if (pl != inPlayResources)
                {
                    grabOre = grabOre + pl.loseOre();
                }
            }
            inPlayResources.AddMoreOre(grabOre);
            
        }
        else if (rt == 3)
        {
            int grabGrain = 0;
            foreach (PlayerResources pl in allResources)
            {
                if (pl != inPlayResources)
                {
                    grabGrain = grabGrain + pl.loseWheat();
                }
            }
            inPlayResources.AddMoreWheat(grabGrain);
        }
        else if (rt == 4)
        {
            int grabWool = 0;
            foreach (PlayerResources pl in allResources)
            {
                if (pl != inPlayResources)
                {
                    grabWool = grabWool + pl.loseWool();
                }
            }
            inPlayResources.AddMoreWool(grabWool);
        }
        closeResourseDisplay();
    }
}
