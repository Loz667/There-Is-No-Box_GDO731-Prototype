using UnityEngine;
using UnityEngine.UI;

public class DiceRemovalUI : UIPanel, IDropTarget
{
    
    [SerializeField] private Image portalImage;
    
    private bool allowDiscard = false;
    
   
    //TODO Need to change the portalImage color depending on whether it is active or not

    public bool AllowDiscard{
        get { return allowDiscard; }
        set
        {
            allowDiscard = value;
            UpdateDiscardIcon();
        }   
    }
    

    public bool isDropAllowed(Die dieToDrop)
    {
        Debug.Log("Die dropped on discard portal");
        return allowDiscard;
    }

    public void DropDie(Die droppedDie)
    {
        //droppedDie.State = DiceEnums.DieState.Discarded;
        GetComponentInParent<PuzzleUI>()?.DiscardDie(droppedDie);
        /*
        PuzzleUI puzzleController = GetComponentInParent<PuzzleUI>();
        if (puzzleController != null)
        {
            puzzleController.DiscardDie(); //TODO Handle removal of dice in PuzzleUI or here? 
        }
        */
    }

    private void UpdateDiscardIcon()
    {
        if (allowDiscard)
        {
            portalImage.color = Color.cyan;
        }
        else
        {
            portalImage.color = Color.gray;
        }
    }
    
    
}
