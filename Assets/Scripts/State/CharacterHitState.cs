public class CharacterHitState : ICharacterState
{
    public static CharacterHitState Instance { get; private set; } = new CharacterHitState();

    public void OnDeath(CharacterObject characterObject)
    {
        characterObject.SetState(CharacterDeathState.Instance);

        if (characterObject.animator != null) characterObject.animator.SetTrigger("OnDeath");
    }

    public void OnHit(CharacterObject characterObject)
    {
    }

    public void OnIdle(CharacterObject characterObject)
    {
        characterObject.SetState(CharacterIdleState.Instance);

        if (characterObject.animator != null) characterObject.animator.SetBool("IsMove", false);
    }

    public void OnMove(CharacterObject characterObject)
    {
        characterObject.SetState(CharacterMoveState.Instance);

        if (characterObject.animator != null) characterObject.animator.SetBool("IsMove", true);
    }
}
