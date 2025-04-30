using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [field: SerializeField] public CinemachineVirtualCameraBase NormaalCam { get; private set; }
    [field: SerializeField] public CinemachineVirtualCameraBase CharacterInfoMenuCam { get; private set; }

    private static CameraManager instance;
    public static CameraManager Instance { get => instance ??= FindObjectOfType<CameraManager>(); }

    public void ActiveNormalCam()
    {
        if (NormaalCam)
        {
            NormaalCam.Priority = 20;
        }

        if (CharacterInfoMenuCam)
        {
            CharacterInfoMenuCam.Priority = 10;
            CharacterInfoMenuCam.LookAt = null;
        }
    }   

    public void ActiveChaaracterInfoMenuCam(GameObject target)
    {
        if (NormaalCam)
        {
            NormaalCam.Priority = 10;
        }

        if (CharacterInfoMenuCam)
        {
            CharacterInfoMenuCam.Priority = 20;
            CharacterInfoMenuCam.LookAt = target.transform;
        }
    }
}
