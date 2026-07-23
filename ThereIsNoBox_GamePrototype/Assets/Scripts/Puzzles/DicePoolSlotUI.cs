using UnityEngine;
using UnityEngine.UI;

public class DicePoolSlotUI : MonoBehaviour
{

    [SerializeField] private Image slotBg;
    //[SerializeField] private Image faceIcon;
    //[SerializeField] private Sprite emptySlot;
    [SerializeField] private DieFaceUI dieFace;
   
    
    private int index;
    private Die die;
    
    private Color colorLocked = new Color(0f, 0f, 0f, 1f);
    
    private Color colorAvailable = new Color(1f, 1f, 1f, 1f);

    private Color colorUnavailable = new Color(0.3019608f, 0.3019608f, 0.3019608f, 1f);
    
    void Start()
    {
        //slotBg.color = colorLocked;
        //faceIcon.sprite = emptySlot;
        //faceIcon.color = colorUnavailable;
    }
    
    public void AddDie(Die newDie)
    {
        //Debug.Log("DicePoolSlotUI.AddDice called");
        die = newDie;
        slotBg.color = newDie.GetColor();
        //Debug.Log("BG slot color: " + slotBg.color);
        //faceIcon.color = colorAvailable;
    }

    public void UpdateFace()
    {
        if (die.rollIcon != null && die.State == DiceEnums.DieState.Rolling)
        {
            dieFace.SetDieFace(die.rollIcon);
            dieFace.State = DieFaceUI.SlotState.ROLLED;
        }
    }


    public Die GetDie() => die;
   

    public void RemoveDie() => dieFace.State = DieFaceUI.SlotState.AVAILABLE;
    
    /*
    public void LoseDie()
    {
        dieFace.State = DieFaceUI.SlotState.EMPTY;
    }
    */

    public void ResetSlot()
    {
        if (die.State == DiceEnums.DieState.Rolling || die.State == DiceEnums.DieState.Added)
        {
            dieFace.State = DieFaceUI.SlotState.AVAILABLE;
        }
        else
        {
            dieFace.State = DieFaceUI.SlotState.EMPTY;
        }
    }

    public void SetDieDraggedState()
    {
        dieFace.State = DieFaceUI.SlotState.DRAGGING;
    }
    
    
}
