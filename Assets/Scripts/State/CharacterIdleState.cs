public class CharacterIdleState : ICharacterState
{
    public static CharacterIdleState Instance { get; private set; } = new CharacterIdleState();

    public void OnHit(CharacterObject characterObject)
    {
        characterObject.SetState(CharacterHitState.Instance);

        if (characterObject.animator != null) characterObject.animator.SetTrigger("OnHit");
    }

    public void OnIdle(CharacterObject characterObject)
    {
    }

    public void OnDeath(CharacterObject characterObject)
    {
        characterObject.SetState(CharacterDeathState.Instance);

        if (characterObject.animator != null) characterObject.animator.SetTrigger("OnDeath");
    }

    public void OnMove(CharacterObject characterObject)
    {
    }
}
