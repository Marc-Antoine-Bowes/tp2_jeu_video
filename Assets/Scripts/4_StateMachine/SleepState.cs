using UnityEngine;

public class SleepState : CharacterState
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void ManageStateChange()
    {
        character.Vitals.LowerSleepiness();

        if (character.Vitals.IsSleepinessBellowTarget)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.move);
        }
    }
}
