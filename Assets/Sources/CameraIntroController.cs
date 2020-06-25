using Cinemachine;
using UnityEngine;

public class CameraIntroController : MonoBehaviour
{
    public CinemachineVirtualCamera cameraIntro = null;

    private void Start()
    {
        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;
    }

    public void OnIntroStarted(Transform target, AbstractCharacterData _)
    {
        cameraIntro.m_LookAt = target;

        cameraIntro.Priority = 100;
    }

    public void OnIntroEnded()
    {
        cameraIntro.Priority = 0;
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onIntroStarted -= OnIntroStarted;
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;
        }
    }
}