public class GameEvent
{
    public int Value;
    public GameEventType Type;

    public GameEvent(GameEventType type, int value = 0)
    {
        Type = type;
        Value = value;
    }
}
