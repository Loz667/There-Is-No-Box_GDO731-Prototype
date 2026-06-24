using UnityEngine;

public class DieTarget : MonoBehaviour
{
    public GameObject currentDie;
    [SerializeField] private Task parentTask;

    public void UpdateTask(int i)
    {
        parentTask.UpdateValue(i);
    }
    
}
