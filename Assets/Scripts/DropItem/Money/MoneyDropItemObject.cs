using DG.Tweening;
using UnityEngine;

public class MoneyDropItemObject : DropItemObject
{
    private Sequence shakeSequence;

    private void Awake()
    {
        // ½¦ÀÌÅ© Æ®À©½º
        shakeSequence = DOTween.Sequence()
            .SetAutoKill(false)
            .Pause()
            .Append(transform.DOShakeRotation(
                    duration: 0.5f,  // ½¦ÀÌÅ© ½Ã°£
                    strength: new Vector3(0, 0, 15), // ½¦ÀÌÅ© È¸Àü
                    vibrato: 10, // ½¦ÀÌÅ© È½¼ö
                    randomness: 90, // ·£´ý
                    fadeOut: true // ½¦ÀÌÅ© ÆäÀÌµå ¾Æ¿ô
                )).OnComplete(() =>
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                });
    }

    private void OnEnable()
    {
        // ½¦ÀÌÅ© Æ®À©½º Àç»ý
        shakeSequence.Restart();
    }
}
