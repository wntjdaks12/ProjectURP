public interface ICharacterState
{
    public void OnIdle(CharacterObject characterObject);
    public void OnMove(CharacterObject characterObject);
    public void OnDeath(CharacterObject characterObject);
    public void OnHit(CharacterObject characterObject);
}
