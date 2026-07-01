using UnityEngine;

public class PuzzleView : MonoBehaviour
{
    public void ToggleView()
    {
        Game.HUD.ToggleHUD(gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
