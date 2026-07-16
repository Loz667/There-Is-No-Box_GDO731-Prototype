using UnityEngine;
using UnityEngine.UI;

public class ContainmentLockUI : MonoBehaviour
{
    
    private Color colorOn = new Color(0f, 1f, 0f, 1f);
    private Color colorOff = new Color(1f, 0f, 0f, 1f);

    [SerializeField] private Image lockIcon;
    [SerializeField] private GameObject valveGlow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockIcon.color = colorOn;
    }

    public void TurnOff()
    {
        valveGlow.SetActive(false);
        lockIcon.color = colorOff;
    }
    
}
