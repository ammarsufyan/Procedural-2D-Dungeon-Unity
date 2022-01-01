using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;

    [SerializeField]
    private TileBase floorTile, wallTop;

    // IEnumerable is a generic interface that allows you to iterate over a collection of items
    public void PaintFloorTiles(IEnumerable<Vector2Int> floor_position) 
    {
        PaintTiles(floor_position, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile) 
    {
        foreach(var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position) 
    {
        // get the tile position in the tilemap
        // convert world position to cell position need to convert to vector3Int
        var tile_position = tilemap.WorldToCell((Vector3Int)position);
        // set the tile at the tile position
        tilemap.SetTile(tile_position, tile);
    }

    internal void PaintSingleBasicWall(Vector2Int position) 
    {
        PaintSingleTile(wallTilemap, wallTop, position);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}