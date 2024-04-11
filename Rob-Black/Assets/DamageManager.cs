using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public int health;
    public bool ignoredamage;
    public float iframelength;
    public int blips;

    public float recoilForce = 15f;

    public healthBarScript healthbar;

    public GameObject Player;
    public SpriteRenderer playerSprite;

    public TMPro.TMP_Text playerHealth;

    public Vector3 emepos;
    public void addHealth(int amount )
    {
        if (ignoredamage) { return; }

        health += amount;

        if (amount < 0)
        {
            Player.GetComponent<Rigidbody>().AddExplosionForce(recoilForce, emepos, 10f, 0f, ForceMode.Impulse);
            StartCoroutine("Iframe");
        }


        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(Player);
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



    private void Start()
    {
        StartCoroutine("Iframe");
    }

    private void Update()
    {
        playerHealth.text = health.ToString();
        healthbar.health = health;
    }
}
