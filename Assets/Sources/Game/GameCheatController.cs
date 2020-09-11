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
        InputManager.instance.cheat.Klapaucius.performed += GetMoney;
    }

    private void OnDestroy()
    {
        if (InputManager.instance != null)
        {
            InputManager.instance.cheat.Kill_Enemy.performed -= KillEnemy;
            InputManager.instance.cheat.Kill_Player.performed -= KillPlayer;
            InputManager.instance.cheat.Klapaucius.performed -= GetMoney;
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

    private void GetMoney(InputAction.CallbackContext context)
    {
        SaveData.current.playerProfile.currency += 100;

        GameManager.instance.Save();
    }
#endif
}
