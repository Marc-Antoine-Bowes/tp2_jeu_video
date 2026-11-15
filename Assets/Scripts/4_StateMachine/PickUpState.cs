using System.Collections;
using UnityEngine;

public class PickUpState : CharacterState
{
    private IDestination destination;

    protected override void Awake()
    {
        base.Awake();
        destination = character.Blackboard.LastSeenTrash;
    }

    void Start()
    {
        character.CancelNavigate();
        character.NavigateTo(destination);
    }

    protected override void Update()
    {
        base.Update();
        if (character.IsCloseTo(destination) && !character.IsPickingTrash())
        {
            character.PickUpTrash(destination as Trash);
        }
    }

    protected override void ManageStateChange()
    {
        if (character.IsPickingTrash())
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
