using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SaveNameCode : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject leaderBoard;

    public void SaveName()
    {
        //string playerName = nameInput.text;
        //Debug.Log("Nome digitado: " + playerName);

        //FindObjectOfType<LeaderboardManager>().AddNewScore(playerName, points);

        gameOver.SetActive(false);
        leaderBoard.SetActive(true);
    }
}
