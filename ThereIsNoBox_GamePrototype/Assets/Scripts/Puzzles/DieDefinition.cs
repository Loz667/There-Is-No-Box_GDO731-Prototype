using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleDie", menuName = "Puzzle Dice/Add Die", order = 1)]
public class DieDefinition : ScriptableObject, IRollable<DieFace>
{
    [SerializeField] private DieFace[] faces;
    public DieFace[] Faces => faces;

    public DieFace GetRoll()
    {
        return faces[Random.Range(0, faces.Length)];
    }
    
    
}
