using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunUIManager : MonoBehaviour
{
    public TMP_Text GunName;
    public TMP_Text Munitions;

    public GameObject raw;

    public BasicGun Gun;

    // Update is called once per frame

    public void setblack(bool isblack)// sets black ui element to be active if active
    {
        raw.SetActive(isblack);
    }

    void Update()
    {

        if (FindObjectOfType<PlayerAttributes>() == null) return;

        var GunObject = FindObjectOfType<PlayerAttributes>().Gun;

        if (GunObject == null) return;

        Gun = GunObject.GetComponent<BasicGun>();

        GunName.text = Gun.Name;

        if (Gun.isReloading)
        {
            Munitions.text = "Reloading...";
        }

        else
        {
            Munitions.text = $"{Gun.magazineSize - Gun.shotsFired} / {Gun.magazineSize} / {Gun.magazineCount}";
        }
    }
}
