using UnityEngine;
using TMPro;
using System.Linq;

public class LeaderboardUI : MonoBehaviour
{
    public LeaderboardManager leaderboardManager;
    public GameObject scoreEntryPrefab;      // Score Entry
    public Transform contentParent;          // ScrollView/Viewport/Content

    void Start()
    {
        leaderboardManager.OnScoresChanged += ListarScores;
        ListarScores();
    }
    public void ListarScores()
    {
        // limpa itens anteriores
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // pega dados do json (ordenado opcionalmente)

        var scores = leaderboardManager.GetScores()
            .OrderByDescending(s => s.score)
            .ToList();

        foreach (var s in scores)
        {
            GameObject entry = Instantiate(scoreEntryPrefab, contentParent);

            // Acessa os textos do Score Entry:
            TextMeshProUGUI nameText = entry.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = entry.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            nameText.text = s.name;
            scoreText.text = s.score.ToString();
        }
        leaderboardManager.LoadFile();
    }
}
