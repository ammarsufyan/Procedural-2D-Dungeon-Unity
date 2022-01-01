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

    // use list (because ordered) to access the last position of the path
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int start_position, int corridor_length)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Directions2D.GetRandomDirections();
        var current_position = start_position;
        corridor.Add(current_position);

        for (int i = 0; i < corridor_length; i++)
        {
            current_position = current_position + direction;
            corridor.Add(current_position);
        }

        return corridor;
    }
}