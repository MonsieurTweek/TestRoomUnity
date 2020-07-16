using System;
using System.Collections.Generic;

[Serializable]
public class PlayerStateAttack : CharacterStateAttack
{
    public override void Exit()
    {
        // TODO : Keep fx back to the pool instead
        // GameObject.Destroy(_currentFx);

        List<VisualEffectController> fxByState = character.fxController.GetFxByState(flag);

        character.fxController.StopFx(fxByState);
    }
}