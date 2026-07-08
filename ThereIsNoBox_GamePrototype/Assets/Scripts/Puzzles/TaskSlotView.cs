using UnityEngine;
using UnityEngine.UI;

public class TaskSlotView : MonoBehaviour, IDropTarget
{
    public GameObject currentDie;
    //[SerializeField] private TaskView parentTaskView;
    [SerializeField] private DiceEnums.RollType requiredType;
    [SerializeField] private Image resultImage;
    [SerializeField] private Image taskIcon;
    
    public static Color colorGreenTint = new Color(0.2f, 1f, 0.2f, 1f);

    public static Color colorRedTint = new Color(0.85f, 0.2f, 0.2f, 1f);

    public static Color colorYellowTint = new Color(0.9f, 0.82f, 0.2f, 1f);

    public static Color colorHeldTint = new Color(0.7f, 0.2f, 0.9f, 1f);

    public static Color colorGrayTint = new Color(0.5f, 0.5f, 0.5f, 1f);

    public static Color colorWhite = new Color(0f, 0f, 0f, 1f);
    
    public static Color kDisableColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    
    private Color colorEnabled = new Color(1f, 1f, 1f, 1f);

    private Color colorAvailable = new Color(0.3019608f, 0.3019608f, 0.3019608f, 1f);

    private Color colorLocked = new Color(0f, 0f, 0f, 1f);
    
    public DiceEnums.RollType RequiredType {get => requiredType;}

    public void MatchResult()
    {
        resultImage.color = Color.forestGreen;
        taskIcon.color = colorHeldTint;
    }
    public void UpdateTask(int i)
    {
        Debug.Log("UpdateTask");
        //parentTaskView.UpdateValue(i);
    }

    public bool isDropAllowed(Die dieToDrop)
    {
        Debug.Log("Dropped die type: " + dieToDrop.RollType);
        if (dieToDrop.RollType == requiredType) return true;
        return false;
    }

    public void DropDie(Die droppedDie)
    {
        Debug.Log("Accepting dropped die - need to do something");
        MatchResult();
    }
}
