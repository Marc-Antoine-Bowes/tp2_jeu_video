using UnityEngine;

public class CharacterSensor : MonoBehaviour
{
    private CharacterBlackboard blackboard;
    
    void Start()
    {
        blackboard = GetComponent<CharacterBlackboard>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Trash>(out Trash trash))
        {
            blackboard.LastSeenTrash = trash;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Trash>(out Trash trash))
        {
            if (blackboard.LastSeenTrash == trash)
            {
                blackboard.LastSeenTrash = null;
            }
        }
    }
}
