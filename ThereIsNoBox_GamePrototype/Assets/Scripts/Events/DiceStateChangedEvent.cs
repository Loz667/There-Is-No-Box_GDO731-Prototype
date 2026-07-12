public struct DiceStateChangedEvent : IEvent
{
    public DiceEnums.DieState NewState { get; private set; }

    public DiceStateChangedEvent(DiceEnums.DieState newState)
    {
        NewState = newState;
    }
}
