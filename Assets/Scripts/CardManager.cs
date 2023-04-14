using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private int currantPlayer;

    [SerializeField] private List<Cards> Deck = new List<Cards>();
    [SerializeField] private List<CardHand> CardHands = new List<CardHand>();
    [SerializeField] private Image display;

    [SerializeField] private Cards currantCard;
    [SerializeField] private int numberCard;

    [SerializeField] private GameObject cardViewer;

    [SerializeField] private GameObject nextBtn;
    [SerializeField] private GameObject prevBtn;
    [SerializeField] private GameObject useBtn;
    


    void Start()
    {
        cardViewer.SetActive(false);
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
}
