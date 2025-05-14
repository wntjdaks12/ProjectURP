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
        // 쉐이크 트윈스
        shakeSequence = DOTween.Sequence()
            .SetAutoKill(false)
            .Pause()
            .Append(transform.DOShakeRotation(
                    duration: 0.5f,  // 쉐이크 시간
                    strength: new Vector3(40, 0, 40), // 쉐이크 회전
                    vibrato: 30, // 쉐이크 횟수
                    randomness: 90, // 랜덤
                    fadeOut: true // 쉐이크 페이드 아웃
             )).Join(transform.DOLocalMove(new Vector3(0, 0, 0), 0.25f)); // 위에서 아래로 떨어지는 트윈스 조인
    }

    private void OnEnable()
    {
        transform.localPosition = new Vector3(0, 10, 0);
        transform.rotation = Quaternion.Euler(0, Random.Range(-360f, 361f), 0);

        // 쉐이크 트윈스 재생
        shakeSequence.Restart();
    }
}
