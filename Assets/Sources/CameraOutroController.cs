using Cinemachine;
using UnityEngine;

public class CameraOutroController : MonoBehaviour
{
    [Header("References")]
    public CinemachineVirtualCamera cameraOutro = null;
    public CinemachineFollowZoom cameraZoom = null;

    [Header("Properties")]
    public float distanceToTarget = 5f;

    private CinemachineComposer _composer = null;

    private void Start()
    {
        _composer = cameraOutro.GetCinemachineComponent<CinemachineComposer>();

        CharacterGameEvent.instance.onOutroStarted += OnOutroStarted;
    }

    public void OnOutroStarted(Transform target)
    {
        Vector3 cameraPosition = target.position + target.forward * distanceToTarget;

        cameraPosition.y = Mathf.Max(target.localScale.y, 2f);

        cameraOutro.m_LookAt = target;
        cameraOutro.transform.position = cameraPosition;

        _composer.m_TrackedObjectOffset.y = target.localScale.y;

        cameraZoom.m_Width = target.localScale.y * 2f;

        cameraOutro.Priority = 100;
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onOutroStarted -= OnOutroStarted;
        }
    }
}