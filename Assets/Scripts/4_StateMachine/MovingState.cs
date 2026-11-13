using UnityEngine;

public class MovingState : CharacterState
{
    private IDestination destination;

    override protected void Awake()
    {
        base.Awake();
        destination = stateMachine.Destination;
    }

    private void Start()
    {
        character.NavigateTo(destination);
    }

    override protected void Update()
    {
        base.Update();
        RaiseVitals();
    }

    protected override void ManageStateChange()
    {
        if (character.Blackboard.LastSeenFriend != null)
        {
            stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Greet);
        }
        else if (character.Blackboard.LastSeenTrash != null && stateMachine.TrashBehaviour == CityCharacterTrashBehaviour.PickUp)
        {
            stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Move);
        }
        else if (character.Blackboard.ShouldThrowTrash)
        {
            stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.ThrowTrash);
        }
        else if (character.IsCloseTo(destination))
        {
            if (destination is Building building)
            {
                switch (building.Type)
                {
                    case Building.CityBuildingType.House:
                        stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Sleep);
                        break;
                    case Building.CityBuildingType.Workplace:
                        stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Work);
                        break;
                    case Building.CityBuildingType.Food:
                        stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Eat);
                        break;
                    case Building.CityBuildingType.Social:
                        stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.Social);
                        break;
                }
            }
            else
            {
                stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterNextState.PickUpTrash);
            }
        }
    }

    private void RaiseVitals()
    {
        character.Vitals.RaiseHunger();
        character.Vitals.RaiseLoneliness();
        character.Vitals.RaiseSleepiness();
    }
}
