public class CharacterDeathState : ICharacterState
{
    public static CharacterDeathState Instance { get; private set; } = new CharacterDeathState();

    public void OnDeath(CharacterObject characterObject)
    {
    }

    public void OnHit(CharacterObject characterObject)
    {
    }

    public void OnIdle(CharacterObject characterObject)
    {
    }

    public void OnMove(CharacterObject characterObject)
    {
    }
}
