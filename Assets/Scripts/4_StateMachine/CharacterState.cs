using UnityEngine;

public abstract class CharacterState : MonoBehaviour
{
    protected Character character;
    protected CharacterStateMachine stateMachine;

    protected virtual void Awake()
    {
        character = GetComponent<Character>();
        stateMachine = GetComponent<CharacterStateMachine>();
    }

    protected virtual void Update()
    {
        ManageStateChange();
        ManageVitals();
    }

    protected abstract void ManageStateChange();
    protected abstract void ManageVitals();
}
