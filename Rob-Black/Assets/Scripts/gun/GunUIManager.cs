using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GunUIManager : MonoBehaviour
{
    public TMP_Text GunName;
    public TMP_Text Munitions;

    public GameObject raw;

    public BasicGun Gun;
    public GameObject gunimage;

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

        Image img = gunimage.GetComponent<Image>();
        img.sprite = Gun.spriteRenderer.sprite;
        gunimage.transform.localScale = new Vector3(Gun.transform.localScale.x*Gun.viewResizeX*2*3.12f, Gun.transform.localScale.y*1/3*Gun.viewResizeY*0.61f, Gun.transform.localScale.z);
        img.color = Gun.spriteRenderer.color;
    }
}
