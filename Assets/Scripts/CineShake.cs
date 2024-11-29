using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineShake : MonoBehaviour
{
    private CinemachineFreeLook cfl;
    public float shakeTimer;
    public LayerMask ball;
    private void Awake()
    {
        cfl = GetComponent<CinemachineFreeLook>();
    }

    public void ShakeCam(float Intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin CMBPN1 = cfl.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CinemachineBasicMultiChannelPerlin CMBPN2 = cfl.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CinemachineBasicMultiChannelPerlin CMBPN3 = cfl.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CMBPN1.m_AmplitudeGain = Intensity;
        CMBPN2.m_AmplitudeGain = Intensity;
        CMBPN3.m_AmplitudeGain = Intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin CMBPN1 = cfl.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                CinemachineBasicMultiChannelPerlin CMBPN2 = cfl.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                CinemachineBasicMultiChannelPerlin CMBPN3 = cfl.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                CMBPN1.m_AmplitudeGain = 0f;
                CMBPN2.m_AmplitudeGain = 0f;
                CMBPN3.m_AmplitudeGain = 0f;
            }
        }
    }
}
