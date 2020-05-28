using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("References")]
    public CinemachineFreeLook thirdPersonCamera = null;
    public CinemachineTargetGroup targetGroup = null;

    public void FollowTarget(Transform target)
    {
        targetGroup.AddMember(target, 0.25f, 0f);
        thirdPersonCamera.m_YAxis.m_InputAxisName = string.Empty;
    }

    public void ReleaseTarget(Transform target)
    {
        targetGroup.RemoveMember(target);
        thirdPersonCamera.m_YAxis.m_InputAxisName = "Mouse X";
    }

    public void ToggleFreekLook(bool isToggle)
    {
        thirdPersonCamera.m_XAxis.m_InputAxisName = isToggle == true ? "Mouse X" : string.Empty;
    }
}
