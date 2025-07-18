using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPay : EnemyDeathEvent
{
    public override void OnDeath(GameObject enemy) // drops bills on death
    {
        index.idx.drop_item(new Dictionary<GameObject, RangeInt>
        {
            { index.idx.bills[dollabill.denomonationtoindex(dollabill.denomonation.one)], index.idx.chance_out_of_one_hundred(10) },
            { index.idx.bills[dollabill.denomonationtoindex(dollabill.denomonation.five)], index.idx.chance_out_of_one_hundred(4) },
            { index.idx.bills[dollabill.denomonationtoindex(dollabill.denomonation.ten)], index.idx.chance_out_of_one_hundred(3) },
            { index.idx.bills[dollabill.denomonationtoindex(dollabill.denomonation.tweny)], index.idx.chance_out_of_one_hundred(2) },
            { index.idx.bills[dollabill.denomonationtoindex(dollabill.denomonation.fifty)], index.idx.chance_out_of_one_hundred(1) },
            { index.idx.bills[dollabill.denomonationtoindex(dollabill.denomonation.hundred)], index.idx.chance_out_of_one_hundred(1) },

            { index.idx.heart, index.idx.chance_out_of_one_hundred(70) }

        },

        new RangeInt(1, 100), enemy.transform);
    }

}
