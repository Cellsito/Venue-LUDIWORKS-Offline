using TMPro;
using UnityEngine;

public class GetPointsTXT : MonoBehaviour
{
    public TextMeshProUGUI points;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void GetPointsPuzzle()
    {
        ScoreManager scoreManager;
        scoreManager = ScoreManager.Instance;

        points.text = $"Pontuação: {scoreManager.filledTiles.ToString()}";
    }

    public void GetPointsKahoot()
    {
        PlayerPoints playerPoints = FindAnyObjectByType<PlayerPoints>();
        points.text = $"Pontuação: {playerPoints.points.ToString()}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
