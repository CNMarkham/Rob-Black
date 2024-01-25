using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public GameObject Gun;

    public List<GameObject> playerGuns = new List<GameObject>();
    public int currentGunIndex = 0;

    public GameObject SpriteChild;

    private bool scrollCooldown;
    public float scroll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void disableGuns(int gunIndex)
    {
        foreach (GameObject gun in playerGuns)
        {
            gun.SetActive(false);
        }

        playerGuns[gunIndex].SetActive(true);
    }

    IEnumerator scrollGuns(int scrollBy) // eww Selenium
    {
        if (currentGunIndex <= 0) { yield break; }

        currentGunIndex += scrollBy;

        if (scrollCooldown) { yield break; }
        scrollCooldown = true;

        disableGuns(currentGunIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGuns.Count != currentGunIndex) { currentGunIndex = 0; }
        if (playerGuns.Count != 0) { Gun = playerGuns[currentGunIndex]; }

        print(Input.mouseScrollDelta.y);

  
    }
}
