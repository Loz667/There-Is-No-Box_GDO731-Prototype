using UnityEngine;

public class DiceEnums 
{
    public enum DieResult
    {
        SCREW1,
        SCREW2,
        SCREW3,
        SPANNER,
        HAMMER,
        CROWBAR,
        HIT,
        CRIT,
        MISS,
        BLOCK,
        SUCCESS,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT
    }

    public enum DieType
    {
        Engineering,
        Chemical,
        Combat,
    }

    public enum DieState
    {
        Rolling,
        Locked,
        Used,
        Lost
    }
    
}
