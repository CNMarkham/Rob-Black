using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : BasicPickupObject
{

    public SpriteRenderer sprite;
    public bool flashRunning;

    public override void OnPickup()
    {
        var ph = index.idx.getPlayer().GetComponent<PlayerHealth>();
        if (ph.health >= ph.maxHealth) { return; };
        ph.addHealth(new System.Random().Next(10, 20));
        Destroy(gameObject);
    }

    public IEnumerator toggleFlash()
    {
        flashRunning = true;
        yield return new WaitForSeconds(1);
        this.transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f);
        yield return new WaitForSeconds(0.1f);
        this.transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f);
        flashRunning = false;
    }

    private void Update()
    {
        if (!flashRunning)
        {
            StartCoroutine(toggleFlash());
        }
    }

}
