using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUI : MonoBehaviour
{

    [SerializeField] private TaskView[] taskViews;
    [SerializeField] private GameObject progressView;

    [SerializeField] private DicePoolManager dicePool;
    [SerializeField] private DicePoolUI dicePoolUI;
    
    private Puzzle activePuzzle;
    
    [SerializeField] public Button rollButton;

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
        dicePool.InitializeDicePool();
        if (rollButton != null)
        {
            rollButton.onClick.AddListener(RollDice);
        }
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

    public void RollDice()
    {
        Debug.Log("Rolling Dice");
        dicePool.TempRollDice();
        dicePoolUI.UpdateSlots();
    }
    
    
    
}
