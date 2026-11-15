using UnityEngine;

public class GreetingState : CharacterState
{
    private void Start()
    {
        character.GreetCharacter(character.Blackboard.LastSeenFriend);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void ManageStateChange()
    {
        if (character.IsGreetingCharacter())
            return;

        character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move, true);
    }

    protected override void ManageVitals()
    {
        character.Vitals.LowerLoneliness();
    }
}
