using UnityEngine;
using UnityEngine.TextCore.Text;

public class SocialState : CharacterState
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
        if (character.Vitals.IsLonelinessBellowTarget)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move);
        }
    }

    protected override void ManageVitals()
    {
        character.Vitals.LowerLoneliness();
    }
}
