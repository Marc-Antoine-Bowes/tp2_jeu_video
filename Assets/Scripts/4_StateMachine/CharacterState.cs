using UnityEngine;

public abstract class CharacterState : MonoBehaviour
{
    protected Character character;

    protected virtual void Awake()
    {
        character = GetComponent<Character>();
    }

    protected virtual void Update()
    {
        ManageStateChange();
        ManageVitals();
    }

    protected abstract void ManageStateChange();
    protected abstract void ManageVitals();
}
