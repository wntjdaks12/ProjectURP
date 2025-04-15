using UnityEngine;
using UnityEngine.UI;

public class OverrayCanvas : MonoBehaviour
{
    public CanvasScaler CanvasScaler { get; private set; }

    public void Awake()
    {
        CanvasScaler = GetComponent<CanvasScaler>();
    }
}
