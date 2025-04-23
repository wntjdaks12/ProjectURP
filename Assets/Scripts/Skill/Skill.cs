using UnityEngine;

public class Skill : Entity
{
    public int VFXId { get; set; }
    public int HitVFXId { get; set; }

    public TargetInfo.TargetType TargetType { get; set; }
}
