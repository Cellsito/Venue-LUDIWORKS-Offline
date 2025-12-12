using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerPoints : MonoBehaviour
{
    public int points = 0;
    public TextMeshProUGUI scoreText;
    public GameObject floatingWinPrefab;  
    public GameObject floatingLosePrefab;  
    public Transform canvasTransform;      
    

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        points += amount;
        MostrarFeedback(true, new Vector3(-10, -10, 0));
        UpdateUI();
    }

    public void RemoveScore(int amount)
    {
        points -= amount;
        if (points < 0) points = 0;

        MostrarFeedback(false, new Vector3(-10, -10, 0));
        UpdateUI();
    }

    void MostrarFeedback(bool ganhou, Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        GameObject prefab = ganhou ? floatingWinPrefab : floatingLosePrefab;

        GameObject efeito = Instantiate(prefab, canvasTransform);
        efeito.GetComponent<RectTransform>().anchoredPosition = screenPos;
    }


    void UpdateUI()
    {
        scoreText.text = "Pontos: " + points;
    }
}
