using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Directions2D 
{
    public static List<Vector2Int> GetDirections = new List<Vector2Int>()
    {
        new Vector2Int(0, 1), // Up
        new Vector2Int(1, 0), // Right
        new Vector2Int(0, -1), // Down
        new Vector2Int(-1, 0) // Left
    };

    public static Vector2Int GetRandomDirections() 
    {
        return GetDirections[Random.Range(0, GetDirections.Count)];
    }
}