using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossuiscript : MonoBehaviour
{
    [Header("edited")]
    public string bosstitle;
    public Color bossbarcolor;
    public int bossHealth;

    [Header("internal")]
    public GameObject healthArea;
    public Image healthAreaSprite;
    public float healthAreaOriginalSize;
    public float bossHealthPercentage;
    public int bossHealthOriginal;
    public TMPro.TMP_Text bosstitleobject;

    // Start is called before the first frame update
    void Start()
    {
        healthAreaSprite = healthArea.GetComponent<Image>();
        healthAreaOriginalSize = healthArea.transform.localScale.x;
        bossHealthOriginal = bossHealth;
    }

    private void FixedUpdate()
    {
        bossHealthPercentage = (bossHealth * 100) / bossHealthOriginal;

        healthArea.transform.localScale = new Vector3(
            (healthAreaOriginalSize * (bossHealthPercentage/100)),
            healthArea.transform.localScale.y,
            healthArea.transform.localScale.z
        ); ;

        healthAreaSprite.color = bossbarcolor;

        bosstitleobject.text = bosstitle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
