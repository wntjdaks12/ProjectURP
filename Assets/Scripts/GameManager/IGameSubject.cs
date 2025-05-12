public interface IGameSubject
{
    public void Register(IGameObserver observer);
    public void Remove(IGameObserver observer);

    public void IdleNotify();
    public void CombatNotify();
    public void EndCombatNotify();
}
