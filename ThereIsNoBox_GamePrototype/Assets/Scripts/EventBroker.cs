

public static class EventBroker<T> where T : IEvent
{
    public delegate void Event(T args);
    public static event Event OnEvent;
    public static void Broadcast(T evt) => OnEvent?.Invoke(evt);
}
