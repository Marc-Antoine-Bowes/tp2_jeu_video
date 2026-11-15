using UnityEngine;

public class WorkState : CharacterState
{
    private void Start()
    {
        if (character.IsAvailable)
            character.MakeInvisible();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void ManageStateChange()
    {
        if (character.Vitals.IsLonelinessAboveThreshold || character.Vitals.IsHungerAboveThreshold || character.Vitals.IsSleepinessAboveThreshold)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move);
        }
    }

    protected override void ManageVitals()
    {
        character.Vitals.RaiseHunger();
        character.Vitals.RaiseSleepiness();
        character.Vitals.RaiseLoneliness();
    }
}
