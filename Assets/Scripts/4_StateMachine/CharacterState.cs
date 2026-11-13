using UnityEngine;

public abstract class CharacterState : MonoBehaviour
{
    protected Character character;
    protected CharacterBlackboard blackboard;
    protected CharacterVitals vitals;
    protected CharacterStateMachine stateMachine;

    protected virtual void Awake()
    {
        character = GetComponent<Character>();
        stateMachine = GetComponent<CharacterStateMachine>();
    }

    protected virtual void Update()
    {
        ManageStateChange();
    }

    protected abstract void ManageStateChange();
}
