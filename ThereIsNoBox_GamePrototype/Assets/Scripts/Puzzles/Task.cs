using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Task : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diceValue;
    
    [SerializeField] private List<DieTarget> targetSlots;
    
    
    
    private int diceTotal = 0;

    void Start()
    {
        diceValue.text = diceTotal.ToString();
    }

    public void UpdateValue(int value)
    {
        diceTotal += value;
        diceValue.text = diceTotal.ToString();
    }
    

}
