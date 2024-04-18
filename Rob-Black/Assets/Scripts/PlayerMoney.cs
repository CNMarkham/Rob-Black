using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public int playerMoney;
    public TMPro.TMP_Text moneyText;

    public void addMoney(int money)
    {
        playerMoney += money;

    }

    public void addMoney(dollabill.denomonation money)
    {
        switch (money)
        {
            case dollabill.denomonation.hundred:
                addMoney(100);
                break;

            case dollabill.denomonation.fifty:
                addMoney(50);
                break;

            case dollabill.denomonation.tweny:
                addMoney(20);
                break;

            case dollabill.denomonation.ten:
                addMoney(10);
                break;

            case dollabill.denomonation.five:
                addMoney(5);
                break;

            case dollabill.denomonation.one:
                addMoney(1);
                break;

            default:
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = playerMoney.ToString();
    }
}
