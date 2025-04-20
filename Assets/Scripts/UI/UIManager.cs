using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get => instance ??= FindAnyObjectByType<UIManager>(); }

    [field: SerializeField] public OverrayCanvas OverrayCanvas { get; private set; }
    [field: SerializeField] public Transform DamagePopupPanel { get; private set; }
    [field: SerializeField] public Transform MiniHUDPanel { get; private set; }
}
