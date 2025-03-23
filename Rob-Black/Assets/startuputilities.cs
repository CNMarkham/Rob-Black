using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startuputilities : MonoBehaviour
{
    public void StartGame()
    {
        PlayerFloorCount.floorNumber = 0;
        SceneManager.LoadScene("GameSceneBackup", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
