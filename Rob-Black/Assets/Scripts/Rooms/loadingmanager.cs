using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingmanager : MonoBehaviour
{
    public GameObject loadingui;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disableloadinganimation());
    }

    IEnumerator disableloadinganimation()
    {
        yield return new WaitForSecondsRealtime(5);
        loadingui.SetActive(false);
    }
}
