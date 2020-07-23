public class MinionFSM : EnemyFSM
{
    /// <summary>
    /// Called from animation event
    /// Override to disable minion rather than destroying enemy
    /// </summary>
    public override void OnSingleAnimationEnded()
    {
        if (data.isAlive == true)
        {
            CharacterStateBase state = (CharacterStateBase)currentState;

            state.OnSingleAnimationEnded();
        }
        else
        {
            currentState.Exit();

            // Disable minion so it get back to the pool
            gameObject.SetActive(false);
        }
    }
}