using System.Collections.Generic;
using UnityEngine;

public partial class AnalyticsManager : MonoBehaviour
{
    #region Singleton
    public static AnalyticsManager instance { private set; get; }

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
    #endregion
    
    private Dictionary<string, object> GetParameters()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        
        parameters["game_state"] = GameManager.instance.GetCurrentState() != null ? (GameStateEnum)GameManager.instance.GetCurrentState().flag : GameStateEnum.NONE;

        return parameters;
    }
}