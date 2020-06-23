using System;
using UnityEngine;

[Serializable]
public class Status : ScriptableObject
{
    public CharacterStatusEnum type = CharacterStatusEnum.NONE;
    public GameObject fx = null;

    public float duration = 0f;
    public float frequency = 0f;
    public bool isTicking = false;

    [HideInInspector]
    public float startTime = 0f;
    [HideInInspector]
    public float tickTime = 0f;

    [HideInInspector]
    public bool isActive = false;

    [HideInInspector]
    public GameObject currentFx = null;

    public virtual void Initialize() { }

    public void Enable(float duration)
    {
        this.duration = duration;
        startTime = Time.time;
        
        isActive = true;

        currentFx.SetActive(isActive);

        if (isTicking == true)
        {
            tickTime = Time.time;
        }
    }

    public void Disable()
    {
        isActive = false;

        currentFx.SetActive(isActive);
    }

    public virtual void Evaluate(CharacterFSM character) { }
}