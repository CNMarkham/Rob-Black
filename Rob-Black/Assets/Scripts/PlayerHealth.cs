using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public bool ignoredamage;
    public float iframelength;
    public int blips;

    public GameObject Player;
    public SpriteRenderer playerSprite;

    public void addHealth(int amount)
    {
        if (ignoredamage) { return; }

        health += amount;
        StartCoroutine("Iframe");
        if (health <= 0)
        {
            Die();
        }
    }
    IEnumerator Iframe()
    {
        ignoredamage = true;

        for (int i = 0; i < blips; i++)
        {
            yield return new WaitForSeconds((iframelength / blips) / 2);

            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.20f);

            yield return new WaitForSeconds((iframelength / blips) / 2);


            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.70f);

           

        }

        playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
        ignoredamage = false;
    }

    public void Die()
    {
        Destroy(Player);
    }

    private void Start()
    {
        StartCoroutine("Iframe");
    }
}
