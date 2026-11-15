using UnityEngine;

public class FoodState : CharacterState
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void ManageStateChange()
    {
        if (character.Vitals.IsHungerBellowTarget)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move);
        }
    }

    protected override void ManageVitals()
    {
        character.Vitals.LowerHunger();
    }
}
