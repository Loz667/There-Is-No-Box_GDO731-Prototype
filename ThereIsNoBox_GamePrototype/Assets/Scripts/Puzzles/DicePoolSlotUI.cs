using UnityEngine;
using UnityEngine.UI;

public class DicePoolSlotUI : MonoBehaviour
{

    [SerializeField] private Image slotBg;
    [SerializeField] private Image faceIcon;
    [SerializeField] private Sprite emptySlot;
    
    private int index;
    private Die die;
    
    private Color colorLocked = new Color(0f, 0f, 0f, 1f);
    
    private Color colorAvailable = new Color(1f, 1f, 1f, 1f);

    private Color colorUnavailable = new Color(0.3019608f, 0.3019608f, 0.3019608f, 1f);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slotBg.color = colorLocked;
        faceIcon.sprite = emptySlot;
        //faceIcon.color = colorUnavailable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddDie(Die newDie)
    {
        Debug.Log("DicePoolSlotUI.AddDice called");
        die = newDie;
        slotBg.color = newDie.GetColor();
        //Debug.Log("BG slot color: " + slotBg.color);
        //faceIcon.color = colorAvailable;
    }

    public void UpdateFace()
    {
        faceIcon.sprite = die.rollIcon != null ? die.rollIcon : emptySlot;
    } 
    

    public Die GetDie()
    {
        return die;
    }

    public void RemoveDie()
    {
        faceIcon.sprite = emptySlot;
        die.State = DiceEnums.DieState.Used;
    }

    public void ResetDie()
    {
        if (die.State == DiceEnums.DieState.Rolling)
        {
            die.State = DiceEnums.DieState.Available;
        }
        else
        {
            die.State = DiceEnums.DieState.Lost;
        }
    }
    
    
}
