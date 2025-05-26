using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private RectTransform joystick;
    [SerializeField] private RectTransform joystickHandle;

    private bool isDragging;

    public Vector3 InputDirection { get; private set; }

    private void Awake()
    {
        // ���� ó��
        Debug.Assert(joystick != null, "���̽�ƽ ���� x");
        Debug.Assert(joystickHandle != null, "���̽�ƽ �ڵ� ���� x");
    }

    private void Start()
    {
        // ���̽�ƽ ��Ȱ��ȭ
        if (joystick.gameObject.activeSelf) joystick.gameObject.SetActive(false);
    }

    private void Update()
    {
        // �÷����� ���� ��ó����
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleKeyInput();
#elif UNITY_IOS || UNITY_ANDROID
        HandleTouchInput();
#endif
    }

    #region Ű �Է� ó��
    private void HandleKeyInput()
    {
        var totalDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            totalDirection += transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            totalDirection += transform.forward * -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            totalDirection += transform.right * -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            totalDirection += transform.right;
        }

        InputDirection = totalDirection;
    }
    #endregion
    #region ��ġ �Է� ó��
    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (Input.touchCount == 2)
                {
                    isDragging = false;
                    InputDirection = Vector3.zero;

                    if (joystick.gameObject.activeSelf)
                    {
                        joystick.gameObject.SetActive(false);
                    }

                    return;
                }

                InputDirection = ((Vector3)touch.position - joystick.position).normalized;

                // ù ��ġ �� ���̽�ƽ Ȱ��ȭ
                if (!joystick.gameObject.activeSelf)
                {
                    joystick.gameObject.SetActive(true);

                    joystick.position = touch.position;
                }

                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                var touchPos = (Vector3)touch.position;
                var dirVec3 = (touchPos - joystick.position);

                InputDirection = (touchPos - joystick.position).normalized;

                // ���̽�ƽ �ڵ� ���� ����
                joystickHandle.localPosition = dirVec3.magnitude < joystickHandle.sizeDelta.x ? dirVec3 : dirVec3.normalized * joystickHandle.sizeDelta.x;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                InputDirection = Vector3.zero;

                // ��ġ ���� �� ���̽�ƽ ��Ȱ��ȭ
                if (joystick.gameObject.activeSelf)
                {
                    joystick.gameObject.SetActive(false);
                }

                isDragging = false;
            }
        }
    }
    #endregion
}
