using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{

    public bool paused = false;
    public bool synced = false;

    public List<GameObject> tobeinvisible;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
            synced = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Startup");
        }

        if (!synced)
        {
            if (paused)
            {
                index.idx.playerAttributes.Gun.GetComponent<BasicGun>().canShoot = false;

                Time.timeScale = 0;

                foreach (GameObject go in tobeinvisible)
                {
                    go.SetActive(true);
                }

                synced = true;
            }

            else if (!paused)
            {
                index.idx.playerAttributes.Gun.GetComponent<BasicGun>().canShoot = true;

                Time.timeScale = 1;

                foreach (GameObject go in tobeinvisible)
                {
                    go.SetActive(false);
                }

                synced = true;
            }
        }


    }
}
