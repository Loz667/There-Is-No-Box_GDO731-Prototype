using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleDemo : UIPanel
{
    
    public static PuzzleDemo Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private GameObject textContent;
    [SerializeField] private GameObject rewardContent;
    [SerializeField] private Image background;
    [SerializeField] private Button _buttonOk;
    [SerializeField] private Button _buttonCancel;
    
    private Color colorWin = new Color(0.1f, 0.4f, 0.2f, 1f);
    private Color colorLoss = new Color(0.65f, 0.2f, 0.2f, 1f);
    private Color colorAction = new Color(0.2f, 0.2f, 0.5f, 1f);
    
    private TaskCompletionSource<bool> _dialogResultSource;

    void Awake()
    {
        base.Awake();
        Instance = this;
        
        _buttonOk.onClick.AddListener(() => ResolveDialog(true));
        _buttonCancel.onClick.AddListener(() => ResolveDialog(false));
    }

    void Start()
    {
        Hide();
    }

    public void CloseDialog()
    {
        Hide();
    }
    
    private void ResolveDialog(bool result)
    {
        Hide();
        _dialogResultSource?.TrySetResult(result);
    }
    
    public Task<bool> WinAsync()
    {
        _buttonCancel.gameObject.SetActive(false);
        textContent.SetActive(false);
        rewardContent.SetActive(true);
        background.color = colorWin;
        _headerText.text = "SUCCESS";
        _messageText.text = "Well done. I'll get you a badge saying 'I was very brave and completed a puzzle'";
        
        Show();
        _dialogResultSource = new TaskCompletionSource<bool>();
        return _dialogResultSource.Task;
    }
    
    public Task<bool> LoseAsync()
    {
        _buttonCancel.gameObject.SetActive(false);
        textContent.SetActive(true);
        rewardContent.SetActive(false);
        background.color = colorLoss;
        _headerText.text = "FAILED";
        _messageText.text = "Your inadequate peformance has attracted the attention of the HR Manager. Lose 1 morale.";
        
        Show();
        _dialogResultSource = new TaskCompletionSource<bool>();
        return _dialogResultSource.Task;
    }
    
    public Task<bool> LoseLabAsync()
    {
        _buttonCancel.gameObject.SetActive(false);
        textContent.SetActive(true);
        rewardContent.SetActive(false);
        background.color = colorLoss;
        _headerText.text = "FAILED";
        _messageText.text = "Compound volatility level exceeded. All progress has been lost.";
        
        Show();
        _dialogResultSource = new TaskCompletionSource<bool>();
        return _dialogResultSource.Task;
    }
    
    public Task<bool> GoToNextRoomAsync()
    {
        _buttonCancel.gameObject.SetActive(true);
        textContent.SetActive(true);
        rewardContent.SetActive(false);
        background.color = colorAction;
        _headerText.text = "MOVE";
        _messageText.text = "Move to next room (Costs 1 Action)";
        
        Show();
        _dialogResultSource = new TaskCompletionSource<bool>();
        return _dialogResultSource.Task;
    }

    // Keep existing methods as wrappers to avoid breaking legacy calls
    public void Win() => _ = WinAsync();
    public void Lose() => _ = LoseAsync();
    public void GoToNextRoom() => _ = GoToNextRoomAsync();
    /*
    public void Win()
    {
        _buttonCancel.gameObject.SetActive(false);
        textContent.SetActive(false);
        rewardContent.SetActive(true);
        background.color = colorWin;
        _headerText.text = "SUCCESS";
        _messageText.text = "Well done. I'll get you a badge saying 'I was very brave and completed a puzzle'";
        Show();
    }

    // Update is called once per frame
    public void Lose()
    {
        _buttonCancel.gameObject.SetActive(false);
        textContent.SetActive(true);
        rewardContent.SetActive(false);
        background.color = colorLoss;
        _headerText.text = "FAILED";
        _messageText.text = "You failed the puzzle. When it all collapses down around your ears and the world comes to a screaming halt, you're the one they'll blame.";
        Show();
    }

    public void GoToNextRoom()
    {
        _buttonCancel.gameObject.SetActive(true);
        textContent.SetActive(true);
        rewardContent.SetActive(false);
        background.color = colorLoss;
        _headerText.text = "MOVE";
        _messageText.text = "Move to next room (Costs 1 Action)";
        Show();
    }
    */
}
