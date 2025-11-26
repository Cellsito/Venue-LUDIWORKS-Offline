using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{

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
    private LeaderboardData data;


    void Awake()
    {
        filePath = Application.persistentDataPath + "/leaderboard.json";

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
        SaveFile();
    }

    // ==== CARREGAR ====
    public List<PlayerScore> GetScores()
    {
        return data.scores;
    }

    // ==== FUNÇÕES DO ARQUIVO ====
    private void SaveFile()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    private void LoadFile()
    {
        string json = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<LeaderboardData>(json);
    }
}
