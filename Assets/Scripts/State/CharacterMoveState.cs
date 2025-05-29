public class CharacterMoveState : ICharacterState
{
    public static CharacterMoveState Instance { get; private set; } = new CharacterMoveState();

    public void OnHit(CharacterObject characterObject)
    {
        characterObject.SetState(CharacterHitState.Instance);

        if (characterObject.animator != null) characterObject.animator.SetTrigger("OnHit");
    }

    public void OnIdle(CharacterObject characterObject)
    {
        characterObject.SetState(CharacterIdleState.Instance);

        if (characterObject.animator != null) characterObject.animator.SetBool("IsMove", false);
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
