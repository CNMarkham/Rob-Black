using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dollabill : BasicPickupObject
{
    public enum denomonation
    {
        hundred = 100,
        fifty = 50,
        tweny = 20,
        ten = 10,
        five = 5,
        one = 1
    }

    public static int denomonationtoindex(denomonation den)
    {
        switch (den)
        {
            case denomonation.hundred:
                return 0;

            case denomonation.fifty:
                return 1;

            case denomonation.tweny:
                return 2;

            case denomonation.ten:
                return 3;

            case denomonation.five:
                return 4;

            default:
                return 5;
        }
    }

    public denomonation den;
    public override void OnPickup()
    {
        index.idx.getPlayer().GetComponent<PlayerMoney>().addMoney(den);
        Destroy(gameObject);
    }
}
