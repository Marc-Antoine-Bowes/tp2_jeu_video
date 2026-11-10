using UnityEngine;

public abstract class CharacterState : MonoBehaviour
{

    protected IDestination destination;
    void Update()
    {
        ManageStateChange();
    }

    public abstract void ManageStateChange();
}
