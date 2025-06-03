using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageManager : MonoBehaviour
{
    public bool VARYSIZE = false;
    public float VARYSIZEPERCENTMIN = 1f;
    public float VARYSIZEPERCENTMAX = 5f;

    public int health;
    public int maxHealth = 0;
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

    public void addHealth(int amount, bool stop_at_max = true) // Adds int health to player health e.g. -10 or 10
    {
        if (ignoredamage) { return; }

        if (stop_at_max)
        {
            if (health + amount > maxHealth)
            {
                health = maxHealth;
            }

            else
            {
                health += amount;
            }
        }

        else
        {
            health += amount;
        }

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

    public void Die() // Destroys player object OR runs enemy die function
    {
        try
        {
            GetComponent<BasicEnemy>().Die();
        }

        catch
        {

            if (playerHealth != null) 
            {

                Destroy(Player);
                
                ScoreKeeper.lost = true;
                ScoreKeeper.updated = false;

                SceneManager.LoadScene("Ending");
            
            }
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
        if (maxHealth == 0) { maxHealth = health; }

        if (VARYSIZE)
        {
            float newscale = gameObject.transform.localScale.x + Random.Range(VARYSIZEPERCENTMIN, VARYSIZEPERCENTMAX) / 100;
            gameObject.transform.localScale = new Vector3(newscale, newscale, newscale);
        }

        PiranhaAI pai = GetComponent<PiranhaAI>();

        if (pai != null)
        {
            this.maxHealth = pai.health;
            this.health = pai.health;
        }

        StartCoroutine("Iframe");
    }

    private void Update() // Update func
    {
        if (playerHealth != null) playerHealth.text = health.ToString();
        if (healthbar != null) healthbar.health = health;
    }
}
