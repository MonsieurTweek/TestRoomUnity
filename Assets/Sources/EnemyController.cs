using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject TargetIndicator = null;

    private void Awake()
    {
        SetActiveTargetIndicator(false);
    }

    public void SetActiveTargetIndicator(bool value)
    {
        TargetIndicator.gameObject.SetActive(value);
    }
}
