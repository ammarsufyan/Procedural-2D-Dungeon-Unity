using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : RandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;

    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    
    // prevent the floors to be connected
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;

    // checking if we want to use random walk to create a room
    // or generate a square room using bounding box
    [SerializeField]
    private bool randomWalkRooms = false;

    protected override void RunProceduralGeneration() 
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var room_list = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), 
                                                                            minRoomWidth, 
                                                                            minRoomHeight);
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(room_list); 

        List<Vector2Int> room_centers = new List<Vector2Int>();

        // add the each center of the room into the list
        foreach(var room in room_list)
        {
            // .center is Vector3Int, so we need to convert it to Vector2Int
            room_centers.Add(((Vector2Int)Vector3Int.RoundToInt(room.center)));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(room_centers);

        // spawn the floor tiles for those corridors
        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);                                                                
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> room_centers)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var current_room_center = room_centers[Random.Range(0, room_centers.Count)];
        // remove the selected room center from the list
        room_centers.Remove(current_room_center);
        
        // if the room center is greater than zero, then we need to connect it to the previous room
        while(room_centers.Count > 0)
        {
            Vector2Int closest_room_center = FindClosestPointTo(current_room_center, room_centers);
            room_centers.Remove(closest_room_center);
            HashSet<Vector2Int> newCorridor = CreateCorridor(current_room_center, closest_room_center);
            current_room_center = closest_room_center;
            corridors.UnionWith(newCorridor);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int current_room_center, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = current_room_center;
        corridor.Add(position);

        // iterate until y position == y destination
        // to move the position up and down
        while(position.y != destination.y)
        {   
            if(destination.y > position.y) 
            {
                position += Vector2Int.up;
            }
            else if(destination.y < position.y)
            {
                position += Vector2Int.down;
            }

            corridor.Add(position);
        }

        // same iterate but using x to right and left
        while(position.x != destination.x) 
        {
            if(destination.x > position.x)
            {
                position += Vector2Int.right;
            } 
            else if(destination.x < position.x) 
            {
                position += Vector2Int.left;
            }

            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int current_room_center, List<Vector2Int> room_centers)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;

        foreach(var position in room_centers)
        {
            float current_distance = Vector2.Distance(position, current_room_center);
            // check if the current distance is smaller than the previous distance
            // add to the closest point
            if(current_distance < distance) 
            {
                distance = current_distance;
                closest = position;
            }
        }

        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> room_list)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach(var room in room_list) 
        {
            for(int col = offset; col < room.size.x - offset; col++)
            {
                for(int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }

        return floor;
    }
}