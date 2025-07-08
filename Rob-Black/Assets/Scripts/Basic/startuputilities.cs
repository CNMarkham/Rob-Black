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
    
    // Could be changed to be a single method, which itself is called from the button, but there's more friction when trying to make modifications to the scene
    public void backtomainmenu()
    {
        SceneManager.LoadScene("Startup", LoadSceneMode.Single);
    }

    public void credits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    public void instructions()
    {
        SceneManager.LoadScene("Instructions", LoadSceneMode.Single);
    }

    public void saveScore()
    {
        ScoreKeeper.save();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetScores()
    {
        PlayerPrefs.SetString("scoreboard", "");

        var highscore = GameObject.FindGameObjectWithTag("HighScore");
        highscore.GetComponent<TMPro.TMP_Text>().text = "";
    }
}
