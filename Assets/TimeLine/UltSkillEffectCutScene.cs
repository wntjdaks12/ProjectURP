using UnityEngine;

public class UltSkillEffectCutScene : MonoBehaviour
{
    private string oriLayerName;


    public void SignelStart()
    {
        var heroObj = PlayerManager.Instance.PlayerViewModel.HeroObject;

        oriLayerName = LayerMask.LayerToName(heroObj.gameObject.layer);

        heroObj.gameObject.ChangeLayersRecursively("UIObject");
    }

    public void SignelEnd()
    {
        PlayerManager.Instance.PlayerViewModel.HeroObject.gameObject.ChangeLayersRecursively(oriLayerName);
    }
}
