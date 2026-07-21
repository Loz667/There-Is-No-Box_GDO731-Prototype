using UnityEngine;
using UnityEngine.UI;

public class TaskSlotView : MonoBehaviour
{
    public GameObject currentDie;
    //[SerializeField] private TaskView parentTaskView;
    
    [SerializeField] private Image resultImage;

    private DiceEnums.RollType requiredRollType;
    [SerializeField] private Image taskIcon;
    private bool hasMatchedDie = false;
    private bool isActive = false;
    
    
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
    
    public DiceEnums.RollType RequiredType {get => requiredRollType;}
    public bool IsFilled => hasMatchedDie;
    
    public bool IsActive => isActive;

    public void Initialize(DiceEnums.RollType requiredType, Sprite typeIcon)
    {
        requiredRollType = requiredType;
        taskIcon.sprite = typeIcon;
        //hasMatchedDie = false;
        isActive = true;
        ResetResult();
    }
    
    public void MatchResult()
    {
        resultImage.color = Color.forestGreen;
        hasMatchedDie = true;
        //taskIcon.color = colorHeldTint;
    }

    public void ResetResult()
    {
        resultImage.color = Color.red;
        hasMatchedDie = false;
    }
}
