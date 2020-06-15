using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { private set; get; }

    [SerializeField]
    private GameFSM fsm = null;

    private void Awake()
    {
        // First destroy any existing instance of it
        if (instance != null)
        {
            Destroy(instance);
        }

        // Then reassign a proper one
        instance = this;
    }

    public AbstractFSM.StateBase GetCurrentState()
    {
        return fsm.currentState;
    }
}