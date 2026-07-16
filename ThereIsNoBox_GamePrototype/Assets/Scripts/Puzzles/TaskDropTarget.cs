using UnityEngine;
using UnityEngine.UI;

public class TaskDropTarget : MonoBehaviour, IDropTarget
{
   
    [SerializeField] private DiceEnums.RollType requiredType;
    [SerializeField] private Image taskIcon;
    
    
    public bool isDropAllowed(Die dieToDrop)
    {
        Debug.Log("TASKDROPTARGET: Dropped die type: " + dieToDrop.RollType);
        if (dieToDrop.RollType == requiredType) return true;
        return false;
    }

    public void DropDie(Die droppedDie)
    {
        Debug.Log("Accepting dropped die - need to do something");
        TaskSlotView parentTask =  GetComponentInParent<TaskSlotView>();
        parentTask?.MatchResult(); //TODO work out whether this needs to be here or further up e.g. in TaskView?
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
