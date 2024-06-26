using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dollabill : MonoBehaviour
{

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


    private void OnCollisionEnter(Collision collision)
    { // when touching player add money to player
        if (collision.gameObject.CompareTag("Player"))
        {
            index.idx.getPlayer().GetComponent<PlayerMoney>().addMoney(den);
            Destroy(gameObject);
        }
    }
}
