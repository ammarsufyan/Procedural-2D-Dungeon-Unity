using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    // <BoundsInt> is truct in UnityEngine (UnityEngine.CoreModule)
    // Represents an axis aligned bounding box with all values as integers.
    // BinarySpacePartitioning is use to split the room
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt space_to_split, int min_width, int min_height)
    {
        // queue is data structure that implement FIFO (First In First Out)
        Queue<BoundsInt> rooms_queue = new Queue<BoundsInt>();
        List<BoundsInt> room_list = new List<BoundsInt>();
        rooms_queue.Enqueue(space_to_split);
        
        // if have rooms to split then do BSP
        while(rooms_queue.Count > 0)
        {
            var room = rooms_queue.Dequeue();
            if(room.size.x >= min_width && room.size.y >= min_height)
            {
                // random value generate value from 0 to 1
                if(UnityEngine.Random.value < 0.5f) 
                {
                    // split horizontally if the room is bigger than min_height
                    if(room.size.y >= min_height * 2) 
                    {
                        SplitHorizontally(min_height, rooms_queue, room);
                    }
                    // split vertically if the room is bigger than min_width
                    else if(room.size.x >= min_width * 2) 
                    {
                        SplitVertically(min_width, rooms_queue, room);
                    }
                    // add a room if the area cannot be split
                    else if(room.size.x >= min_width && room.size.y >= min_height)
                    {
                        room_list.Add(room);
                    }
                }
                // change the direction of the split to vertically first to make it unique
                else
                {
                    // split vertically if the room is bigger than min_width
                    if(room.size.x >= min_width * 2) 
                    {
                        SplitVertically(min_width, rooms_queue, room);
                    }
                    // split horizontally if the room is bigger than min_height
                    else if(room.size.y >= min_height * 2) 
                    {
                        SplitHorizontally(min_height, rooms_queue, room);
                    }
                    // add a room if the area cannot be split
                    else if(room.size.x >= min_width && room.size.y >= min_height)
                    {
                        room_list.Add(room);
                    }
                }
            }
        }
        
        return room_list;
    }

    private static void SplitVertically(int min_width, Queue<BoundsInt> rooms_queue, BoundsInt room)
    {
        // range doesn't include the max value so room.size.x is minus 1
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), 
                          new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

        rooms_queue.Enqueue(room1);
        rooms_queue.Enqueue(room2);
    }

    private static void SplitHorizontally(int min_height, Queue<BoundsInt> rooms_queue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y); 
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), 
                          new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

        rooms_queue.Enqueue(room1);
        rooms_queue.Enqueue(room2);
    }
}