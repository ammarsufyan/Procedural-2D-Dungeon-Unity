using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungeonGenerator generator;

    // Initialize
    private void Awake() 
    {
        // access the target of the custom inspector
        generator = (AbstractDungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        // Display all fields from class
        base.OnInspectorGUI();

        // When the user presses the button, generate a new dungeon
        if (GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }
}
