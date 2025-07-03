using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public int playerMoney;
    public TMPro.TMP_Text moneyText;

    public GameObject tobepurchased;

    public void addMoney(int money) // money += x
    {
        playerMoney += money;

    }

    //TODO: After making change to the dollabill enum, use the enum directly by casting to an integer.
    public void addMoney(dollabill.denomonation money) // Adds money by denomonation e.g. twenty gives += 20 money
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

        if (Input.GetKeyDown(KeyCode.E) && tobepurchased != null)
        { 
            var ghs = tobepurchased.GetComponent<gunholderspawner>();

            if (playerMoney >= ghs.price)
            {
                addMoney(-ghs.price);
                ghs.nopickup = false;
                ghs.gpa.playercol(index.idx.Player);
            }
        }

        moneyText.text = playerMoney.ToString();
    }
}
