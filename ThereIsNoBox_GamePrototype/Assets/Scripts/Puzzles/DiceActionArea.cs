using UnityEngine;
using UnityEngine.UI;

public class DiceActionArea : MonoBehaviour
{
    private static readonly int IsActive = Animator.StringToHash("IsActive");
    private static readonly int Normal = Animator.StringToHash("Normal");

    public enum RollState
    {
       Ready,
       Rolled,
       Hidden
    }
    
    [SerializeField] public DiceRemovalUI discardUI;
    [SerializeField] public Button rollButton;
    [SerializeField] private Animator buttonAnimator;
    
    private RollState curState = RollState.Hidden;

    public void SetButtonActive(bool isActive)
    {
        discardUI.AllowDiscard = !isActive;
        if(isActive) buttonAnimator.SetTrigger(Normal);
        buttonAnimator.SetBool(IsActive, isActive);
        rollButton.interactable = isActive;
    }

    public void SetHidden()
    {
        curState = RollState.Hidden;
    }

    public void ChangeState(RollState newState)
    {
        if (curState != newState)
        {
            curState = newState;
            switch (curState)
            {
                case RollState.Hidden:
                    SetHidden();
                    break;
                case RollState.Rolled:
                    SetButtonActive(false);
                    break;
                case RollState.Ready:
                    SetButtonActive(true);
                    break;
            }
        }
    }
    
}
