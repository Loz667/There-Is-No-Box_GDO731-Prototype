using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class TaskView : MonoBehaviour, IDropTarget
{

    public enum SelectionState
    {
        Selected,
        NotSelected,
        NoneSelected 
    }

    public enum TaskState
    {
        InActive,
        Active,
        Complete
    }
    
    
    [SerializeField] private TaskSlotView[] targetSlots;
    [SerializeField] private RectTransform taskCover;
    
    [SerializeField] private Sprite spannerSprite;
    [SerializeField] private Sprite screwDriverSprite;
    
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject completeStripe;
    
    //[SerializeField] private List<DiceEnums.RollType> requiredTypes;
    private List<DiceEnums.RollType> requiredTypes = new List<DiceEnums.RollType>();
    public List<Die> CommittedDice { get; private set; }= new List<Die>();
   
    //private const float fullCoverHeight = 492f;
    //private const float openCoverHeight = 30f;
    private const float closedCover = 0f;
    private const float openCover = 450f;
    
    private TaskState state = TaskState.InActive;
    public bool IsActive => state == TaskState.Active;

    public void LoadTask(TaskData newTask)
    {
        requiredTypes.Clear();
        CommittedDice.Clear();
        
        List<RequiredRollData> taskGlyphs = newTask.requiredGlyphs;
        for (int i = 0; i < taskGlyphs.Count; i++)
        {
            LoadGlyph(targetSlots[i], taskGlyphs[i]);
            
        }
        completeStripe.SetActive(false);
        taskCover.anchoredPosition = new Vector2(taskCover.anchoredPosition.x, openCover);
        state = TaskState.Active;
    }
   
    private void LoadGlyph(TaskSlotView taskSlot, RequiredRollData glyph)
    {
        taskSlot.gameObject.SetActive(true);
        taskSlot.Initialize(glyph.requiredRoll.rollType, glyph.requiredRoll.targetIcon);
        requiredTypes.Add(glyph.requiredRoll.rollType);
       
    }
    
    
    public bool isDropAllowed(Die dieToDrop)
    {
       Debug.Log("RequiredTypes: " + string.Join(", ", requiredTypes));
        return requiredTypes.Contains(dieToDrop.RollType);
    }

    public void DropDie(Die droppedDie)
    {
        Debug.Log("TaskView::DropDie");
        PuzzleUI puzzleController = GetComponentInParent<PuzzleUI>();
        if (puzzleController != null)
        {
            puzzleController.DieDroppedOnTask(droppedDie, this);
        }
    }

    public void UpdateTask(Die droppedDie)
    {
        //Get Task Requirements
        foreach (TaskSlotView taskSlot in targetSlots)
        {
            if (taskSlot.IsActive)
            {
                Debug.Log("TaskSlot Type: " + taskSlot.RequiredType);
                Debug.Log("TaskSlot Filled: " + taskSlot.IsFilled);
                
                if (taskSlot.RequiredType == droppedDie.RollType && !taskSlot.IsFilled)
                {
                    Debug.Log("Matched result: " + droppedDie.RollType);
                    CommittedDice.Add(droppedDie);
                    taskSlot.MatchResult();
                    break;
                }
            }
            else
            {
                Debug.Log("Taskslot not active");
            }
        }
        
    }

    public bool IsTaskComplete()
    {
        Debug.Log("TaskView::IsTaskComplete()");
        
        foreach (TaskSlotView taskSlot in targetSlots)
        {
            if (taskSlot.IsActive && !taskSlot.IsFilled) return false;
        }
        return true;
        /*
        bool allMatched = true;
        foreach (TaskSlotView taskSlot in targetSlots)
        {
            Debug.Log("Checking taskSlot: " + taskSlot);
            if(taskSlot.IsActive) {allMatched = taskSlot.IsFilled;}
        }
        Debug.Log("All Matched? " + allMatched);
        return allMatched;
        */
    }

    public void CompleteTask()
    {
        completeStripe.SetActive(true);
        taskCover.anchoredPosition = new Vector2(taskCover.anchoredPosition.x, closedCover);
        SetSelected(false);
        state = TaskState.Complete;
    }

    public void SetSelected(bool selected)
    {
        Debug.Log("TaskView: SetSelected");
        if (selected)
        {
            closeButton.gameObject.SetActive(true);
            closeButton.interactable = true;
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else
        {
            closeButton.gameObject.SetActive(false);
            closeButton.interactable = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }


    public void FailTask()
    {
        SetSelected(false);
        GetComponentInParent<PuzzleUI>()?.ResetRolledDice();
        foreach (TaskSlotView taskSlot in targetSlots)
        {
            taskSlot.ResetResult();    
        }
        
    }
    
    
    
    //TEST CODE
    /*
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
    */
    
    

}
