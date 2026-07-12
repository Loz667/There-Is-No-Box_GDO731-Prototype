using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleDie", menuName = "Puzzle Dice/Add Die", order = 1)]
public class DieDefinition : ScriptableObject, IRollable<DieFace>
{
    //Type?
    public List<DieFace> faces = new List<DieFace>();
    public Color dieColor;

    public DieFace GetRoll()
    {
        if (faces == null || faces.Count == 0) return null;
        int index = Random.Range(0, faces.Count);
        return faces[index];
    }
    
}
