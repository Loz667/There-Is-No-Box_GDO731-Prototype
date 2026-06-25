using UnityEngine;

public class TaskSlotView : MonoBehaviour
{
    public GameObject currentDie;
    [SerializeField] private TaskView parentTaskView;

    public void UpdateTask(int i)
    {
        parentTaskView.UpdateValue(i);
    }
    
}
