using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dollabill : BasicPickupObject
{
    //TODO: Set the equals to the actual value amount. For example, one = 1, five = 5, ten = 10, etc
    public enum denomonation
    {
        hundred = 0,
        fifty = 1,
        tweny = 2,
        ten = 3,
        five = 4,
        one = 5
    }

    public denomonation den;
    public override void OnPickup()
    {
        index.idx.getPlayer().GetComponent<PlayerMoney>().addMoney(den);
        Destroy(gameObject);
    }
}
