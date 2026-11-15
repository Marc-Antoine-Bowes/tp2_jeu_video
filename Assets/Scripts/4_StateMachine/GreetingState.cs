using UnityEngine;

public class GreetingState : CharacterState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character.CancelNavigate();
        character.GreetCharacter(character.Blackboard.LastSeenFriend);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void ManageStateChange()
    {
        if (character.IsGreetingCharacter())
            return;

        stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move, true);
    }

    protected override void ManageVitals()
    {
        character.Vitals.LowerLoneliness();
    }
}
