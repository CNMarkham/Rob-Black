using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startuputilities : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameSceneBackup");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
