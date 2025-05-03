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

    public static void ChangeTagsRecursively(this GameObject gameObject, string name)
    {
        gameObject.tag = name;
        foreach (Transform child in gameObject.transform)
        {
            ChangeTagsRecursively(child.gameObject, name);
        }
    }
}
