using System;
using System.Collections.Generic;

[Serializable]
public class PlayerStateAttack : CharacterStateAttack
{
    public override void Exit()
    {
        List<VisualEffectController> fxByState = character.fxController.GetFxByState(flag);

        character.fxController.StopFx(fxByState);
    }
}