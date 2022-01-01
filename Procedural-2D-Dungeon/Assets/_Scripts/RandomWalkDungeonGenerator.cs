using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    // Serialize Field is used to make the variable visible and editable in the inspector
    [SerializeField]
    protected RandomWalkData randomWalkParameters;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floor_position = RunRandomWalk(randomWalkParameters, startPosition);
        
        // print the floor position
        Debug.Log("Iterasi-" + randomWalkParameters.iterations);
        foreach(var position in floor_position) 
        {
            Debug.Log(position);
        }

        // print total generate tiles
        Debug.Log("Total Tiles: " + floor_position.Count);


        // clear the tilemap
        tilemapVisualizer.Clear();
        // Paint the floor tiles
        tilemapVisualizer.PaintFloorTiles(floor_position);
        // Add the wall
        WallGenerator.CreateWalls(floor_position, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(RandomWalkData parameters, Vector2Int position)
    {
        var current_position = position;
        HashSet<Vector2Int> floor_position = new HashSet<Vector2Int>();
        for(int i = 0; i < parameters.iterations; i++)
        {
            // Start random walk and add to path
            var path = ProceduralGenerationAlgorithms.RandomWalk(current_position, parameters.walkLength);
            
            // Add the path to the floor position and unique the list
            floor_position.UnionWith(path);

            // start randomly iteration
            if(parameters.startRandomlyIteration)
            {
                // Make sure the start position is not in the path
                current_position = floor_position.ElementAt(Random.Range(0, floor_position.Count));
            }
        }

        return floor_position;
    }
}