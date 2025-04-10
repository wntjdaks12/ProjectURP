using System;
using System.Collections;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public event Action OnTriggerAttackEvent;
    public event Action OnTriggerSkillEvent;
    public event Action OnStartSkillEvent;
    public event Action OnExitSkillEvent;
    public event Action OnStartIdleEvent;
    public event Action OnExitIdleEvent;
    public event Action OnExitEquipEvent;
    public event Action OnExitJumpEvent;
    public event Action OnExitAttackEvent;

    public bool IsAttacking { get; private set; }
    public bool IsSkilled { get; private set; }
    public bool IsAiming { get; private set; }

    /// <summary>
    /// 공격 시작
    /// </summary>
    public void StartAttackAnimation()
    {
        IsAttacking = true;
    }

    public void TriggerAttack()
    {
        OnTriggerAttackEvent?.Invoke();
    }

    /// <summary>
    /// 공격 끝
    /// </summary>
    public void ExitAttackAnimation()
    {
        IsAttacking = false;

        OnExitAttackEvent?.Invoke();
    }

    /// <summary>
    /// 스킬 시작
    /// </summary>
    public void StartSkillAnimation()
    {
        IsSkilled = true;

        OnStartSkillEvent?.Invoke();
    }

    public void TriggerSkill()
    {
        OnTriggerSkillEvent?.Invoke();
    }

    /// <summary>
    /// 스킬 끝
    /// </summary>
    public void ExitSkillAnimation()
    {
        IsSkilled = false;

        OnExitSkillEvent?.Invoke();
    }

    /// <summary>
    /// 스탠딩 시작
    /// </summary>
    public void StartIdleAnimation()
    {
        OnStartIdleEvent?.Invoke();
    }

    /// <summary>
    /// 스탠딩 끝
    /// </summary>
    public void ExitIdleAnimation()
    {
        OnExitIdleEvent?.Invoke();
    }

    /// <summary>
    /// 점프 끝
    /// </summary>
    public void ExitJumpAnimation()
    {
        OnExitJumpEvent?.Invoke();
    }

    /// <summary>
    /// 장착 끝
    /// </summary>
    public void ExitEquipAnimation()
    {
        OnExitEquipEvent?.Invoke();
    }
}
