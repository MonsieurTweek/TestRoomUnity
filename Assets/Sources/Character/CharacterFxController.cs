using System.Collections.Generic;
using UnityEngine;

public class CharacterFxController : MonoBehaviour
{
    public Transform anchor = null;

    private List<VisualEffectController> _fxInUse = new List<VisualEffectController>();

    public VisualEffectController PlayFx(Object prefab, bool isRooted, uint stateId)
    {
        // If we can't play the fx we return null
        if (prefab == null || (isRooted == false && anchor == null))
        {
            return null;
        }

        GameObject gameObject = GamePoolManager.instance.UseFromPool(prefab.name);

        VisualEffectController newFx = gameObject.GetComponent<VisualEffectController>();

        UseFx(newFx, isRooted, stateId);

        return newFx;
    }

    public void StopFx(VisualEffectController fx)
    {
        fx.Stop();

        _fxInUse.Remove(fx);
    }

    public void StopFx(List<VisualEffectController> list)
    {
        foreach (VisualEffectController fx in list)
        {
            StopFx(fx);
        }
    }

    public List<VisualEffectController> GetFxByState(uint stateId)
    {
        List<VisualEffectController> fxByState = new List<VisualEffectController>();

        foreach (VisualEffectController fx in _fxInUse)
        {
            if (fx.stateId == stateId)
            {
                fxByState.Add(fx);
            }
        }

        return fxByState;
    }

    private void UseFx(VisualEffectController fx, bool isRooted, uint stateId)
    {
        

        if (isRooted == false)
        {
            fx.AttachToAnchor(anchor);
        }
        else
        {
            fx.transform.position = transform.position;
            fx.transform.rotation = transform.rotation;
        }

        fx.Reset(stateId);

        _fxInUse.Add(fx);
    }
}