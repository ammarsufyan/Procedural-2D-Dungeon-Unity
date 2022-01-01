using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to store the parameters for the random walk algorithm
// CreateAssetMenu is used to create a menu in the Assets menu
// PCG = Procedural Content Generation
[CreateAssetMenu(fileName = "RandomWalkParameters_", menuName = "PCG/RandomWalkData")]
public class RandomWalkData : ScriptableObject
{
    public int iterations = 10;
    public int walkLength = 10;
    public bool startRandomlyIteration = true;
}
