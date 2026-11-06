using UnityEngine;

public class CharacterShower : MonoBehaviour
{
    private Character[] characters;
    private Character currentCharacter;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = Finder.GameManager;
    }
    void Start()
    {
        characters = gameManager.CityObjects.Characters;

        currentCharacter = characters[0];
    }

    void Update()
    {
        
    }
}
