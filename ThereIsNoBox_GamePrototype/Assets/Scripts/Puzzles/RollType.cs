using UnityEngine;

[CreateAssetMenu(fileName = "RollType_", menuName = "Back In Your Box/Puzzle Dice/Add Roll Type", order = 0)]
public class RollType : ScriptableObject
{
    public DiceEnums.RollType rollType;
    public Sprite rollIcon;
    public Sprite targetIcon;
}
