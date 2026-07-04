using UnityEngine;
using UnityEngine.UI;

public class TaskSlotView : MonoBehaviour
{
    public GameObject currentDie;
    //[SerializeField] private TaskView parentTaskView;
    [SerializeField] private DiceEnums.RollType requiredType;
    [SerializeField] private Image resultImage;
    
    public DiceEnums.RollType RequiredType {get => requiredType;}

    public void MatchResult()
    {
        resultImage.color = Color.forestGreen;
    }
    public void UpdateTask(int i)
    {
        Debug.Log("UpdateTask");
        //parentTaskView.UpdateValue(i);
    }
    
}
