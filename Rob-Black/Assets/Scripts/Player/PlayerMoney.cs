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

    public void addMoney(dollabill.denomonation money) // Adds money by denomonation e.g. twenty gives += 20 money
    {
        addMoney((int)money);
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
