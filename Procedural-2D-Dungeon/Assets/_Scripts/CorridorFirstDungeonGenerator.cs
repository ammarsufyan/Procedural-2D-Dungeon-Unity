using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : RandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;

    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floor_position = new HashSet<Vector2Int>();

        // create corridors
        HashSet<Vector2Int> potential_room_position = new HashSet<Vector2Int>();      
        CreateCorridors(floor_position, potential_room_position);    
        
        // create rooms
        HashSet<Vector2Int> room_position = CreateRooms(potential_room_position);
        
        // find dead end position
        List<Vector2Int> dead_end_position = FindAllDeadEndPosition(floor_position);
        
        // create rooms at dead end
        CreateRoomsAtDeadEnd(dead_end_position, room_position);
        
        floor_position.UnionWith(room_position);

        tilemapVisualizer.PaintFloorTiles(floor_position);
        WallGenerator.CreateWalls(floor_position, tilemapVisualizer);
    }

    private void CreateCorridors(HashSet<Vector2Int> floor_position, HashSet<Vector2Int> potential_room_position)
    {
        var current_position = startPosition;
        potential_room_position.Add(current_position);

        for(int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(current_position, corridorLength);
            floor_position.UnionWith(corridor);
            
            // to make sure the corridor is connected             
            current_position = corridor[corridor.Count - 1];

            // add the current corridor position to the potential room position
            potential_room_position.Add(current_position);
        }
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> dead_ends, HashSet<Vector2Int> room_floors)
    {
        foreach(var position in dead_ends)
        {
            if(room_floors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                room_floors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEndPosition(HashSet<Vector2Int> floor_position)
    {
        List<Vector2Int> dead_ends = new List<Vector2Int>();
        foreach(var position in floor_position)
        {
            int neighbour_count = 0;

            // check if the position is a dead end
            foreach(var direction in Directions2D.GetDirections)
            {
                if(floor_position.Contains(position + direction))
                {
                    neighbour_count++;
                }
            }

            // when the neighbour count is 1, it is a dead end
            if(neighbour_count == 1)
            {
                dead_ends.Add(position);
            }
        }

        return dead_ends;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potential_room_position)
    {
        HashSet<Vector2Int> room_positions = new HashSet<Vector2Int>();
        
        // calculate the room size
        int room_count = Mathf.RoundToInt(potential_room_position.Count * roomPercent);

        // GUID (aka UUID) is an acronym for 'Globally Unique Identifier'
        // using list for querying and using GUID to generate unique id for each room 
        List<Vector2Int> rooms_to_create = potential_room_position.OrderBy(x => Guid.NewGuid()).Take(room_count).ToList();

        foreach(var position in rooms_to_create)
        {
            // generate the room using the random room position
            var room_floor = RunRandomWalk(randomWalkParameters, position);
            room_positions.UnionWith(room_floor);
        }

        return room_positions;
    }
}