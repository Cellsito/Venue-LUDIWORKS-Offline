using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    // Use um nome que não conflite com o tipo TileBase
    public TileBase tileAsset;

    public Board mainBoard;
    public Piece trackingPiece;

    public Tilemap tilemap { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        cells = new Vector3Int[4];
    }

    private void LateUpdate()
    {
        if (mainBoard == null || trackingPiece == null || tilemap == null)
            return;

        Clear();
        Copy();
        Drop();
        Set();
    }

    private void Clear()
    {
        if (tilemap == null || cells == null) return;

        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    private void Copy()
    {
        if (trackingPiece == null || cells == null) return;

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = trackingPiece.cells[i];
        }
    }

    private void Drop()
    {
        if (trackingPiece == null || mainBoard == null) return;

        Vector3Int localPosition = trackingPiece.position;

        int current = localPosition.y;
        int bottom = -mainBoard.boardSize.y / 2 - 1;

        // Temporariamente limpar a peça real do tabuleiro para checar colisões
        mainBoard.Clear(trackingPiece);

        for (int row = current; row >= bottom; row--)
        {
            localPosition.y = row;

            if (mainBoard.IsValidPosition(trackingPiece, localPosition))
            {
                this.position = localPosition;
            }
            else
            {
                break;
            }
        }

        // Recoloca a peça real no tabuleiro
        mainBoard.Set(trackingPiece);
    }

    private void Set()
    {
        if (tilemap == null || tileAsset == null || cells == null) return;

        RectInt bounds = mainBoard != null ? mainBoard.Bounds : new RectInt(new Vector2Int(-10 / 2, -20 / 2), new Vector2Int(10, 20));

        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;

            // evita tentar setar tiles fora do board (só por segurança)
            if (mainBoard != null && !bounds.Contains((Vector2Int)tilePosition))
                continue;

            // Aqui usamos a variável de instância `tileAsset` — NUNCA o tipo TileBase
            tilemap.SetTile(tilePosition, tileAsset);
        }
    }
}
