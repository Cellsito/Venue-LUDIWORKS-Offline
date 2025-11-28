using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerPoints : MonoBehaviour
{
    public int points = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        points += amount;
        UpdateUI();
    }

    public void RemoveScore(int amount)
    {
        points -= amount;
        if (points < 0) points = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Pontos: " + points;
    }
}
