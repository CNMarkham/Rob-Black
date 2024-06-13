using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBarScript : MonoBehaviour
{

    public int health;
    public int maxhealth = 100;

    public GameObject greenPart;
    public GameObject blackPart;

    public float[] maxgreenwidthtohealthratio = new float[2];
    public float[] maxblackwidthtohealthratio = new float[2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() // changes size of green bar and black bar relative to eachother;
    {
        greenPart.transform.localScale = new Vector3((maxgreenwidthtohealthratio[0] / maxgreenwidthtohealthratio[1]) * (float)health, greenPart.transform.localScale.y, greenPart.transform.localScale.z);
        blackPart.transform.localScale = new Vector3((maxblackwidthtohealthratio[0] / maxblackwidthtohealthratio[1]) * (float)maxhealth, blackPart.transform.localScale.y, blackPart.transform.localScale.z);
    }
}
