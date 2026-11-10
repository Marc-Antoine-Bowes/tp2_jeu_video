using System.Linq;
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
        else if (other.isTrigger)
        {
            return;
        }
        else if (other.TryGetComponent<Character>(out Character character))
        {
            if (blackboard.Friends.Contains(character))
            {
                blackboard.LastSeenFriend = character;
            }
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
        else if (other.isTrigger)
        {
            return;
        }
        else if (other.TryGetComponent<Character>(out Character character))
        {
            if (blackboard.LastSeenFriend == character)
            {
                blackboard.LastSeenFriend = null;
            }
        }

    }
}
