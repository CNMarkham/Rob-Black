using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{

    public bool paused = false;
    public bool synced = false;

    public List<GameObject> tobeinvisible;

    void setcanshoot(bool value)
    {

        BasicGun bg = index.idx.playerAttributes.Gun.GetComponent<BasicGun>();
        if (bg!=null) bg.canShoot = value;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
            synced = false;
        }
        //TODO: Consider making a button in the pause menu instead, since escape is also how the player exits fullscreen in WebGL
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Startup");
        }

        if (!synced)
        {
            if (paused)
            {
                setcanshoot(false);

                Time.timeScale = 0;

                foreach (GameObject go in tobeinvisible)
                {
                    go.SetActive(true);
                }

                synced = true;
            }

            else if (!paused)
            {
                setcanshoot(true);

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
