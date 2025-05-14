using DG.Tweening;
using UnityEngine;

public class DropItemTweener : MonoBehaviour
{
    private enum TweenerTypes{ Shake }

    [SerializeField] private TweenerTypes tweenerType;

    private Sequence shakeSequence;

    private Quaternion oriRot;

    private void Awake()
    {
        // ����ũ Ʈ����
        shakeSequence = DOTween.Sequence()
            .SetAutoKill(false)
            .Pause()
            .Append(transform.DOShakeRotation(
                    duration: 0.5f,  // ����ũ �ð�
                    strength: new Vector3(40, 0, 40), // ����ũ ȸ��
                    vibrato: 30, // ����ũ Ƚ��
                    randomness: 90, // ����
                    fadeOut: true // ����ũ ���̵� �ƿ�
             ));
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(-360f, 361f), 0);

        // ����ũ Ʈ���� ���
        shakeSequence.Restart();
    }
}
