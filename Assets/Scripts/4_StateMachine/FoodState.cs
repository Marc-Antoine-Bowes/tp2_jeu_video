using UnityEngine;

public class FoodState : CharacterState
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void ManageStateChange()
    {
        character.Vitals.LowerHunger();

        if (character.Vitals.IsHungerBellowTarget)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move);
        }
    }
}
