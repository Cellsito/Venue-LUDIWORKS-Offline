using UnityEngine;
using UnityEngine.Tilemaps;

[DefaultExecutionOrder(-1)]
public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }

    public TetrominoData[] tetrominoes;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);

    public bool isGameOver { get; private set; } = false;

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < tetrominoes.Length; i++)
            tetrominoes[i].Initialize();
    }

    private void Start()
    {
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        // Não spawnar novas peças se o jogo já estiver em Game Over
        if (isGameOver)
            return;

        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = tetrominoes[random];

        activePiece.Initialize(this, spawnPosition, data);

        if (IsValidPosition(activePiece, spawnPosition))
        {
            Set(activePiece);
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        // Marca estado de Game Over para bloquear spawn/inputs
        isGameOver = true;

        // NÃO limpar o tilemap aqui — queremos manter a visualização e a pontuação
        // tilemap.ClearAllTiles(); <-- removido intencionalmente

        // Mostrar o UI de Game Over (pausa o jogo)
        if (GameOverManager.Instance != null)
            GameOverManager.Instance.ShowGameOver();
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, piece.data.tile);
        }

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.RecalculateScore(tilemap);
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition))
                return false;

            if (tilemap.HasTile(tilePosition))
                return false;
        }

        return true;
    }
}
