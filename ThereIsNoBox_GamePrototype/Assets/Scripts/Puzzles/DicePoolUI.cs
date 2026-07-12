using System.Collections.Generic;
using UnityEngine;

public class DicePoolUI : MonoBehaviour
{

    [SerializeField] private DicePoolSlotUI[] diceSlots;

    public void LoadDice(List<Die> allDice)
    {
        Debug.Log("DicePoolUI.LoadDice called");
        if (allDice.Count > diceSlots.Length)
        {
            Debug.LogError("There are more dice than dice slots");
            return;
        }

        int i = 0;
        foreach (Die die in allDice)
        {
            diceSlots[i++].AddDie(die);
        }
    }

    public void UpdateSlots()
    {
        foreach (DicePoolSlotUI slot in diceSlots)
        {
            slot.UpdateFace();
        }
    }

    public void ResetDice()
    {
        foreach (DicePoolSlotUI slot in diceSlots)
        {
            slot.ResetDie();
        }
    }


}
