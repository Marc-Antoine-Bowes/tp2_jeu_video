using UnityEngine;
using UnityEngine.TextCore.Text;

public class SocialState : CharacterState
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void ManageStateChange()
    {
        character.Vitals.LowerLoneliness();

        if (character.Vitals.IsLonelinessBellowTarget)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.move);
        }
    }
}
