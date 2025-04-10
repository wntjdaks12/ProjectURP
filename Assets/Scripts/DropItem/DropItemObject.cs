using UnityEngine;

public class DropItemObject : EntityObject
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
