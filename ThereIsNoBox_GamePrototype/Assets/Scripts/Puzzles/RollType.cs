using UnityEngine;

[CreateAssetMenu(fileName = "RollType_", menuName = "Puzzle Dice/Add Roll Type", order = 0)]
public class RollType : ScriptableObject
{
    public DiceEnums.RollType rollType;
    public Sprite icon;
}
