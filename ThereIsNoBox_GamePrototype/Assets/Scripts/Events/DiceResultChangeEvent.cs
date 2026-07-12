
public struct DiceResultChangeEvent : IEvent
{
    public DiceEnums.DieResult NewResult { get; private set; }

    public DiceResultChangeEvent(DiceEnums.DieResult newResult)
    {
        NewResult = newResult;
    }
    
}
