using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkDungeonGenerator : MonoBehaviour
{
    // Serialize Field is used to make the variable visible in the inspector
    [SerializeField]
    protected Vector2Int start_position = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10;

    [SerializeField]
    public int walk_length = 10;

    [SerializeField]
    public bool start_randomly_iteration = true;

    [SerializeField]
    private TilemapVisualizer tilemapVisualizer;

    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floor_position = RunRandomWalk();
        // clear the tilemap
        tilemapVisualizer.Clear();
        // Paint the floor tiles
        tilemapVisualizer.PaintFloorTiles(floor_position);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var current_position = start_position;
        HashSet<Vector2Int> floor_position = new HashSet<Vector2Int>();
        for(int i = 0; i < iterations; i++)
        {
            // Start random walk and add to path
            var path = ProceduralGenerationAlgorithms.RandomWalk(current_position, walk_length);
            
            // Add the path to the floor position and unique the list
            floor_position.UnionWith(path);

            // start randomly iteration
            if(start_randomly_iteration)
            {
                // Make sure the start position is not in the path
                current_position = floor_position.ElementAt(Random.Range(0, floor_position.Count));
            }
        }

        return floor_position;
    }
}