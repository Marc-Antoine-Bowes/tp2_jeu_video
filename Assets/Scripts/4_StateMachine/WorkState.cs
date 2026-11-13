using UnityEngine;

public class WorkState : CharacterState
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void ManageStateChange()
    {
        character.Vitals.RaiseHunger();
        character.Vitals.RaiseSleepiness();
        character.Vitals.RaiseLoneliness();

        if (character.Vitals.IsLonelinessAboveThreshold)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move);
        }
        else if (character.Vitals.IsHungerAboveThreshold)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Eat);
        }
        else if (character.Vitals.IsSleepinessAboveThreshold) 
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Sleep);
        }
    }
}
