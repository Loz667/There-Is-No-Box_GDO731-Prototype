using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUI : MonoBehaviour
{

    [SerializeField] private TaskView[] taskViews;
    [SerializeField] private TaskProgressUI progressView;

    [SerializeField] private DicePoolManager dicePool;
    [SerializeField] private DicePoolUI dicePoolUI;
    
    private Puzzle activePuzzle;
    
    //[SerializeField] public Button rollButton;
    [SerializeField] public DiceActionArea diceActionArea;

    private void Start()
    {
        LoadPuzzleTemp();
    }
    
    public void ToggleView()
    {
        Game.HUD.ToggleHUD(gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
        LoadPuzzleTemp();
    }

    public void LoadPuzzleTemp()
    {
        TempLoadTasks();
        dicePool.InitializeDicePool();
        
        diceActionArea.rollButton.onClick.AddListener(RollDice);
        
        Debug.Log("Loading dice into UI");
        dicePoolUI.LoadDice(dicePool.AllDice);
    }
    
    //TODO Fill this in later:
    
    /*
    public void LoadPuzzle(Puzzle puzzle)
    {
        if (puzzle != null)
        {
            activePuzzle = puzzle;
            //Puzzle title?
            LoadTasks(puzzle);
            
        }
    }

    private void LoadTasks(Puzzle puzzle)
    {
        //Hide task panels first?
        List<TaskData> tasks = puzzle.tasks;
        foreach (TaskData task in tasks)
        {
            //TaskView.LoadTask(task)
        }
    }
    */

    private void TempLoadTasks()
    {
        taskViews[0].Initialize(false);
    }

    public void RollDice()
    {
        //TODO Add different action depending on current state
        diceActionArea.ChangeState(DiceActionArea.RollState.Rolled);
        dicePool.TempRollDice();
        dicePoolUI.UpdateSlots();
        
    }

    public void DieDroppedOnTask()
    {
        Debug.Log("PuzzleUI: DieDroppedOnTask called");
    }

    public void DiscardDie()
    {
        Debug.Log("PuzzleUI: DiscardDie called");
        diceActionArea.ChangeState(DiceActionArea.RollState.Ready);
    }
    
}
