using System;

public static class GameEvents
{
    public static event Action<GameEvent> OnGameEvent;

    public static void Raise(GameEvent gameEvent)
    {
        OnGameEvent?.Invoke(gameEvent);
    }
}
