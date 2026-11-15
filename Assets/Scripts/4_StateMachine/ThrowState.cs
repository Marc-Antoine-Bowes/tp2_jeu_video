using UnityEngine;

public class ThrowState : CharacterState
{
    private void Start()
    {
        character.CancelNavigate();
        character.ThrowTrash();
    }

    protected override void ManageStateChange()
    {
        if (character.IsThrowingTrash())
            return;

        character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move, true);
    }

    protected override void ManageVitals()
    {
        character.Vitals.RaiseHunger();
        character.Vitals.RaiseLoneliness();
        character.Vitals.RaiseSleepiness();
    }
}