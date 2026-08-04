using UnityEngine;

public class DiceEnums 
{
    public enum DieResult
    {
        EMPTY,
        BLANK,
        SCREWDRIVER,
        GEAR1,
        GEAR2,
        GEAR3,
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

    public enum RollType
    {
        NONE,
        GEAR,
        SCREWDRIVER,
        SPANNER,
        HAMMER,
        CROWBAR,
        HIT,
        CRIT,
        MISS,
        BLOCK,
        SUCCESS,
        NUMBER
    }

    public enum DieType
    {
        Engineering,
        Chemical,
        Combat,
    }

    public enum DieState
    {
        Added,
        Rolling,
        Lost,
        
        Used,
        XCommitted,
        Focused,
        Locked
    }
    
}
