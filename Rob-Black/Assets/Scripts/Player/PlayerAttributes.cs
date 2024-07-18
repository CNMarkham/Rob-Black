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
    public GameObject GunHolder;

    private bool scrollCooldown;
    public float scrollCooldownTime;

    public GameObject flashLight;
    public bool flashlightEnabled;



    // Start is called before the first frame update
    void Start()
    {
        scrollCooldown = false;
    }

    public void disableGunsAndEnableGun(int gunIndex) // this disables all of the guns currently in inventory and then enables the gun @ index
    {
        foreach (GameObject gun in playerGuns)
        {
            gun.SetActive(false);

            BasicGun bg = gun.GetComponent<BasicGun>();

            bg.disableShooting = false;
            bg.isHeld = false;
           
        }

        playerGuns[gunIndex].SetActive(true);

        Gun = playerGuns[gunIndex];

        BasicGun bgb = Gun.GetComponent<BasicGun>();

        bgb.disableShooting = false;
        bgb.isHeld = true;
    }

    IEnumerator startScrollCooldown(float timetowait)
    {
        scrollCooldown = true;
        yield return new WaitForSeconds(timetowait);
        scrollCooldown = false;
    }

    IEnumerator scrollGuns(int scrollBy) // eww Selenium reference?, it scrolls the guns by a dactor of scrollby
    {
        if (currentGunIndex < 0 || scrollCooldown) {  yield break; }

        int newGunIndex = currentGunIndex + scrollBy;
        //print(newGunIndex);

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

        if (Input.GetKeyDown(KeyCode.G))
        {
            index.idx.guntoaparatus(Gun, currentGunIndex, index.idx.Player.transform.position + new Vector3(-1, -1, -1));
        }

        if (flashlightEnabled) { flashLight.SetActive(true); }

        if (scrollDeltaY != 0 && Gun != null)
        {
            var NewCoke = (int)(scrollDeltaY / Mathf.Abs(scrollDeltaY));

            StartCoroutine(scrollGuns(NewCoke));

            //print(NewCoke);
        }

  
    }
}
