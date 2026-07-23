using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Back In Your Box/Puzzle/Dice Pool")]
public class Puzzle: ScriptableObject
{

    public string name;
    public string description;
    
    public List<TaskData> tasks =  new List<TaskData>(); 

    public List<string> rewards =  new List<string>();
    public List<string> failures = new List<string>();

    public List<DieDefinition> allowedDice = new List<DieDefinition>();
    
    public bool isActive { get; private set; }
    
    //TODO Decide if task progression maintains between attempts or resets each time
    //TODO Decide if a task can be attempted multiple times in the same turn
    
    


}
