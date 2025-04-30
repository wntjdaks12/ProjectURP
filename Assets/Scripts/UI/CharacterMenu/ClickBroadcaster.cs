using UnityEngine;

public class ClickBroadcaster : MonoBehaviour
{
    private ClickSystem clickSystem;

    private enum LayerTypes { Player, Enemy }
    [SerializeField] private LayerTypes targetLayer;

    private void Awake()
    {
        clickSystem = FindObjectOfType<ClickSystem>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var clickedObj = hit.collider.gameObject;

                if (clickedObj.layer == LayerMask.NameToLayer(targetLayer.ToString()))
                {
                    if (clickSystem) clickSystem.OnClick(clickedObj);
                }
            }
        }
#endif
    }
}
