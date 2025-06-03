using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startuputilities : MonoBehaviour
{
    public void StartGame()
    {
        PlayerFloorCount.floorNumber = 0;
        ScoreKeeper.score = 0;

        SceneManager.LoadScene("GameSceneBackup", LoadSceneMode.Single);
    }

    public void backtomainmenu()
    {
        SceneManager.LoadScene("Startup", LoadSceneMode.Single);
    }

    public void credits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    public void saveScore()
    {
        ScoreKeeper.save();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
