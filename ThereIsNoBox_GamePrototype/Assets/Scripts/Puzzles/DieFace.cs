using UnityEngine;

[CreateAssetMenu(fileName = "DieFace_", menuName = "Puzzle Dice/Add Face", order = 0)]
public class DieFace : ScriptableObject
{
    public DiceEnums.DieResult resultType;

    public Sprite image;

    public string label;
}
