using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPay : EnemyDeathEvent
{
    public override void OnDeath(GameObject enemy) // drops bills on death
    {
        Debug.LogError(index.idx.randomChoice(index.idx.bills).GetComponent<dollabill>().den);

        index.idx.pay_bills(index.idx.randomChoice(index.idx.bills).GetComponent<dollabill>().den, enemy.transform);
    }

}
