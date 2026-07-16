using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUI : UIPanel
{
    public enum PuzzleStates
    {
        StartPuzzle,
        PreRoll,
        Rolling,
        PostRoll,
        CompleteTask,
        FailTask,
        None
    }
    
    private PuzzleStates state = PuzzleStates.None; //TODO Figure out default
    [SerializeField] private Puzzle demoPuzzle;
    private Puzzle activePuzzle;
    
    [SerializeField] private bool debugMode;
    [SerializeField] private TaskView[] taskViews;
 
    [SerializeField] private DicePoolManager dicePool;
    [SerializeField] private DicePoolUI dicePoolUI;
    
    [SerializeField] public DiceActionArea diceActionArea;

    private TaskView activeTask;
    private List<Die> diceRequiredForTask = new List<Die>();
    
    private void Start()
    {
        if(debugMode) LoadPuzzle(demoPuzzle);//LoadPuzzleTemp();
    }
    
    public override void Show()
    {
        //LoadPuzzleTemp();
        base.Show();
    }
    
    //TODO Manage unloading of puzzle in Hide
    
    
    public void LoadPuzzle(Puzzle puzzle)
    {
        state = PuzzleStates.StartPuzzle;
        if (puzzle != null)
        {
            activePuzzle = puzzle;
            //Puzzle title?
            LoadTasks(puzzle);
            dicePool.InitializeDicePool();
        
            diceActionArea.rollButton.onClick.AddListener(RollDice);
        
            Debug.Log("Loading dice into UI");
            dicePoolUI.LoadDice(dicePool.AllDice);
            state = PuzzleStates.PreRoll;
        }
    }

    private void LoadTasks(Puzzle puzzle)
    {
        //Hide task panels first?
        List<TaskData> tasks = puzzle.tasks;
        //foreach (TaskData task in tasks)
        for(int i = 0; i < tasks.Count; i++)
        {
            taskViews[i].LoadTask(tasks[i]);
            //TaskView.LoadTask(task)
        }
    }
 
    public void RollDice()
    {
        if(state != PuzzleStates.PreRoll) return;
        //TODO Add different action depending on current state
        ChangeState( PuzzleStates.Rolling);
        
        diceActionArea.ChangeState(DiceActionArea.RollState.Rolled);
        dicePool.TempRollDice();
        dicePoolUI.UpdateSlots();
       
    }

    public void ChangeState(PuzzleStates newState)
    {
        //Do something with new state if necessary
        if(state == newState) return;
        state = newState;
        switch (state)
        {
            case PuzzleStates.PreRoll:
                break;
            case PuzzleStates.Rolling:
                break;
            case PuzzleStates.PostRoll:
               CheckPuzzleProgression();
                break;
            case PuzzleStates.CompleteTask:
                break;
            case PuzzleStates.FailTask:
                break;
        }
    }
    
    public void DieDroppedOnTask(Die droppedDie, TaskView task)
    {
        Debug.Log($"PuzzleUI: {droppedDie.RollType} on {task}");
        if(state != PuzzleStates.Rolling) return;

        if (activeTask != null && task != activeTask) return; //Tried dropping on another task after starting one
        
        if (activeTask == null)
        {
            activeTask = task;
            activeTask.SetSelected(true);
        }
        
        activeTask.UpdateTask(droppedDie);
        if (activeTask.IsTaskComplete())
        {
            Debug.Log("Woohoo! Task has been completed.");
            ChangeState( PuzzleStates.CompleteTask );
           
            //Shut the task door
            activeTask.CompleteTask();
           
            //Check if all the tasks have been completed?
            
            //Flag that a task has been completed this turn to reset DicePool button
            dicePool.TaskWasCompletedThisRoll = true;
            dicePool.UseDice(activeTask.CommittedDice);
            
            
            //Reset Task
            activeTask = null;
            ChangeState( PuzzleStates.PostRoll );
        }
    }

    public void DiscardDie(Die droppedDie)
    {
        Debug.Log("PuzzleUI: DiscardDie called");
        ChangeState(PuzzleStates.FailTask);
        droppedDie.State = DiceEnums.DieState.Lost;
        ChangeState( PuzzleStates.PostRoll );
       
    }
    
    

    private void CheckPuzzleProgression()
    {
        if (AllTasksComplete())
        {
            WinPuzzle();
            return;
        }

        if (!dicePool.AnyDiceLeft)
        {
            LosePuzzle();
            return;
        }
            
        dicePoolUI.ResetDice();
        diceActionArea.ChangeState(DiceActionArea.RollState.Ready);
        
        ChangeState(PuzzleStates.PreRoll); 
    }

    private bool AllTasksComplete()
    {
        foreach (TaskView t in taskViews)
        {
            if (t.IsActive) return false;
        }
        return true;
    }
    
    private void WinPuzzle()
    {
        Debug.Log("Win puzzle!");
        PuzzleDemo.Instance.Win();
    }
    
    private void LosePuzzle()
    {
        Debug.Log("Lose puzzle!");
        PuzzleDemo.Instance.Lose();
    }
    
    //TEST CODE
    /*
    
    public void LoadPuzzleTemp()
    {
        TempLoadTasks();
        dicePool.InitializeDicePool();
        
        diceActionArea.rollButton.onClick.AddListener(RollDice);
        
        Debug.Log("Loading dice into UI");
        dicePoolUI.LoadDice(dicePool.AllDice);
    }
    
    private void TempLoadTasks()
    {
        taskViews[0].Initialize(false);
    }
    
    
    */
}
