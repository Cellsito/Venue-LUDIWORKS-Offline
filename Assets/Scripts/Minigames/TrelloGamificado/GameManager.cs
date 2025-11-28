using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    //public TextMeshProUGUI scoreText;

    public TextMeshProUGUI finalTimerText;
    public TextMeshProUGUI finalScoreText;
    public TMP_InputField nameInput;

    private float timer = 0f;
    private bool isPlaying = false;

    public int score = 0;
    private int finalScore;
    public int addScore = 100;
    public int removeScore = 50;

    public GameObject infoGame;
    public GameObject gameOver;
    public GameObject leaderBoard;
    public GameObject topPanel;

    public LeaderboardManager leaderboardManager;

    void Start()
    {
        isPlaying = true;
        UpdateUI();
        infoGame.SetActive(true);
        gameOver.SetActive(false);
        leaderBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
            UpdateUI();

            if (AllCardsPositioned())
            {
                Ended();
            }
        }
    }

    public void UpdateUI()
    {

        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);

        timerText.text = $"{min:00}:{sec:00}";

        //scoreText.text = score.ToString();

    }

    public void AddPoints()
    {
        score += addScore;
        UpdateUI();
    }
    public void RemovePoints()
    {
        score -= removeScore;
        if (score < 0) score = 0;
        UpdateUI();
    }

    public bool AllCardsPositioned()
    {
        CardController[] cards = FindObjectsOfType<CardController>();

        foreach (CardController card in cards) {
            if (!card.inArea || !card.inCorrectArea)
            {
                return false;
            }
        }
        return true;
    }

    public void Ended()
    {
        isPlaying = false;
        infoGame.SetActive(false);
        topPanel.SetActive(false);
        
        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);

        finalTimerText.text = $"Tempo final: {min:00}:{sec:00}";
        // Salvar valor abaixo em uma variavel

        finalScore = Mathf.RoundToInt(score * (100f / (timer + 1f)));
        finalScoreText.text =$"Pontuação final: {finalScore.ToString()}";

        gameOver.SetActive(true);

        Time.timeScale = 0f;
    }

    // ESTE MÉTODO PEGA O NOME DO INPUT FIELD
    public void SaveName()
    {
        string playerName = nameInput.text;
        Debug.Log("Nome digitado: " + playerName);
        if (string.IsNullOrEmpty(playerName)) return;


        FindObjectOfType<LeaderboardManager>().AddNewScore(playerName, finalScore);

        gameOver.SetActive(false);
        leaderBoard.SetActive(true);
    }
}
