using System;

public class Data : IData
{
    public event Action<IData> OnDataRemove;

    public int Id { get; set; }
    public int InstanceId { get; set; }
    public int ServerId { get; set; }

    public IDataTable TableModel { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public void OnRemoveData()
    {
        OnDataRemove?.Invoke(this);
    }

    //위치 수정해야됨 임시로 넣음
    public enum TargetTypes
    {
        Self = 0, // 자기 자신
        Ally = 1, // 팀
        Enemy = 2, // 적
        Both = 3, // 팀 & 적군
    }
}
