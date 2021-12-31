using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int start_position = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10;

    [SerializeField]
    public int walk_length = 10;

    [SerializeField]
    public bool start_randomly_iteration = true;

    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floor_position = RunRandomWalk();

        foreach(var position in floor_position) {
            Debug.Log(position);
        }
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var current_position = start_position;
        HashSet<Vector2Int> floor_position = new HashSet<Vector2Int>();
        for(int i = 0; i < iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.RandomWalk(current_position, walk_length);
            floor_position.UnionWith(path);
            if(start_randomly_iteration)
            {
                current_position = floor_position.ElementAt(Random.Range(0, floor_position.Count));
            }
        }

        return floor_position;
    }
}