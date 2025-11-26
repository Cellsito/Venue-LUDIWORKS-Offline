using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    [Header("SPRITES DISPONÍVEIS")]
    public Sprite[] spritesDisponiveis;

    [Header("REFERÊNCIAS UI")]
    public Image previewImage;
    public Button botaoConfirmar;

    private int indexAtual = 0;

    void Start()
    {
        // 🔹 Carrega o personagem salvo anteriormente (se existir)
        if (PlayerPrefs.HasKey("PersonagemSelecionado"))
        {
            indexAtual = PlayerPrefs.GetInt("PersonagemSelecionado");
            indexAtual = Mathf.Clamp(indexAtual, 0, spritesDisponiveis.Length - 1);
        }

        AtualizarPreview();

        // 🔹 Adiciona listener ao botão de confirmar
        if (botaoConfirmar != null)
            botaoConfirmar.onClick.AddListener(ConfirmarSelecao);
    }

    public void Proximo()
    {
        indexAtual++;
        if (indexAtual >= spritesDisponiveis.Length)
            indexAtual = 0;

        AtualizarPreview();
    }

    public void Anterior()
    {
        indexAtual--;
        if (indexAtual < 0)
            indexAtual = spritesDisponiveis.Length - 1;

        AtualizarPreview();
    }

    private void AtualizarPreview()
    {
        if (previewImage != null && spritesDisponiveis.Length > 0)
            previewImage.sprite = spritesDisponiveis[indexAtual];
    }

    public void ConfirmarSelecao()
    {
        // 🔹 Salva o personagem escolhido
        PlayerPrefs.SetInt("PersonagemSelecionado", indexAtual);
        PlayerPrefs.Save();

        Debug.Log($"✅ Personagem {indexAtual} confirmado e salvo!");

        // Aqui você pode adicionar feedback visual, som, etc.
        // Exemplo: mudar a cor do botão, tocar um som, mostrar uma mensagem...
    }
}
