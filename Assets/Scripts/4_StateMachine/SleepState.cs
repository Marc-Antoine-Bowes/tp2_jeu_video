using UnityEngine;

public class SleepState : CharacterState
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
        if (character.Vitals.IsSleepinessBellowTarget)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move);
        }
    }

    protected override void ManageVitals()
    {
        character.Vitals.LowerSleepiness();
    }
}
