using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public int health;
    public bool ignoredamage; // if true ignore damage
    public float iframelength; // time it takes for the iframe to blink
    public int blips; // amount of blinks in iframe blink

    public float recoilForce = 15f;

    public healthBarScript healthbar;

    public GameObject Player;
    public SpriteRenderer playerSprite;

    public GameObject permaemepos;

    public TMPro.TMP_Text playerHealth;

    public Vector3 emepos;
    public void addHealth(int amount ) // Adds int health to player health e.g. -10 or 10
    {
        if (ignoredamage) { return; }

        health += amount;

        if (amount < 0)
        {
            //if (permaemepos != null) emepos = permaemepos.transform.position;
            Player.GetComponent<Rigidbody>().AddExplosionForce(recoilForce, emepos, 10f, 0f, ForceMode.Impulse);
            Debug.Log("Forced");
            StartCoroutine("Iframe");
        }


        if (health <= 0)
        {
            Die();
        }
    }

    public void Die() // Destroys player object OR runs enemy die function
    {
        try
        {
            GetComponent<BasicEnemy>().Die();
        }

        catch (System.Exception e)
        {
            print(e);
            Destroy(Player);
        }

    }

    IEnumerator Iframe() // Makes the player/enemy blink when hit and make them invincible for that time
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



    private void Start() // Start of script init
    {
        StartCoroutine("Iframe");
    }

    private void Update() // Update func
    {
        if (playerHealth != null) playerHealth.text = health.ToString();
        if (healthbar != null) healthbar.health = health;
    }
}
