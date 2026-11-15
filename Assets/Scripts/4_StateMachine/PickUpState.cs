using System.Collections;
using UnityEngine;

public class PickUpState : CharacterState
{
    private IDestination destination;
    private bool headingToTrash;

    protected override void Awake()
    {
        base.Awake();
        destination = character.Blackboard.LastSeenTrash;
    }

    void Start()
    {
        character.CancelNavigate();
        character.NavigateTo(destination);
        headingToTrash = true;
    }

    protected override void Update()
    {
        base.Update();

        if (character.IsCloseTo(destination) && headingToTrash)
        {
            character.PickUpTrash(character.Blackboard.LastSeenTrash);
            headingToTrash = false;
        }
    }

    protected override void ManageStateChange()
    {
        if (character.IsPickingTrash() || headingToTrash)
            return;

        character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move, true);
    }

    protected override void ManageVitals()
    {
        character.Vitals.RaiseHunger();
        character.Vitals.RaiseLoneliness();
        character.Vitals.RaiseSleepiness();
    }

    private IEnumerator PickUpTrashCoroutine()
    {
        yield return new WaitUntil(() => character.IsCloseTo(destination));
        character.PickUpTrash(character.Blackboard.LastSeenTrash);

        yield return new WaitUntil(() => !character.IsPickingTrash());
        character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move, true);
    }
}
