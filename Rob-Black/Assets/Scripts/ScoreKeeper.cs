using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public static int score;
    public static bool lost;

    public static bool updated;

    void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
        updatescore();
    }
    
    void updatescore()
    {
        var highscore = GameObject.FindGameObjectWithTag("HighScore");

        string scores = "";

        if (highscore)
        {

            string prefscores = PlayerPrefs.GetString("scoreboard", ",ROB:4,PI:3,E:2");

            List<List<string>> scorevecs = new();

            if (prefscores == "") return;

            foreach (string score in prefscores.Split(","))
            {
                if (score == "") continue;
                string[] splitscore = score.Split(":");
                scorevecs.Add(new() { splitscore[0], splitscore[1] });
            }

            scorevecs.OrderBy((vec) => vec[1]);

            foreach (List<string> vec in scorevecs)
            {
                scores += $"{vec[0]} - {vec[1]}\n";
            }

        }

        if (highscore != null)
        {
            var hst = highscore.GetComponent<TMPro.TMP_Text>();
            if (hst != null) hst.text = scores;
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        updatescore();
    }

    private void Update()
    {
        while (!updated && lost)
        {
            var scoreboard = GameObject.FindGameObjectWithTag("ScoreBoard");
            scoreboard.GetComponent<TMPro.TMP_Text>().text = "Your Floor: " + score.ToString();
            
            updated = true;
        }
    }

    public static void save()
    {
        var username = GameObject.FindGameObjectWithTag("UserName");

        PlayerPrefs.SetString(
            "scoreboard",
            PlayerPrefs.GetString("scoreboard", "") + $",{username.GetComponent<TMPro.TMP_InputField>().text}:{score}"
         );
    }

}
