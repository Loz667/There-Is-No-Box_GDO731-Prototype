using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleDemo : UIPanel
{
    
    public static PuzzleDemo Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Image background;
    
    private Color colorWin = new Color(0.1f, 0.4f, 0.2f, 1f);
    private Color colorLoss = new Color(0.65f, 0.2f, 0.2f, 1f);

    void Awake()
    {
        base.Awake();
        Instance = this;
    }

    void Start()
    {
        Hide();
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Win()
    {
        background.color = colorWin;
        _headerText.text = "You completed the puzzle";
        _messageText.text = "Well done. I'll get you a badge saying 'I was very brave and completed a puzzle'";
        Show();
    }

    // Update is called once per frame
    public void Lose()
    {
        background.color = colorLoss;
        _headerText.text = "FAILED";
        _messageText.text = "You failed the puzzle. When it all collapses down around your ears and the world comes to a screaming halt, you're the one they'll blame.";
        Show();
    }
}
