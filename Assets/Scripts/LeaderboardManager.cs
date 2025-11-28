using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardManager : MonoBehaviour
{
    public System.Action OnScoresChanged;
    private void Start()
    {
        Debug.Log("Quantidade no JSON: " + data.scores.Count);
    }

    [System.Serializable]
    public class PlayerScore
    {
        public string name;
        public int score;
    }

    [System.Serializable]
    public class LeaderboardData
    {
        public List<PlayerScore> scores = new List<PlayerScore>();
    }

    private string filePath;
    public string fileName;
    private LeaderboardData data;


    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + fileName;

        if (!File.Exists(filePath))
        {
            data = new LeaderboardData();
            SaveFile(); // cria o json
        }

        LoadFile();
    }

    // ==== SALVAR ====
    public void AddNewScore(string playerName, int score)
    {
        PlayerScore newScore = new PlayerScore();
        newScore.name = playerName;
        newScore.score = score;

        data.scores.Add(newScore);
        Debug.Log(data.scores);
        SaveFile();
        OnScoresChanged?.Invoke();
    }


    // ==== CARREGAR ====
    public List<PlayerScore> GetScores()
    {
        return data.scores;
    }

    private void SaveFile()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    public void LoadFile()
    {
        string json = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<LeaderboardData>(json);
    }
}
