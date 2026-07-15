using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DebugPanelUI: UIPanel
{
    [SerializeField] TMP_Text roundText;
    [SerializeField] Button endTurnButton;
    [SerializeField] GameObject enemyTurnVisual;



    private void OnEnable() => SubscribeEvents();
    private void OnDisable() => UnsubscribeEvents();
    
    void Start()
    {
        endTurnButton.onClick.AddListener(() =>
        {
            Game.Director.TriggerTurnEnd();
        });

        UpdateTurnText();
        ShowEnemyTurnVisual();
        ShowEndButtonDuringPlayerTurn();
    }

    void OnTurnStart(TurnStartEvent e)
    {
        Debug.Log("DebugUI: OnTurnStart");
        UpdateTurnText();
        ShowEnemyTurnVisual();
        ShowEndButtonDuringPlayerTurn();
    }
    
    void UpdateTurnText()
    {
        Debug.Log("DebugUI: UpdateTurnText");
        roundText.text = "Round: " + Game.Director.RoundNumber;
    }

    void ShowEnemyTurnVisual()
    {
        enemyTurnVisual.SetActive(!Game.Director.IsPlayerTurn);
    }

    void ShowEndButtonDuringPlayerTurn()
    {
        endTurnButton.gameObject.SetActive(Game.Director.IsPlayerTurn);
    }
    
    private void SubscribeEvents()
    {
        EventBroker<TurnStartEvent>.OnEvent += OnTurnStart;
    }

    private void UnsubscribeEvents()
    {
        EventBroker<TurnStartEvent>.OnEvent -= OnTurnStart;
        
    }
    
}
