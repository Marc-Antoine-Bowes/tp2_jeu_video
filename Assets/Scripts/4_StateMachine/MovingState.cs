using UnityEngine;

public class MovingState : CharacterState
{
    private IDestination destination;

    override protected void Awake()
    {
        base.Awake();
        destination = character.StateMachine.Destination;
    }

    private void Start()
    {
        if (!character.IsAvailable)
            character.MakeVisible();

        character.NavigateTo(destination);
    }

    override protected void Update()
    {
        base.Update();
    }

    protected override void ManageStateChange()
    {
        if (character.Blackboard.LastSeenFriend != null)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Greet);
        }
        else if (character.Blackboard.LastSeenTrash != null && character.StateMachine.TrashBehaviour == CityCharacterTrashBehaviour.PickUp)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.PickUpTrash);
        }
        else if (character.Blackboard.ShouldThrowTrash)
        {
            character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.ThrowTrash);
        }
        else if (character.IsCloseTo(destination))
        {
            if (destination is Building building)
            {
                switch (building.Type)
                {
                    case Building.CityBuildingType.House:
                        character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Sleep);
                        break;
                    case Building.CityBuildingType.Workplace:
                        character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Work);
                        break;
                    case Building.CityBuildingType.Food:
                        character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Eat);
                        break;
                    case Building.CityBuildingType.Social:
                        character.StateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Social);
                        break;
                }
            }
        }
    }

    protected override void ManageVitals()
    {
        character.Vitals.RaiseHunger();
        character.Vitals.RaiseLoneliness();
        character.Vitals.RaiseSleepiness();
    }
}
