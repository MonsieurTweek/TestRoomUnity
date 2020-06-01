using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("References")]
    public CinemachineFreeLook cameraFree = null;
    public CinemachineFreeLook cameraTarget = null;
    public CinemachineTargetGroup targetGroup = null;

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
}
