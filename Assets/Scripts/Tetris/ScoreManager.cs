using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int filledTiles = 0;   // occupied spaces (0-200)
    public int fullRows = 0;      // fully filled rows
    public int maxTiles = 200;    // 10 x 20

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI rowText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    // Called whenever a Piece is placed
    public void RecalculateScore(Tilemap tilemap)
    {
        // SE Tilemap for nulo, NÃO zerar mais — apenas sair.
        if (tilemap == null)
        {
            UpdateUI();
            return;
        }

        filledTiles = 0;
        fullRows = 0;

        BoundsInt bounds = tilemap.cellBounds;

        // Count occupied tiles
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
                filledTiles++;
        }

        if (filledTiles > maxTiles)
            filledTiles = maxTiles;

        // Count full rows
        CountFullRows(tilemap);

        UpdateUI();
    }

    private void CountFullRows(Tilemap tilemap)
    {
        fullRows = 0;

        int width = 10;
        int height = 20;

        for (int y = -height / 2; y < height / 2; y++)
        {
            int count = 0;

            for (int x = -width / 2; x < width / 2; x++)
            {
                if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                    count++;
            }

            if (count == width)
                fullRows++;
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Ocupado: " + filledTiles + " / " + maxTiles;

        if (rowText != null)
            rowText.text = "Linhas completas: " + fullRows;
    }

    // Chamado APENAS quando o jogador reiniciar manualmente
    public void ResetScore()
    {
        filledTiles = 0;
        fullRows = 0;
        UpdateUI();
    }
}
