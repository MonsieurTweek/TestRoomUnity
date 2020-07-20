using System;

[Serializable]
public class EnemyPhase
{
    public bool isActive = true;
    public float tresholdMin = 0f;
    public float tresholdMax = 100f;

    public EnemyStateMove stateMove = new EnemyStateMove();
    public EnemyStateAttack stateAttack = new EnemyStateAttack();

    public bool IsAvailable(AbstractCharacterData data)
    {
        float percentage = data.health * 100f / data.healthMax;

        return isActive == true && (percentage >= tresholdMin && percentage <= tresholdMax);
    }
}
