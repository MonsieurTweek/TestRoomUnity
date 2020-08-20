using TMPro;
using UnityEngine;

public class GameStatisticController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI enemyKilled;
    public TextMeshProUGUI enemyKilledTotal;

    public TextMeshProUGUI damageInflicted;
    public TextMeshProUGUI damageReceived;

    public TextMeshProUGUI timeAverage;
    public TextMeshProUGUI timeTotal;

    public void RefreshForSession()
    {
        enemyKilled.text = SaveData.current.playerProfile.sessionStatistics.countEnemyKilled.ToString();
        enemyKilledTotal.text = SaveData.current.playerProfile.totalStatistics.countEnemyKilled.ToString();

        damageInflicted.text = SaveData.current.playerProfile.sessionStatistics.countDamageInflicted.ToString();
        damageReceived.text = SaveData.current.playerProfile.sessionStatistics.countDamageReceived.ToString();

        timeAverage.text = string.Format("{0}s", Mathf.RoundToInt(SaveData.current.playerProfile.sessionStatistics.timeAverage).ToString());
        timeTotal.text = string.Format("{0}s", Mathf.RoundToInt(SaveData.current.playerProfile.sessionStatistics.timeTotal).ToString());
    }
}