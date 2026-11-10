using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterShower : MonoBehaviour
{
    private Character[] characters;
    private Character currentCharacter;

    [SerializeField] private int currentCharacterIndex = 0;
    private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterHunger;
    [SerializeField] private Image characterLoneliness;
    [SerializeField] private Image characterSleep;
    [SerializeField] private Image avatar;

    private InputAction nextCharacter;
    private InputAction previousCharacter;
    private void Awake()
    {
        gameManager = Finder.GameManager;
    }
    void Start()
    {
        nextCharacter = InputSystem.actions.FindAction("NextCharacter");
        previousCharacter = InputSystem.actions.FindAction("PreviousCharacter");
        characters = gameManager.CityObjects.Characters;

        currentCharacter = characters[currentCharacterIndex];
    }

    void Update()
    {
        manageCharacterToBeShown();
    }

    void manageCharacterToBeShown()
    {
        if(nextCharacter.WasPressedThisFrame()) {
            if (currentCharacterIndex == characters.Length - 1) {
                currentCharacterIndex = 0;
            } else
            {
                currentCharacterIndex++;
            }
            currentCharacter.HidePointer();
            setCharacter();
        }
        if (previousCharacter.WasPressedThisFrame()) {
            if(currentCharacterIndex == 0)
            {
                currentCharacterIndex = characters.Length - 1;
            }
            else
            {
                currentCharacterIndex--;
            }
            currentCharacter.HidePointer();
            setCharacter();
        }

    }

    void setCharacter()
    {
        currentCharacter = characters[currentCharacterIndex];
        currentCharacter.ShowPointer();
        characterName.text = currentCharacter.FullName;
        avatar.sprite = currentCharacter.Avatar;

        characterHunger.fillAmount = currentCharacter.Vitals.Hunger;

        characterHunger.fillAmount = currentCharacter.Vitals.Hunger;

        characterHunger.fillAmount = currentCharacter.Vitals.Hunger;
    }
}
