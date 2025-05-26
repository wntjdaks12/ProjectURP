using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private RectTransform joystick;
    [SerializeField] private RectTransform joystickHandle;

    private bool isDragging;

    public Vector3 InputDirection { get; private set; }

    private void Awake()
    {
        // 예외 처리
        Debug.Assert(joystick != null, "조이스틱 연결 x");
        Debug.Assert(joystickHandle != null, "조이스틱 핸들 연결 x");
    }

    private void Start()
    {
        // 조이스틱 비활성화
        if (joystick.gameObject.activeSelf) joystick.gameObject.SetActive(false);
    }

    private void Update()
    {
        // 플랫폼에 따른 전처리기
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleKeyInput();
#elif UNITY_IOS || UNITY_ANDROID
        HandleTouchInput();
#endif
    }

    #region 키 입력 처리
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
    #region 터치 입력 처리
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

                // 첫 터치 시 조이스틱 활성화
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

                // 조이스틱 핸들 범위 제한
                joystickHandle.localPosition = dirVec3.magnitude < joystickHandle.sizeDelta.x ? dirVec3 : dirVec3.normalized * joystickHandle.sizeDelta.x;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                InputDirection = Vector3.zero;

                // 터치 끝날 시 조이스틱 비활성화
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
