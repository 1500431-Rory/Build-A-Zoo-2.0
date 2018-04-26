using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour {

    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5;
    public Text text6;
    public Text text7;
    public Text text8;
    Color colour;
    void Start()
    {
        colour = new Vector4(0.2f, 1, 0.2f, 1);
        //Text sets your text to say this message
        text1.text = "£" + NumberTrackers.totalMoney.ToString();
        text1.color = colour;
        text2.text = "£" + NumberTrackers.totalMoney.ToString();
        text2.color = colour;
        text3.text = "£" + NumberTrackers.totalMoney.ToString();
        text3.color = colour;
        text4.text = "£" + NumberTrackers.totalMoney.ToString();
        text4.color = colour;
        text5.text = "£" + NumberTrackers.totalMoney.ToString();
        text5.color = colour;
        text6.text = "£" + NumberTrackers.totalMoney.ToString();
        text6.color = colour;
        text7.text = "£" + NumberTrackers.totalMoney.ToString();
        text7.color = colour;
        text8.text = "£" + NumberTrackers.totalMoney.ToString();
        text8.color = colour;
    }

    void Update()
    {
        if (NumberTrackers.totalMoney <= 0)
        {
           colour = Color.red;
        }
        else
        {
            colour = new Vector4(0.2f, 1, 0.2f, 1);
        }

        text1.color = colour;
        text1.text = "£" + NumberTrackers.totalMoney.ToString();
        text2.color = colour;
        text2.text = "£" + NumberTrackers.totalMoney.ToString();
        text3.color = colour;
        text3.text = "£" + NumberTrackers.totalMoney.ToString();
        text4.color = colour;
        text4.text = "£" + NumberTrackers.totalMoney.ToString();
        text5.color = colour;
        text5.text = "£" + NumberTrackers.totalMoney.ToString();
        text6.color = colour;
        text6.text = "£" + NumberTrackers.totalMoney.ToString();
        text7.color = colour;
        text7.text = "£" + NumberTrackers.totalMoney.ToString();
        text8.color = colour;
        text8.text = "£" + NumberTrackers.totalMoney.ToString();   
    }
}
