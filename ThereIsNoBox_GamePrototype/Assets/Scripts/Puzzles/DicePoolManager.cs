using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO make this a controller class?
public class DicePoolManager : MonoBehaviour
{
   
    [SerializeField] private List<DieDefinition> allowedDice = new List<DieDefinition>();

    private List<Die> allDice = new List<Die>();
    private List<Die> activeDice = new List<Die>();
    
    public List<Die> AllDice => allDice;
    public List<Die> ActiveDice => activeDice;

    public bool TaskWasCompletedThisRoll = false;
    
    public void InitializeDicePool()
    {
        foreach (DieDefinition dieDef in allowedDice)
        {
            allDice.Add(new Die(dieDef));
        }
        
        activeDice.AddRange(allDice); //TODO Check this is right
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

    public void UseDice(List<Die> usedDice)
    {
        foreach (Die die in usedDice)
        {
            die.State = DiceEnums.DieState.Used;
        }
    }

    public void AssignDie(DieDefinition dieDef)
    {
        allowedDice.Add(dieDef);
    }
    
}
