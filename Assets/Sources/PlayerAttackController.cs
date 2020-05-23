using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private Animator _animator;

    public GameObject fx = null;

    public Transform anchor = null;

    private List<GameObject> _fxInPool = new List<GameObject>();
    private List<GameObject> _fxInUse = new List<GameObject>();

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) == true)
        {
            _animator.SetTrigger("Attack_1H");
        }
    }

    public void OnAttackPlayFx()
    {
        if (fx != null && anchor != null)
        {
            if (_fxInUse.Count >= _fxInPool.Count)
            {
                GameObject newFx = Instantiate<GameObject>(fx);
                
                newFx.transform.position = anchor.position;
                newFx.transform.rotation = transform.rotation;

                _fxInUse.Add(newFx);
            }
            else
            {
                // Use from pool
            }
        }
    }
}