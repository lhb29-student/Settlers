using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradingUI : MonoBehaviour
{
    [SerializeField] private Trading trading;
    [SerializeField] private Button[] tradeIn = new Button[6];
    [SerializeField] private Button[] tradeInAny3 = new Button[5];
    [SerializeField] private Button[] tradeInAny4 = new Button[5];
    [SerializeField] private TextMeshProUGUI tradeAnyText;
    [SerializeField] private List<string> resString;
    [SerializeField] private string tradeInString;
    [SerializeField] private string tradeOutString;

    private void Start()
    {
        tradeInString = resString[0];
        tradeOutString = resString[0];
    }
    // Update is called once per frame
    void Update()
    {
        buttonEnabling();
        updateText();
        trading3();
        trading4();
    }

    public void trading3()
    {
        if (trading.got3Lumber())
        {
            tradeInAny3[0].enabled = true;
            tradeInAny3[0].image.color = Color.white;
        }
        else
        {
            tradeInAny3[0].enabled = false;
            tradeInAny3[0].image.color = Color.gray;
        }
        if (trading.got3Wool())
        {
            tradeInAny3[1].enabled = true;
            tradeInAny3[1].image.color = Color.white;
        }
        else
        {
            tradeInAny3[1].enabled = false;
            tradeInAny3[1].image.color = Color.gray;
        }
        if (trading.got3Grain())
        {
            tradeInAny3[2].enabled = true;
            tradeInAny3[2].image.color = Color.white;
        }
        else
        {
            tradeInAny3[2].enabled = false;
            tradeInAny3[2].image.color = Color.gray;
        }
        if (trading.got3Ore())
        {
            tradeInAny3[3].enabled = true;
            tradeInAny3[3].image.color = Color.white;
        }
        else
        {
            tradeInAny3[3].enabled = false;
            tradeInAny3[3].image.color = Color.gray;
        }
        if (trading.got3Brick())
        {
            tradeInAny3[4].enabled = true;
            tradeInAny3[4].image.color = Color.white;
        }
        else
        {
            tradeInAny3[4].enabled = false;
            tradeInAny3[4].image.color = Color.gray;
        }
    }

    public void trading4()
    {
        if (trading.got4Lumber())
        {
            tradeInAny4[0].enabled = true;
            tradeInAny4[0].image.color = Color.white;
        }
        else
        {
            tradeInAny4[0].enabled = false;
            tradeInAny4[0].image.color = Color.gray;
        }
        if (trading.got4Wool())
        {
            tradeInAny4[1].enabled = true;
            tradeInAny4[1].image.color = Color.white;
        }
        else
        {
            tradeInAny4[1].enabled = false;
            tradeInAny4[1].image.color = Color.gray;
        }
        if (trading.got4Grain())
        {
            tradeInAny4[2].enabled = true;
            tradeInAny4[2].image.color = Color.white;
        }
        else
        {
            tradeInAny4[2].enabled = false;
            tradeInAny4[2].image.color = Color.gray;
        }
        if (trading.got4Ore())
        {
            tradeInAny4[3].enabled = true;
            tradeInAny4[3].image.color = Color.white;
        }
        else
        {
            tradeInAny4[3].enabled = false;
            tradeInAny4[3].image.color = Color.gray;
        }
        if (trading.got4Brick())
        {
            tradeInAny4[4].enabled = true;
            tradeInAny4[4].image.color = Color.white;
        }
        else
        {
            tradeInAny4[4].enabled = false;
            tradeInAny4[4].image.color = Color.gray;
        }
    }
    private void buttonEnabling()
    {
        if (trading.getLumberBool())
        {
            tradeIn[0].enabled = true;
            tradeIn[0].image.color = Color.white;
        }
        else
        {
            tradeIn[0].enabled = false;
            tradeIn[0].image.color = Color.gray;
        }

        if (trading.getWoolBool())
        {
            tradeIn[1].enabled = true;
            tradeIn[1].image.color = Color.white;
        }
        else
        {
            tradeIn[1].enabled = false;
            tradeIn[1].image.color = Color.gray;
        }

        if (trading.getGrainBool())
        {
            tradeIn[2].enabled = true;
            tradeIn[2].image.color = Color.white;
        }
        else
        {
            tradeIn[2].enabled = false;
            tradeIn[2].image.color = Color.gray;
        }

        if (trading.getOreBool())
        {
            tradeIn[3].enabled = true;
            tradeIn[3].image.color = Color.white;
        }
        else
        {
            tradeIn[3].enabled = false;
            tradeIn[3].image.color = Color.gray;
        }

        if (trading.getBrickBool())
        {
            tradeIn[4].enabled = true;
            tradeIn[4].image.color = Color.white;
        }
        else
        {
            tradeIn[4].enabled = false;
            tradeIn[4].image.color = Color.gray;
        }

        if (trading.getAnyBool())
        {
            tradeIn[5].enabled = true;
            tradeIn[5].image.color = Color.white;
        }
        else
        {
            tradeIn[5].enabled = false;
            tradeIn[5].image.color = Color.gray;
        }
    }

    public void setTradeInString(int i)
    {
        tradeInString = resString[i];
    }
    public void setTradeOutString(int i)
    {
        tradeOutString = resString[i];
    }
    private void updateText()
    {
        tradeAnyText.text = 
        "Trading " + trading.getTradeWithAmount() + " " + tradeInString + " For " + trading.getTradeForAmount() + " " + tradeOutString;
    }
}
