using UnityEngine;

public class ClosePanelOnEnter : MonoBehaviour
{
    [Tooltip("Arraste aqui o GameObject do painel que deve ser fechado")]
    public GameObject panel;

    [Tooltip("Se true, o script restaurará Time.timeScale = 1 ao fechar")]
    public bool restoreTimeScale = true;

    public static bool isClosed = false;

    private void Start()
    {
        if (isClosed) return;
        panel.SetActive(true);
    }
    void Update()
    {
        if (panel == null) return;

        if (panel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            panel.SetActive(false);
            isClosed = true;
            if (restoreTimeScale)
                Time.timeScale = 1f;
        }
    }
}
