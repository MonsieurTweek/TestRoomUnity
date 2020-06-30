using System;

[Serializable]
public class EnemyPhase
{
    public int treshold = 0;
    public EnemyStateMove stateMove = new EnemyStateMove();
    public EnemyStateAttack stateAttack = new EnemyStateAttack();    
}
