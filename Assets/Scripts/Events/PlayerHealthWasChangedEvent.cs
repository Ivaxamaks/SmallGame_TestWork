namespace Events
{
    public struct PlayerHealthWasChangedEvent
    {
        public int CurrentPlayerHealth { get; }

        public PlayerHealthWasChangedEvent(int currentPlayerHealth)
        {
            CurrentPlayerHealth = currentPlayerHealth;
        }
    }
}