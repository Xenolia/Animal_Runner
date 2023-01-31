using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager current;

    public CinemachineVirtualCamera[] cameras;

    private CinemachineVirtualCamera currentCam;

    [SerializeField]private CinemachineBrain cinemachineBrain;
    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }
    private void Start()
    {
        currentCam =cameras[0];
        currentCam.gameObject.SetActive(true);
    }

    public void ChangeCamera(CinemachineVirtualCamera newCam)
    {
        currentCam.gameObject.SetActive(false);
        currentCam = newCam;
        currentCam.gameObject.SetActive(true);
    }

    public void SetCameraChangeSpeed(float camSpeed)
    {
        cinemachineBrain.m_DefaultBlend.m_Time = camSpeed;
    }

}
