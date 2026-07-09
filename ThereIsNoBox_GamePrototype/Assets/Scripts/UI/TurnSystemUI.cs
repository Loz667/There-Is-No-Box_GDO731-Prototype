using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] TMP_Text turnText;
    [SerializeField] Button endTurnButton;
    [SerializeField] GameObject enemyTurnVisual;

    void Start()
    {
        endTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.EndTurn();
        });

        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;

        UpdateTurnText();
        ShowEnemyTurnVisual();
        ShowEndButtonDuringPlayerTurn();
    }

    void OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
        ShowEnemyTurnVisual();
        ShowEndButtonDuringPlayerTurn();
    }

    void UpdateTurnText()
    {
        turnText.text = "TURN: " + TurnSystem.Instance.GetTurnNumber();
    }

    void ShowEnemyTurnVisual()
    {
        enemyTurnVisual.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    void ShowEndButtonDuringPlayerTurn()
    {
        endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}
