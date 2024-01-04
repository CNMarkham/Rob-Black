using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public GameObject Player;

    public void Damage(int amount)
    {
        health -= amount;
        // StartCoroutine("Iframe");
        if (health <= 0)
        {
            Die();
        }
    }
    IEnumerator Iframe()
    {
        yield return new WaitForSeconds(2f);
    }
    public void Die()
    {
        Destroy(Player);
    }
}
