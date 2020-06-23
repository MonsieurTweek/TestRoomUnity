using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterFSM))]
public class CharacterStatusController : MonoBehaviour
{
    public Transform anchor = null;

    public Status poisonStatus = null;
    public Status stunStatus = null;

    private CharacterFSM _character = null;

    private Dictionary<CharacterStatusEnum, Status> _statusByType = new Dictionary<CharacterStatusEnum, Status>();

    private void Awake()
    {
        _character = GetComponent<CharacterFSM>();
    }

    private void Start()
    {
        CharacterGameEvent.instance.onStunned += OnStunned;
        CharacterGameEvent.instance.onPoisonned += OnPoisonned;
    }

    private void OnStunned(uint uniqueId, float duration)
    {
        if (_character.data.uniqueId == uniqueId && _character.data.isAlive == true)
        {
            // Create a new stun status
            if (_statusByType.ContainsKey(CharacterStatusEnum.STUN) == false)
            {
                Status stun = Instantiate(stunStatus);
                GameObject fx = Instantiate<GameObject>(stun.fx, anchor.position, anchor.rotation);

                fx.transform.parent = transform;

                stun.currentFx = fx;

                // Keep track of status logic
                _statusByType.Add(CharacterStatusEnum.STUN, stun);
            }

            _statusByType[CharacterStatusEnum.STUN].Enable(duration);

            // Add status to status mask
            _character.data.SetStatus(CharacterStatusEnum.STUN);

            _character.TransitionToStun(duration);
        }
    }

    private void OnPoisonned(uint uniqueId, float duration)
    {
        if (_character.data.uniqueId == uniqueId && _character.data.isAlive == true)
        {
            // Create a new poison status
            if (_statusByType.ContainsKey(CharacterStatusEnum.POISON) == false)
            {
                Status poison = Instantiate(poisonStatus);
                GameObject fx = Instantiate<GameObject>(poison.fx, anchor.position, anchor.rotation);

                fx.transform.parent = transform;

                poison.currentFx = fx;

                // Keep track of status logic
                _statusByType.Add(CharacterStatusEnum.POISON, poison);
            }

            _statusByType[CharacterStatusEnum.POISON].Enable(duration);

            // Add status to status mask
            _character.data.SetStatus(CharacterStatusEnum.POISON);
        }
    }

    private void Update()
    {
        if (_character.data.isAlive == true)
        {
            foreach(Status status in _statusByType.Values)
            {
                if (status.isActive == true)
                {
                    // Status has ticked
                    if (Time.time - status.tickTime >= status.frequency)
                    {
                        status.tickTime = Time.time;
                        status.Evaluate(_character);
                    }

                    // Status has ended
                    if (Time.time - status.startTime >= status.duration)
                    {
                        status.Disable();

                        _character.data.RemoveStatus(status.type);

                        if (status.type == CharacterStatusEnum.STUN)
                        {
                            _character.TransitionToIdle();
                        }
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onStunned -= OnStunned;
            CharacterGameEvent.instance.onPoisonned -= OnPoisonned;
        }
    }
}