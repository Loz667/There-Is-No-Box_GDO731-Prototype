using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class LabPuzzle : UIPanel
{

    private readonly float target = 4f;
    private int progress = 0;
    private int volatilityCount = 1;
    [SerializeField] private DieDefinition dieDef;
    [SerializeField] private int availableDice = 3;
    
    [SerializeField] private VolatilityTracker tracker;
    [SerializeField] private DicePoolManager dicePool;
    [SerializeField] private DicePoolUI dicePoolUI;
    [SerializeField] private Slider containerLevel;
    [SerializeField] private TextMeshProUGUI volatilityDie;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] public Button rollButton;

    void Start()
    {
        //Show();
    }
    public override void Show()
    {
        containerLevel.value = GetProgress();
        volatilityCount = 1;
        tracker.init();
        resultText.text = "";
        volatilityDie.text = "";
        rollButton.onClick.AddListener(RollDice);
        SetupDice();
        
        base.Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupDice()
    {
        for (int i = 0; i < availableDice; i++)
        {
            dicePool.AssignDie(dieDef);
        }

        dicePool.InitializeDicePool();
        dicePoolUI.LoadDice(dicePool.AllDice);
    }
    

    public void RollDice()
    {
        
        /*
          if(state != PuzzleStates.PreRoll) return;
        //TODO Add different action depending on current state
        ChangeState( PuzzleStates.Rolling);

        diceActionArea.ChangeState(DiceActionArea.RollState.Rolled);
        dicePool.TempRollDice();
        dicePoolUI.UpdateSlots();

        */
        
        int volatileRoll = GetVolatilityRoll();
        int hitCount = GetDiceResults();
        string resultUpdate; 
        
        //Debug.Log("Volatile Roll: " +  volatileRoll);
        //Debug.Log("Hit Count: " + hitCount);
        
        //Update UI
        volatilityDie.text = volatileRoll.ToString();

        if (volatileRoll <= volatilityCount)
        {
            FailPuzzle();
            return;
        }

        if (hitCount > 0)
        {
            progress++;
            resultUpdate = $"Formula at {GetProgress() * 100}%";
        }
        else
        {
            resultUpdate = $"Formulation failed";
        }

        UpdateVolatility();
        resultUpdate += $", Volatility Level at {volatilityCount}";
        resultText.text = resultUpdate;
        containerLevel.value = GetProgress();

    }

    private async void FailPuzzle()
    {
        await PuzzleDemo.Instance.LoseLabAsync();
        
        Game.UI.CloseLabPuzzle();
        
        if (ScreenFader.Instance != null)
        {
            await ScreenFader.Instance.FadeOut();
            await Task.Delay(100);
            await ScreenFader.Instance.FadeIn();
        }
    }

    private int GetDiceResults()
    {
        int hits = 0;
        //Debug.Log($"GetDiceResults: Hits: {hits}");
        dicePool.TempRollDice();
        dicePoolUI.UpdateSlots();
        List<Die> rolledDice = dicePool.AllDice;
        /* Foreach die in DicePool, roll and add 1 if any hits
         * 
         */
        foreach (Die die in rolledDice)
        {
            if (die.RollResult == DiceEnums.DieResult.HIT)
            {
                //Debug.Log($"GetDiceResults: Rolled a hit");
                hits++;
            }
            else
            {
                //Debug.Log($"GetDiceResults: MISS");
            }
        }
        return hits;
    }

    private int GetVolatilityRoll() => Random.Range(1, 9);

    private void UpdateVolatility()
    {
        volatilityCount++;
        tracker.TrackerUpdate(volatilityCount); //Update UI
    }
    
    private float GetProgress() => (progress == 0) ? 0f : progress/target;
}
