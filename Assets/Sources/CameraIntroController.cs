using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraIntroController : MonoBehaviour
{
    [Header("References")]
    public CinemachineVirtualCamera cameraIntro = null;
    public CinemachineFollowZoom cameraZoom = null;

    [Header("Properties")]
    public float distanceToTarget = 5f;
    public float offsetAnimationDuration = 0.5f;

    private CinemachineComposer _composer = null;

    private void Start()
    {
        _composer = cameraIntro.GetCinemachineComponent<CinemachineComposer>();

        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroPaused += OnIntroPaused;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;
    }

    private void OnIntroStarted(Transform target, AbstractCharacterData _)
    {
        Vector3 cameraPosition = target.position + target.forward * distanceToTarget;

        cameraPosition.y = Mathf.Max(target.localScale.y, 2f);

        cameraIntro.m_LookAt = target;
        cameraIntro.transform.position = cameraPosition;

        _composer.m_TrackedObjectOffset.x = 0f;
        _composer.m_TrackedObjectOffset.y = target.localScale.y + 0.5f;

        cameraZoom.m_Width = target.localScale.y * 2f;

        cameraIntro.Priority = 100;
    }

    private void OnIntroPaused()
    {
        LeanTween.value(0f, -2f, offsetAnimationDuration).setOnUpdate(OnTrackedObjectOffsetUpdated);
    }

    private void OnTrackedObjectOffsetUpdated(float progress)
    {
        _composer.m_TrackedObjectOffset.x = progress;
    }

    private void OnIntroEnded()
    {
        cameraIntro.Priority = 0;
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onIntroStarted -= OnIntroStarted;
            CharacterGameEvent.instance.onIntroPaused -= OnIntroPaused;
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;
        }
    }
}