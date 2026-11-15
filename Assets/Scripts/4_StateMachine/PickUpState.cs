using UnityEngine;

public class PickUpState : CharacterState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character.CancelNavigate();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void ManageStateChange()
    {
        if (character.IsPickingTrash())
            return;

        stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move);
    }

    protected override void ManageVitals()
    {
        character.Vitals.RaiseHunger();
        character.Vitals.RaiseLoneliness();
        character.Vitals.RaiseSleepiness();
    }
}
