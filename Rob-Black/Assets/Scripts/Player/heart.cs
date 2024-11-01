using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : BasicPickupObject
{
    public override void OnPickup()
    {
        var ph = index.idx.getPlayer().GetComponent<PlayerHealth>();
        if (ph.health >= ph.maxHealth) { return; };
        ph.addHealth(new System.Random().Next(10, 20));
        Destroy(gameObject);
    }
}
