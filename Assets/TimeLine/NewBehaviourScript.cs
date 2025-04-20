using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private string oriLayerName;

    public void Test1()
    {
        var heroObj = PlayerManager.Instance.PlayerViewModel.HeroObject;

        oriLayerName = LayerMask.LayerToName(heroObj.gameObject.layer);

        heroObj.gameObject.ChangeLayersRecursively("UIObject");
    }

    public void Test2()
    {
        PlayerManager.Instance.PlayerViewModel.HeroObject.gameObject.ChangeLayersRecursively(oriLayerName);
    }
}
