using UnityEngine;

/// <summary>
/// Used to update main player view
/// </summary>
public class HUDController: MonoBehaviour
{

    public GameObject HUD;

    public void ToggleHUD(bool state)
    {
        base.gameObject.SetActive(state);
    }
    
}