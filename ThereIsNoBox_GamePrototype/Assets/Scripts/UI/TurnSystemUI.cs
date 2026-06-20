using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] TMP_Text turnText;
    [SerializeField] Button endTurnButton;

    void Start()
    {
        endTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;

        UpdateTurnText();
    }

    void OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
    }

    void UpdateTurnText()
    {
        turnText.text = "TURN: " + TurnSystem.Instance.GetTurnNumber();
    }
}
