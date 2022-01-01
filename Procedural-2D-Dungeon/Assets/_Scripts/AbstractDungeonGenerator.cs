using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract class is a class that cannot be instantiated
// only for base class, but never create an object for that class.  
public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon() 
    {
        // clear the tilemap
        tilemapVisualizer.Clear();
        // run procedural generation
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}