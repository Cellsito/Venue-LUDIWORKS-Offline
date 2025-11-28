using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SaveNameCode : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject leaderBoard;

    public TMP_InputField nameInput;
    public void SaveName()
    {
        string playerName = nameInput.text;
        Debug.Log("Nome digitado: " + playerName);
        if (string.IsNullOrEmpty(playerName)) return;

        ScoreManager scoreManager = ScoreManager.Instance;

        FindObjectOfType<LeaderboardManager>().AddNewScore(playerName, scoreManager.filledTiles);

        gameOver.SetActive(false);
        leaderBoard.SetActive(true);
    }
}
