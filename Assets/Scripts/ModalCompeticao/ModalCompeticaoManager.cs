using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModalCompeticaoManager : MonoBehaviour
{
    public GameObject modalCompeticao;
    public string gameTrello = "GameTrello";
    private PlayerMovement playerMovement;

    void Update()
    {
        // Encontrar o player só se ainda não pegou
        if (playerMovement == null)
            playerMovement = FindAnyObjectByType<PlayerMovement>();

        // Fechar modal com ESC
        if (modalCompeticao.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseModal();
        }
    }

    public void CloseModal()
    {
        modalCompeticao.SetActive(false);
    }

    public void GoToScene()
    {
        if (playerMovement != null)
        {
            // Salva posição do player antes de trocar de cena
            PlayerPrefs.SetFloat("PlayerX", playerMovement.playerPosition.position.x);
            PlayerPrefs.SetFloat("PlayerY", playerMovement.playerPosition.position.y);
            PlayerPrefs.Save();
        }

        LoadMiniGame();
    }

    private void LoadMiniGame()
    {
        Debug.Log("Carregando cena GameTrello...");
        SceneManager.LoadScene(gameTrello);
    }
}