using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text HighScoreText;

    void Start()
    {
        DataManager.Instance.LoadScore();
        UpdateHighScoreText();
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    //Exit the game
    //Note: If run in unity editor, end playmode
    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    //Should be moved to DataManager or some other script, to avoid duplicates
    void UpdateHighScoreText()
    {
        HighScoreText.text = "High Scores\r\n\r\n";
        foreach (DataManager.Score score in DataManager.Instance.scores)
        {
            HighScoreText.text += $"{score.name} : {score.value}\r\n";
        }
    }
}
