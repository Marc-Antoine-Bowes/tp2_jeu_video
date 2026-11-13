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
        if (blackboard.LastSeenFriend != null)
        {
            // TODO : Implémenter la logique pour changer l'état du personnage lorsqu'il voit un ami.
        }

        if (blackboard.ShouldThrowTrash)
        {
            // TODO : Implémenter la logique pour changer l'état du personnage lorsqu'il doit lancer un déchet.
        }

        if(blackboard.LastSeenTrash != null && stateMachine.TrashBehaviour == CityCharacterTrashBehaviour.PickUp)
        {
            // TODO : Implémenter la logique pour changer l'état du personnage lorsqu'il voit un déchet.
        }

        if (character.IsCloseTo(destination))
        {
            // TODO : Implémenter la logique pour changer l'état du personnage une fois arrivé à destination.
        }
    }

    private void RaiseVitals()
    {
        vitals.RaiseHunger();
        vitals.RaiseLoneliness();
        vitals.RaiseSleepiness();
    }

    public void SetDestination(IDestination destination)
    {
        this.destination = destination;
    }
}
