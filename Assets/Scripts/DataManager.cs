using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public Score[] scores { get; private set; }
    public string playerName;

    private void Awake()
    {
        if (null != Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.scores = scores;

        string json = JsonUtility.ToJson(data);

        string jsonPath = Application.dataPath + "/savefile.json";
        Debug.Log("Saving data to " + jsonPath);
        File.WriteAllText(jsonPath, json);
    }

    public void LoadScore()
    {
        string path = Application.dataPath + "/savefile.json";
        if (File.Exists(path))
        {
            Debug.Log("Loading data from " + path);
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            scores = data.scores;
        }
        else
        {
            scores = new Score[10];
            for (int i = 0; i < 10; i++)
            {
                scores[i] = new Score("AAA", 0);
            };
        }
    }

    public void AddScore(string name, int score)
    {
        if (scores[9].value < score)
        {
            scores[9] = new Score(name, score);
        }
        Array.Sort(scores, new ScoreComparer());
    }

    [Serializable]
    private class SaveData
    {
        public Score[] scores;
    }

    [Serializable]
    public class Score
    {
        public string name;
        public int value;

        public Score(string name, int score)
        {
            this.name = name;
            this.value = score;
        }
    }

    private class ScoreComparer : IComparer
    {
        public int Compare(object a, object b)
        {
            Score score1 = (Score)a;
            Score score2 = (Score)b;

            if (score1.value < score2.value)
                return 1;
            if (score1.value > score2.value)
                return -1;
            else
                return 0;
        }
    }
}
