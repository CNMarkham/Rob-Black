using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunUIManager : MonoBehaviour
{
    public TMP_Text GunName;
    public TMP_Text Munitions;

    public BasicGun Gun;

    // Update is called once per frame
    void Update()
    {

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
