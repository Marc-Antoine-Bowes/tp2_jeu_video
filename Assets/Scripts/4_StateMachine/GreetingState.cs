using UnityEngine;

public class GreetingState : CharacterState
{
    private IDestination destination;

    protected override void Awake()
    {
        base.Awake();
        destination = character.Blackboard.LastSeenFriend;
    }

    void Start()
    {
        character.CancelNavigate();
        character.NavigateTo(destination);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (character.IsCloseTo(destination) && !character.IsGreetingCharacter())
        {
            character.GreetCharacter(destination as Character);
        }
    }

    protected override void ManageStateChange()
    {
        if (character.IsGreetingCharacter())
            return;

        stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move);
    }

    protected override void ManageVitals()
    {
        character.Vitals.LowerLoneliness();
    }
}
