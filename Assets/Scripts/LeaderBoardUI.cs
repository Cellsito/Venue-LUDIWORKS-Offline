using UnityEngine;
using TMPro;

public class LeaderBoardUI : MonoBehaviour
{
    public GameObject entryPrefab;
    public Transform contentParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    //public void ShowLeaderboard()
    //{
    //    var scores = LeaderboardManager.LoadScores();

    //    foreach (var score in scores)
    //    {
    //        GameObject newEntry = Instantiate(entryPrefab, contentParent);

    //        var texts = newEntry.GetComponentsInChildren<TextMeshProUGUI>();
    //        texts[0].text = score.playerName;
    //        texts[1].text = score.score.ToString();

    //        newEntry.SetActive(true);
    //    }
    //}
}
