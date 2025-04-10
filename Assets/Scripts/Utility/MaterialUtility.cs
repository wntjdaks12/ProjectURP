using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ӽ÷� § ��Ƽ���� ��ƿ��Ƽ
// ����ȭ �ڵ� x
// ���� ���� �ʿ�
public class MaterialUtility
{
    public enum MaterialType { Hit }

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

    private Material getMaterial(MaterialType type)
    {
        switch (type)
        {
            case MaterialType.Hit: return Resources.Load<Material>("New Material");
        }

        return null;
    }

    public IEnumerator ChangeMaterial(MaterialType type, float time)
    {
        foreach (var skinnedMeshRenderer in cachedSkinnedMeshRenderers)
        {
            var count = skinnedMeshRenderer.Key.materials.Length;
            var m = new Material[count];
            for (int i = 0; i < m.Length; i++)
            {
                m[i] = getMaterial(type);
            }
            skinnedMeshRenderer.Key.materials = m;
        }

        yield return new WaitForSeconds(time);

        foreach (var skinnedMeshRenderer in cachedSkinnedMeshRenderers)
        {
            skinnedMeshRenderer.Key.materials = skinnedMeshRenderer.Value;
        }
    }
}
