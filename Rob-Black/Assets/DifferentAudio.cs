using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentAudio : MonoBehaviour
{

    public AudioSource audiosource;

    public List<AudioClip> audioclips;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playaudios());
    }

    IEnumerator playaudios()
    {
        int i = 0;

        while (true)
        {
            yield return new WaitForSeconds(100);

            i++;

            audiosource.clip = audioclips[i % audioclips.Count];
            audiosource.Play();

        }

           
    }
}
