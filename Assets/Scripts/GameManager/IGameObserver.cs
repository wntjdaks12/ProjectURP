public interface IGameObserver
{
    public void StartCombatNotify(); // ���� ���� �˸�
    public void CombatNotify(); // ���� �˸�
    public void IdleNotify(); // �����(���� �޽�) �˸�
    public void EndCombatNotify(); // ���� �� �˸�
}
