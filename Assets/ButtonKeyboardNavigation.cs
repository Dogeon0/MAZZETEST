using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonKeyboardNavigation : MonoBehaviour
{
    [SerializeField] private Image highlightImage;
    private Button[] buttons;
    private int selectedButtonIndex = 0;
    public BattleStateMenuTransitions menuTransitions;
    public SoundHandler snds;
    public bool isActive = false;
    public bool Horizontal = false;
    public bool isSecondary;

    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        if (menuTransitions.transitionsDone)
        {
            SelectButton(selectedButtonIndex);
        }
    }

    void Update()
    {
        if (menuTransitions.transitionsDone)
        {
            if (isActive)
            {
                HandleBattleMenuNavigation();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && isActive && isSecondary)
            {
                menuTransitions.hideAttackOptions();
            }
        }
    }

    private void HandleBattleMenuNavigation()
    {
        if(isActive){
            if(Horizontal == false){
                if (Input.GetKeyDown(KeyCode.W))
                {
                    SelectPreviousButton();
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    SelectNextButton();
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    PressSelectedButton();
                }
            }
            else if(Horizontal){
                if (Input.GetKeyDown(KeyCode.A))
                {
                    SelectPreviousButton();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    SelectNextButton();
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    PressSelectedButton();
                }
            }
        }
        
    }

    private void SelectPreviousButton()
    {
        if(isActive){
            snds.PlaySound("misc_menu");
            selectedButtonIndex--;
            if (selectedButtonIndex < 0)
            {
                selectedButtonIndex = buttons.Length - 1;
            }
            SelectButton(selectedButtonIndex);
        }
    }

    private void SelectNextButton()
    {
        if(isActive)
        {
            snds.PlaySound("misc_menu");
            selectedButtonIndex++;
            if (selectedButtonIndex >= buttons.Length)
            {
                selectedButtonIndex = 0;
            }
            SelectButton(selectedButtonIndex);
        }
    }

    private void SelectButton(int index)
    {
        if(isActive)
        {
            EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
            MoveHighlightToButton(buttons[index].transform.position);
        }
    }

    private void PressSelectedButton()
    {
        if(isActive)
        {
            snds.PlaySound("load");
            if (selectedButtonIndex >= 0 && selectedButtonIndex < buttons.Length)
            {
                buttons[selectedButtonIndex].onClick.Invoke();
            }
        }
    }

    private void MoveHighlightToButton(Vector3 position)
    {
        if(isActive)
        {
            highlightImage.rectTransform.DOMove(position, 0.2f);
        }
    }
}
