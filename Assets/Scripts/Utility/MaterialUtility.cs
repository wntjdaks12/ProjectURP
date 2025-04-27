using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 임시로 짠 머티리얼 유틸리티
// 최적화 코딩 x
// 로직 개편 필요
public class MaterialUtility
{
    private Dictionary<SkinnedMeshRenderer, Material[]> cachedSkinnedMeshRenderers;

    public MaterialUtility()
    {
        cachedSkinnedMeshRenderers = new Dictionary<SkinnedMeshRenderer, Material[]>();
    }

    public void Init(Transform transform)
    {
        if (transform.TryGetComponent(out SkinnedMeshRenderer skinnedMeshRenderer))
        {
            cachedSkinnedMeshRenderers.Add(skinnedMeshRenderer, skinnedMeshRenderer.materials);
        }

        foreach (Transform child in transform)
        {
            Init(child);
        }

    }

    public IEnumerator ChangeMaterial(float time)
    {
        ChangeHit();

        yield return new WaitForSeconds(time);

        ChangeHitOri();
    }

    private void ChangeHit()
    {
        foreach (var skinnedMeshRenderer in cachedSkinnedMeshRenderers)
        {
            foreach (var mat in skinnedMeshRenderer.Value)
            {
                mat.SetFloat("_Rim", 1);
            }
        }
    }

    private void ChangeHitOri()
    {
        foreach (var skinnedMeshRenderer in cachedSkinnedMeshRenderers)
        {
            foreach (var mat in skinnedMeshRenderer.Value)
            {
                mat.SetFloat("_Rim", 0);
            }
        }
    }
}
