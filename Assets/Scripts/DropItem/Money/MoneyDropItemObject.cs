using DG.Tweening;
using UnityEngine;

public class MoneyDropItemObject : DropItemObject
{
    private Sequence shakeSequence;

    private void Awake()
    {
        // ����ũ Ʈ����
        shakeSequence = DOTween.Sequence()
            .SetAutoKill(false)
            .Pause()
            .Append(transform.DOShakeRotation(
                    duration: 0.5f,  // ����ũ �ð�
                    strength: new Vector3(0, 0, 15), // ����ũ ȸ��
                    vibrato: 10, // ����ũ Ƚ��
                    randomness: 90, // ����
                    fadeOut: true // ����ũ ���̵� �ƿ�
                )).OnComplete(() =>
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                });
    }

    private void OnEnable()
    {
        // ����ũ Ʈ���� ���
        shakeSequence.Restart();
    }
}
