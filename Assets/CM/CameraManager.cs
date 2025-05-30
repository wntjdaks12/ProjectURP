using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public enum CameraTypes
    {
        Default,
        CharacterInfoMenu,
        ChapterClear,
    }

    [field: SerializeField] public CinemachineVirtualCameraBase DefaultCam { get; private set; }
    [field: SerializeField] public CinemachineVirtualCameraBase CharacterInfoMenuCam { get; private set; }
    [field: SerializeField] public CinemachineVirtualCameraBase ChapterClearuCam { get; private set; }

    public void SwitchCamera(CameraTypes type, Transform follow = null, Transform lookAt = null)
    {
        DefaultCam.Priority = 0;
        CharacterInfoMenuCam.Priority = 0;
        ChapterClearuCam.Priority = 0;

        switch (type)
        {
            case CameraTypes.Default:
                DefaultCam.Priority = 10;
                DefaultCam.Follow = follow;
                DefaultCam.LookAt = lookAt;
                break;
            case CameraTypes.CharacterInfoMenu:
                CharacterInfoMenuCam.Priority = 10;
                CharacterInfoMenuCam.Follow = follow;
                CharacterInfoMenuCam.LookAt = lookAt;
                break;
            case CameraTypes.ChapterClear:
                ChapterClearuCam.Priority = 10;
                ChapterClearuCam.Follow = follow;
                ChapterClearuCam.LookAt = lookAt;
                break;
        }
    }

    public void SwitchChapterClearuCam()
    {
        var playerViewModel = PlayerManager.Instance.PlayerViewModel;

        if (playerViewModel != null && playerViewModel.HeroObject != null)
        {
            SwitchCamera(CameraTypes.ChapterClear, playerViewModel.HeroObject.transform, playerViewModel.HeroObject.transform);
        }
    }
}
