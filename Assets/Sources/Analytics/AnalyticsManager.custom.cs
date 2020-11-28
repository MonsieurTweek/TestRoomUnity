using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public partial class AnalyticsManager : MonoBehaviour
{
    public static void GameInit(bool isFirsSession)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["first_session"] = isFirsSession;

        AnalyticsEvent.Custom("game_init", parameters);
    }

    public static void ChangeState(GameStateEnum fromState, GameStateEnum toState)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["from_state"] = fromState;
        parameters["to_state"] = toState;

        AnalyticsEvent.Custom("change_state", parameters);
    }

    public static void SpawnEnemy(EnemyFSM enemy)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["name"] = enemy.data.name;
        parameters["power"] = enemy.data.power;

        AnalyticsEvent.Custom("spawn_enemy", parameters);
    }

    public static void SpawnPlayer(string name)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["name"] = name;

        AnalyticsEvent.Custom("spawn_player", parameters);
    }

    public static void SelectTile(string name)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["element"] = name;

        AnalyticsEvent.Custom("ui_select_tile", parameters);
    }

    public static void StartConfirmInput(string name)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["element"] = name;

        AnalyticsEvent.Custom("ui_confirm_started", parameters);
    }

    public static void CancelConfirmInput(string name, int progress)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["element"] = name;
        parameters["confirmation_progress"] = progress;

        AnalyticsEvent.Custom("ui_confirm_canceled", parameters);
    }

    public static void DisplayPerks(List<Perk> perks)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        for (int i = 0; i < perks.Count; i++)
        {
            parameters[$"perk_{i + 1}_name"] = perks[i].title;
            parameters[$"perk_{i + 1}_type"] = perks[i].type;
            parameters[$"perk_{i + 1}_usage"] = perks[i].usage;
        }

        AnalyticsEvent.Custom("unlock_perk", parameters);
    }

    public static void UnlockPerk(Perk perk)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["type"] = perk.type;
        parameters["usage"] = perk.usage;

        AnalyticsEvent.Custom("unlock_perk", parameters);
    }

    public static void SwitchControl(bool isKeyboard)
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        parameters["is_keyboard"] = isKeyboard;

        AnalyticsEvent.Custom("switch_control", parameters);
    }

    public static void GameQuit()
    {
        Dictionary<string, object> parameters = instance.GetParameters();

        AnalyticsEvent.Custom("game_quit", parameters);
    }
}