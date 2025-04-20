using UnityEngine;

public static class GameObjectExtension
{
    public static void ChangeLayersRecursively(this GameObject gameObject, string name)
    {
        gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in gameObject.transform)
        {
            ChangeLayersRecursively(child.gameObject, name);
        }
    }
}
