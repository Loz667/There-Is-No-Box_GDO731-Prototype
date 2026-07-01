using UnityEngine;
public class MapView: MonoBehaviour
{
 
    public void ToggleView()
    {
        Game.HUD.ToggleHUD(gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }
    
}
