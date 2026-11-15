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
    [SerializeField] private Scrollbar characterHunger;
    [SerializeField] private Scrollbar characterLoneliness;
    [SerializeField] private Scrollbar characterSleep;
    [SerializeField] private Image avatar;
    [SerializeField] private Color dangerColor = Color.red;
    private Color normalColor;

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

        normalColor = characterHunger.targetGraphic.color;

        characterHunger.interactable = false;
        characterLoneliness.interactable = false;
        characterSleep.interactable = false;

        characterHunger.transition = Selectable.Transition.None;
        characterLoneliness.transition = Selectable.Transition.None;
        characterSleep.transition = Selectable.Transition.None;

        setCharacter();
    }

    void Update()
    {
        manageCharacterToBeShown();
        UpdateVitalsUI();
    }

    void manageCharacterToBeShown()
    {
        if (nextCharacter.WasPressedThisFrame())
        {
            currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;
            currentCharacter.HidePointer();
            setCharacter();
        }
        else if (previousCharacter.WasPressedThisFrame())
        {
            currentCharacterIndex = (currentCharacterIndex - 1 + characters.Length) % characters.Length;
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

        UpdateVitalsUI();
    }

    void UpdateVitalsUI()
    {
        if (currentCharacter == null) return;

        characterHunger.size = currentCharacter.Vitals.Hunger / 100f;
        characterLoneliness.size = currentCharacter.Vitals.Loneliness / 100f;
        characterSleep.size = currentCharacter.Vitals.Sleepiness / 100f;
        characterName.text = $"{currentCharacter.FullName}\n({currentCharacter.StateMachine.CurrentStateName})";

        SetBarColor(characterHunger, currentCharacter.Vitals.IsHungerAboveThreshold);
        SetBarColor(characterLoneliness, currentCharacter.Vitals.IsLonelinessAboveThreshold);
        SetBarColor(characterSleep, currentCharacter.Vitals.IsSleepinessAboveThreshold);
    }

    void SetBarColor(Scrollbar bar, bool isDanger)
    {
        bar.targetGraphic.color = isDanger ? dangerColor : normalColor;
    }
}