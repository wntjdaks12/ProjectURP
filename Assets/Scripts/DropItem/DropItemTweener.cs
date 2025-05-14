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
        // Ω¶¿Ã≈© ∆Æ¿©Ω∫
        shakeSequence = DOTween.Sequence()
            .SetAutoKill(false)
            .Pause()
            .Append(transform.DOShakeRotation(
                    duration: 0.5f,  // Ω¶¿Ã≈© Ω√∞£
                    strength: new Vector3(40, 0, 40), // Ω¶¿Ã≈© »∏¿¸
                    vibrato: 30, // Ω¶¿Ã≈© »Ωºˆ
                    randomness: 90, // ∑£¥˝
                    fadeOut: true // Ω¶¿Ã≈© ∆‰¿ÃµÂ æ∆øÙ
             ));
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(-360f, 361f), 0);

        // Ω¶¿Ã≈© ∆Æ¿©Ω∫ ¿Áª˝
        shakeSequence.Restart();
    }
}
