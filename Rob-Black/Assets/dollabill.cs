using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dollabill : MonoBehaviour
{

    public enum denomonation
    {
        hundred,
        fifty,
        tweny,
        ten,
        five,
        one
    }

    public denomonation den;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            index.idx.getPlayer().GetComponent<PlayerMoney>().addMoney(den);
            Destroy(gameObject);
        }
    }
}
