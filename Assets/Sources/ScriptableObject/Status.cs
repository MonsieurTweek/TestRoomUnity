using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Status", menuName = "ScriptableObjects/Status")]
[Serializable]
public class Status : ScriptableObject
{
    public CharacterStatusEnum type = CharacterStatusEnum.NONE;
    public GameObject currentFx = null;

    public bool isActive = false;

    public float startTime = 0f;
    public float duration = 0f;

    public void Enable(float duration)
    {
        this.duration = duration;
        startTime = Time.time;
        isActive = true;

        currentFx.SetActive(isActive);
    }

    public void Disable()
    {
        isActive = false;

        currentFx.SetActive(isActive);
    }
}