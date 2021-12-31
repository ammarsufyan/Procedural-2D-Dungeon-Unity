using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> RandomWalk(Vector2Int start_position, int walk_length) 
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(start_position);
        var previous_position = start_position;

        // Randomly walk the path
        for (int i = 0; i < walk_length; i++)
        {
            var next_position = previous_position + Directions2D.GetRandomDirections();
            path.Add(next_position);
            previous_position = next_position;
        }

        return path;
    }
}