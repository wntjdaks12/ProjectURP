public interface IGameObserver
{
    public void CombatNotify();
    public void IdleNotify();
    public void EndCombatNotify();
}
