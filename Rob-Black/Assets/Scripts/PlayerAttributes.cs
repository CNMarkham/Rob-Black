using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public GameObject Gun;

    public List<GameObject> playerGuns = new List<GameObject>();
    public int maximumGuns;
    public int currentGunIndex = 0;

    public GameObject SpriteChild;

    private bool scrollCooldown;
    public float scrollCooldownTime;

    // Start is called before the first frame update
    void Start()
    {
        scrollCooldown = false;
    }

    public void disableGunsAndEnableGun(int gunIndex)
    {
        foreach (GameObject gun in playerGuns)
        {
            gun.SetActive(false);
            gun.GetComponent<BasicGun>().disableShooting = false;
        }

        playerGuns[gunIndex].SetActive(true);

        Gun = playerGuns[gunIndex];

        Gun.GetComponent<BasicGun>().disableShooting = false;
    }

    IEnumerator startScrollCooldown(float timetowait)
    {
        scrollCooldown = true;
        yield return new WaitForSeconds(timetowait);
        scrollCooldown = false;
    }

    IEnumerator scrollGuns(int scrollBy) // eww Selenium
    {
        if (currentGunIndex < 0 || scrollCooldown) {  yield break; }

        int newGunIndex = currentGunIndex + scrollBy;
        print(newGunIndex);

        if (newGunIndex > playerGuns.Count - 1) { newGunIndex = 0; }
        if (newGunIndex < 0) { newGunIndex = playerGuns.Count - 1; }

        currentGunIndex = newGunIndex;

        StartCoroutine(startScrollCooldown(scrollCooldownTime));

        disableGunsAndEnableGun(currentGunIndex);
    }

    // Update is called once per frame
    void Update()
    {
        // if (playerGuns.Count != currentGunIndex) { currentGunIndex = 0; }
        if (playerGuns.Count != 0) { Gun = playerGuns[currentGunIndex]; }

        var scrollDeltaY = (int)Input.mouseScrollDelta.y;

        if (scrollDeltaY != 0)
        {
            var NewCoke = (int)(scrollDeltaY / Mathf.Abs(scrollDeltaY));

            StartCoroutine(scrollGuns(NewCoke));

            //print(NewCoke);
        }

  
    }
}
