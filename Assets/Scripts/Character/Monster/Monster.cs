using UnityEngine;

public class Monster : Character
{
    public int Exp { get; set; }

    public override void Init(Transform transform = null)
    {
        base.Init(transform);

        OnDeathEvent += () =>
        {
            PlayerManager.Instance.PlayerViewModel.Exp(Exp);
        };
    }
}
