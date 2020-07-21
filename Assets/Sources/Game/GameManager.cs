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

        Load();
    }

    public void Load()
    {
        SaveData save = (SaveData)SerializationManager.Load(SaveData.SAVE_NAME);
        SaveData.current = save != null ? save : new SaveData();
    }

    public void Save()
    {
        SerializationManager.Save(SaveData.SAVE_NAME, SaveData.current);

        GameEvent.instance.DataSaved();
    }

    public void ResetProgression()
    {
        SaveData.current = new SaveData();

        Save();
    }

    public AbstractFSM.StateBase GetCurrentState()
    {
        return fsm.currentState;
    }
}