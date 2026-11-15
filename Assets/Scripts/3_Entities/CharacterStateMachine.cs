using System.Runtime.CompilerServices;
using UnityEngine;

// Il s'agit de la classe dans laquelle vous allez implémenter la machine à état. Vous aurez tout particulièrement à
// compléter la méthode "ChangeCharacterState" (Vous aurez pour cela besoin de créer une énumération des prochains états)
//
// Notez que c'est aussi ici que les personnages "malpropres" sont gérés. Pour les personnages "malpropre", cette classe
// contient une méthode qui, de temps en temps, met le booléen "shouldThrowTrash" du CharacterBlackboard à "true". Dans
// votre machine à état, cela signifie qu'il est temps de changer d'état pour lancer un déchet.
//
// Au cas où ce n'était pas encore clair, vous avez à modifier cette classe pour le travail.
public class CharacterStateMachine : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField] private CityCharacterTrashBehaviour trashBehaviour = CityCharacterTrashBehaviour.Ignore;
    [SerializeField, Range(0, 100)] private float throwTrashChances = 5f;
    [SerializeField, Min(0)] private float throwTrashCheckDelay = 1f;

    private Character character;
    private float throwTrashCheckTimer;
    private CharacterState state;
    private IDestination destination;

    public string CurrentStateName => state != null ? state.GetType().Name : "None";

    public enum CharacterNextState { Move, Work, Eat, Sleep, Social, PickUpTrash, ThrowTrash, Greet}

    public CityCharacterTrashBehaviour TrashBehaviour => trashBehaviour;
    
    public IDestination Destination => destination;

    private void Awake()
    {
        // Get dependencies.
        character = GetComponent<Character>();

        // Init timers.
        throwTrashCheckTimer = 0;
    }

    private void Start()
    {
        ChangeCharacterState(CharacterNextState.Move);
    }

    private void Update()
    {
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        if (trashBehaviour == CityCharacterTrashBehaviour.Throw)
        {
            throwTrashCheckTimer += Time.deltaTime;
            if (throwTrashCheckTimer >= throwTrashCheckDelay)
            {
                character.Blackboard.ShouldThrowTrash = RandomUtils.Chance(throwTrashChances);
                throwTrashCheckTimer = 0f;
            }
        }
        else
        {
            throwTrashCheckTimer = 0f;
        }
    }

    private void UpdateDestination()
    {
        if (character.Vitals.IsHungerAboveThreshold)
        {
            destination = character.Blackboard.FoodBuildings.Random();
        }
        else if (character.Vitals.IsSleepinessAboveThreshold)
        {
            destination = character.Blackboard.House;
        }
        else if (character.Vitals.IsLonelinessAboveThreshold)
        {
            destination = character.Blackboard.SocialBuildings.Random();
        }
        else
        {
            destination = character.Blackboard.Workplace;
        }
    }

    public void ChangeCharacterState(CharacterNextState nextState, bool keepDestination = false)
    {
        Destroy(state);

        switch (nextState)
        {
            case CharacterNextState.Move:
                if (!keepDestination) {UpdateDestination();}
                state = gameObject.AddComponent<MovingState>();
                break;
            case CharacterNextState.Eat:
                state = gameObject.AddComponent<FoodState>();
                break;
            case CharacterNextState.Sleep:
                state = gameObject.AddComponent<SleepState>();
                break;
            case CharacterNextState.Social: 
                state = gameObject.AddComponent<SocialState>();
                break;
            case CharacterNextState.Work:
                state = gameObject.AddComponent<WorkState>();
                break;
            case CharacterNextState.ThrowTrash:
                state = gameObject.AddComponent<ThrowState>();
                break;
            case CharacterNextState.PickUpTrash:
                state = gameObject.AddComponent<PickUpState>();
                break;
            case CharacterNextState.Greet:
                state = gameObject.AddComponent<GreetingState>();
                break;
        }
    }
}
public enum CityCharacterTrashBehaviour
{
    Ignore,
    PickUp,
    Throw
}