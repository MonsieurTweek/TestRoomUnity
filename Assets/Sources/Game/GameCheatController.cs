using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameCheatController : MonoBehaviour
{
    public UnityEvent killEnemy = null;
    public UnityEvent killPlayer = null;

#if UNITY_EDITOR
    private void Start()
    {
        InputManager.instance.cheat.Kill_Enemy.performed += KillEnemy;
        InputManager.instance.cheat.Kill_Player.performed += KillPlayer;
    }

    private void OnDestroy()
    {
        if (InputManager.instance != null)
        {
            InputManager.instance.cheat.Kill_Enemy.performed -= KillEnemy;
            InputManager.instance.cheat.Kill_Player.performed -= KillPlayer;
        }
    }

    private void KillEnemy(InputAction.CallbackContext context)
    {
        killEnemy.Invoke();
    }

    private void KillPlayer(InputAction.CallbackContext context)
    {
        killPlayer.Invoke();
    }
#endif
}
