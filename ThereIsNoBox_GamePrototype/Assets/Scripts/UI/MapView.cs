using UnityEngine;
public class MapView: MonoBehaviour
{
    
    public bool IsActive => base.gameObject.activeSelf;
    
    public void ToggleView()
    {
        //Game.HUD.ToggleHUD(gameObject.activeSelf);
        base.gameObject.SetActive(!gameObject.activeSelf);
    }
    
}
