public interface IGameObserver
{
    public void StartCombatNotify(); // 전투 시작 알림
    public void CombatNotify(); // 전투 알림
    public void IdleNotify(); // 무방비(전투 휴식) 알림
    public void EndCombatNotify(); // 전투 끝 알림
}
