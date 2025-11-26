using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour
{
    private KeyCode interactable = KeyCode.E;

    public GameObject intButton;
    private CompetitionArea currentArea; // <-- área atual que o player está

    void Start()
    {
        currentArea = null;
        intButton.SetActive(false);
    }

    void Update()
    {
        if (currentArea != null && Input.GetKeyDown(interactable))
        {
            currentArea.modalToOpen.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CompetitionArea area))
        {
            currentArea = area;
            intButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CompetitionArea area))
        {
            if (area == currentArea)
            {
                currentArea = null;
                intButton.SetActive(false);
            }
        }
    }
}