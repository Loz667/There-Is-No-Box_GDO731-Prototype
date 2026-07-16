using UnityEngine;
using UnityEngine.UI;

public class DieFaceUI : UIPanel
{

    public enum SlotState
    {
        EMPTY,
        AVAILABLE,
        ROLLED
    } 
    
    [SerializeField] private Image faceIcon;
    [SerializeField] private Sprite emptySlot;
    private SlotState _curState;
    
    public SlotState State
    {
        get => _curState;
        set
        {
            if (_curState != value)
            {
                _curState = value;
                StateChanged();
            }
        }
    }
 
    void Start()
    {
        _curState = SlotState.AVAILABLE;
        StateChanged();
    }

    public void SetDieFace(Sprite sprite)
    {
        faceIcon.sprite = sprite;
    }
    
    private void StateChanged()
    {
        switch (_curState)
        {
            case SlotState.AVAILABLE:
                faceIcon.sprite = emptySlot;
                canvasGroup.interactable = false;
                break;
            case SlotState.ROLLED:
                canvasGroup.interactable = true;
                break;
            case SlotState.EMPTY:
                Hide();
                break;
        }
        
    }
    
}
