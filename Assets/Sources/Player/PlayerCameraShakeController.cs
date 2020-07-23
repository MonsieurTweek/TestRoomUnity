using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraShakeController : MonoBehaviour
{
    [Header("References")]
    public CinemachineBrain cinemachineBrain = null;

    [Header("Properties")]
    public float amplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    public float frequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private float _elapsedTime = 0f;
    private bool _isShaking = false;

    // Cinemachine Shake
    private CinemachineFreeLook _camera = null;

    // Update is called once per frame
    public void Shake(float duration)
    {
        _camera = (CinemachineFreeLook)cinemachineBrain.ActiveVirtualCamera;

        _elapsedTime = duration;
        _isShaking = true;

        // Rumble 
        // the low-frequency (left) motor at 1/4 speed 
        // the high-frequency (right) motor at 3/4 speed
        InputManager.instance.SetVibration(0.25f, 0.75f);
    }

    private void ApplyNoise(float amplitude, float frequency)
    {
        for (int i = 0; i < 3; i++)
        {
            CinemachineBasicMultiChannelPerlin noise = _camera.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            if (noise != null)
            {
                noise.m_AmplitudeGain = amplitude;
                noise.m_FrequencyGain = frequency;
            }
        }
    }

    private void FixedUpdate()
    {
        // If the Cinemachine component is not set, avoid the shake
        if (_camera != null && _isShaking == true)
        {
            // If Camera Shake effect is still playing
            if (_elapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                ApplyNoise(amplitude, frequency);

                // Update Shake Timer
                _elapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                ApplyNoise(0f, 0f);

                Gamepad.current.SetMotorSpeeds(0f, 0f);

                _elapsedTime = 0f;
                _isShaking = false;
            }
        }
    }

    private void OnDestroy()
    {
        if (InputManager.instance != null)
        {
            InputManager.instance.SetVibration(0f, 0f);
        }
    }
}