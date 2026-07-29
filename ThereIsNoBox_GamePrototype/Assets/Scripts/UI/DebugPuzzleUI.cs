using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugPuzzleUI : MonoBehaviour
{
    [SerializeField] Button winButton;
    [SerializeField] Button loseButton;
    [SerializeField] Button exitButton;
    
    private int diceCount;
    void Start()
    {
        winButton.onClick.AddListener(() =>
        {
            WinPuzzle();
        });
        
        loseButton.onClick.AddListener(() =>
        {
            LosePuzzle();
        });
        
        exitButton.onClick.AddListener(() =>
        {
            Game.UI.ClosePuzzleView();
        });

    }
    
    private void WinPuzzle()
    {
        Debug.Log("Win puzzle!");
        PuzzleDemo.Instance.Win();
    }
    
    private void LosePuzzle()
    {
        Debug.Log("Lose puzzle!");
        PuzzleDemo.Instance.Lose();
    }
    
    
    

}
