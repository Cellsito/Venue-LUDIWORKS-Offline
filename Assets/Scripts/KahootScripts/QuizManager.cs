using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections;
using static LeaderboardManager;
using UnityEngine.SocialPlatforms.Impl;

public class QuizManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshPro questionText;
    public TextMeshProUGUI timerText;
    public float roundTime = 10f;
    public GameObject gameOver;
    public GameObject leaderBoard;

    [Header("Answer Zones")]
    public AnswerZone2D[] answerZones; // zonas de resposta (0–3)
    private int correctZoneIndex;

    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] answers;
        public int correctIndex;
    }

    [Header("Perguntas")]
    public Question[] questions;
    private int currentIndex = 0;

    public int totalQuestions = 2;
    private int answeredCount = 0;

    private float timer;
    private bool roundActive = false;

    public TMP_InputField nameInput;
    void Start()
    {
        StartCoroutine(StartRound());
        gameOver.SetActive(false);
        leaderBoard.SetActive(false);
    }

    IEnumerator StartRound()
    {
        yield return new WaitForSeconds(1f);

        LoadNextQuestion();
        roundActive = true;
        timer = roundTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimer(Mathf.CeilToInt(timer));
            yield return null;
        }

        roundActive = false;
        EvaluateAnswers();

        if (answeredCount >= totalQuestions)
        {
            EndGame();
            yield break;
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(StartRound());

        answeredCount++;

    }

    void EndGame()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (var p in players)
        {
            PlayerPoints score = p.GetComponent<PlayerPoints>();
            Debug.Log($"{p.name} fez {score.points} pontos!");
        }



        // aqui mostra o painel de fim de jogo
        gameOver.SetActive(true);  

    }

    void LoadNextQuestion()
    {
        var q = questions[currentIndex];
        currentIndex = (currentIndex + 1) % questions.Length;

        // Garante que a pergunta tem 4 respostas
        if (q.answers == null || q.answers.Length != 4)
        {
            Debug.LogError($"A pergunta \"{q.question}\" não tem exatamente 4 alternativas!");
            return;
        }

        // embaralha as respostas
        int[] shuffled = Enumerable.Range(0, 4).OrderBy(x => Random.value).ToArray();
        correctZoneIndex = shuffled.ToList().IndexOf(q.correctIndex);

        DisplayQuestion(
            q.question,
            q.answers[shuffled[0]],
            q.answers[shuffled[1]],
            q.answers[shuffled[2]],
            q.answers[shuffled[3]],
            correctZoneIndex
        );
    }

    void DisplayQuestion(string question, string a1, string a2, string a3, string a4, int correctIndex)
    {
        questionText.text = question;
        correctZoneIndex = correctIndex;

        string[] answers = { a1, a2, a3, a4 };
        for (int i = 0; i < answerZones.Length; i++)
        {
            answerZones[i].SetAnswerText(answers[i]);
        }
    }

    void UpdateTimer(int timeLeft)
    {
        timerText.text = $" {timeLeft}s";
    }

    void EvaluateAnswers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (var p in players)
        {
            PlayerZoneChecker2D checker = p.GetComponent<PlayerZoneChecker2D>();
            PlayerPoints points = p.GetComponent<PlayerPoints>();

            if (checker == null || points == null)
                continue;

            // não estava em nenhuma resposta
            if (checker.currentZone == -1)
            {
                Debug.Log($"{p.name} não respondeu.");
                continue;
            }

            // resposta correta
            if (checker.currentZone == correctZoneIndex)
            {
                Debug.Log($"{p.name} acertou!");
                points.AddScore(20); // você escolhe quanto vale
            }
            else
            {
                Debug.Log($"{p.name} errou!");
                points.RemoveScore(15); // perde pontos
            }
        }
    }

    public void SaveName()
    {
        string playerName = nameInput.text;
        if (string.IsNullOrEmpty(playerName)) return;

        Debug.Log("Nome digitado: " + playerName);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerPoints points = player.GetComponent<PlayerPoints>();
        FindObjectOfType<LeaderboardManager>().AddNewScore(playerName, points.points);

        gameOver.SetActive(false);
        leaderBoard.SetActive(true);
    }
}