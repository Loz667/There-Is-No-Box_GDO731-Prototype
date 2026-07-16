using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO make this a controller class?
public class DicePoolManager : MonoBehaviour
{
    //public static DicePoolManager Instance { get; private set; }
    
    [SerializeField] private List<DieDefinition> allowedDice = new List<DieDefinition>();
    
    
    
    //public DieDefinition standardDie;
    //public int initialCount = 6;
   
    //public Transform dicePool;
    //public GameObject diePrefab;
    //public Button rollButton;

    private List<Die> allDice = new List<Die>();
    private List<Die> activeDice = new List<Die>();
    
    public List<Die> AllDice => allDice;
    public List<Die> ActiveDice => activeDice;

    public bool TaskWasCompletedThisRoll = false;
    
    void Start()
    {
        

        //InitializeDicePool();
    }

    public void InitializeDicePool()
    {
        foreach (DieDefinition dieDef in allowedDice)
        {
            allDice.Add(new Die(dieDef));
        }
        
        activeDice.AddRange(allDice); //TODO Check this is right
        
        /*
        foreach (Transform child in dicePool)
        {
            Destroy(child.gameObject);
        }
        activeDice.Clear();

        // Spawn the requested number of dice
        for (int i = 0; i < allowedDice.Count; i++)
        {
            GameObject newDieObj = Instantiate(diePrefab, dicePool);
            DieUI dieUI = newDieObj.GetComponent<DieUI>();
            
            if (dieUI != null)
            {
                activeDice.Add(dieUI);
            }
        }
        Debug.Log($"Spawned {activeDice.Count} dice into the UI.");
        */
    }
    
    public void TempRollDice()
    {
        Debug.Log("PoolManager::TempRollDice");
        //ResetDicePool(); //TODO Temp solution
        foreach (Die d in activeDice)
        {
            d.Roll();
        }
    }

    public bool AnyDiceLeft
    {
        get
        {
            foreach (Die d in activeDice)
            {
                if (d.IsUsable) return true;
            }
            return false;
        }
    }

    public void ResetDicePool()
    {
        foreach (Die d in allDice) 
        {
            /*
            if (d.State != DiceEnums.DieState.Available)
            {
                activeDice.Remove(d);
            }
            */
        }
    }

    public void UseDice(List<Die> usedDice)
    {
        foreach (Die die in usedDice)
        {
            die.State = DiceEnums.DieState.Used;
        }
    }
    
    
    /*
    public void RollDice()
    {
        if (activeDice.Count == 0)
        {
            Debug.Log("No dice left to roll!");
            return;
        }
        
        foreach (DieUI die in activeDice)
        {
            DieFace rolledFace = standardDie.GetRoll();
            die.SetFace(rolledFace);
        }
    }
    

    public void RemoveDieFromPool(DieUI die)
    {
        if (activeDice.Contains(die))
        {
            activeDice.Remove(die);
            Debug.Log($"Die removed from active pool. Remaining dice: {activeDice.Count}");
        }
    }
    */
}
