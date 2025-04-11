using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get => instance ??= FindAnyObjectByType<UIManager>(); }

    [field: SerializeField] public Canvas OverrayCanvas { get; private set; }
    [field: SerializeField] public Canvas DamageCannvas { get; private set; }
}
