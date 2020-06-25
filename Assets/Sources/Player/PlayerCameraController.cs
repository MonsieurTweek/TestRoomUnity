using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("References")]
    public CinemachineFreeLook cameraFree = null;
    public CinemachineFreeLook cameraTarget = null;

    public CinemachineTargetGroup targetGroup = null;

    private void Start()
    {
        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;
    }

    public void OnIntroStarted(Transform target, AbstractCharacterData _)
    {
        cameraFree.Priority = 0;
        cameraTarget.Priority = 0;
    }

    public void OnIntroEnded()
    {
        cameraFree.Priority = 100;
    }

    public void FollowTarget(Transform target)
    {
        targetGroup.AddMember(target, 0.25f, 0f);

        cameraTarget.Priority = 100;
        cameraFree.Priority = 0;
    }

    public void ReleaseTarget(Transform target)
    {
        targetGroup.RemoveMember(target);

        cameraFree.Priority = 100;
        cameraTarget.Priority = 0;
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
