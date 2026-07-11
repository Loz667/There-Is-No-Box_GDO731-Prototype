using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TaskView : MonoBehaviour, IDropTarget
{
   
    [SerializeField] private TaskSlotView[] targetSlots;
    [SerializeField] private RectTransform taskCover;
    
    [SerializeField] private Sprite spannerSprite;
    [SerializeField] private Sprite screwDriverSprite;
    
    [SerializeField] private List<DiceEnums.RollType> requiredTypes;
    
    private const float fullCoverHeight = 492f;
    private const float openCoverHeight = 30f;
    private const float closedCover = 0f;
    private const float openCover = 450f;
    
    public void Initialize(bool isHidden)
    {
        //if(!isHidden) taskCover.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,openCover);
        if (!isHidden)
        {
            taskCover.anchoredPosition = new Vector2(taskCover.anchoredPosition.x, openCover);
            
            SetupTaskIcon(targetSlots[0]);
            SetupTaskIcon(targetSlots[1]);
        }
    }

    private void SetupTaskIcon(TaskSlotView taskSlot)
    {
        taskSlot.gameObject.SetActive(true);
        taskSlot.Initialize(DiceEnums.RollType.SPANNER, spannerSprite);
    }
    
    public bool isDropAllowed(Die dieToDrop)
    {
        Debug.Log("Dropped die type: " + dieToDrop.RollType);
        if (requiredTypes.Contains(dieToDrop.RollType)) return true;
        return false;
    }

    public void DropDie(Die droppedDie)
    {
        Debug.Log("Accepting dropped die " + droppedDie.RollType);

        foreach (TaskSlotView taskSlot in targetSlots)
        {
            if (taskSlot.IsActive)
            {
                if (taskSlot.RequiredType == droppedDie.RollType && !taskSlot.IsFilled)
                {
                    taskSlot.MatchResult();
                    break;
                }
            }
        }
        
        
        /*
        PuzzleUI puzzleController = GetComponentInParent<PuzzleUI>();
        if (puzzleController != null)
        {
            puzzleController.DieDroppedOnTask();
        }
        else
        {
            Debug.Log("TaskViewSlot - can't find PuzzleUI.");
        }
        */
        //MatchResult();
    }
    

}
