using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class ActionSystemUI : MonoBehaviour
{
    [SerializeField] Transform actionButtonContainer;
    [SerializeField] Transform actionButtonPrefab;
    [SerializeField] TMP_Text actionPointsText;

    List<ActionButtonUI> actionButtonList;

    private void Awake()
    {
        actionButtonList = new List<ActionButtonUI>();
    }

    void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += OnActionStarted;
        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
        Unit.OnAnyActionPointsChanged += OnAnyActionPointsChanged;


        CreateActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    void OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    void OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
    }

    void OnActionStarted(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    void OnTurnChanged(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    void OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    void CreateActionButtons()
    {
        foreach (Transform buttonTransform in actionButtonContainer)
        {
            Destroy(buttonTransform.gameObject);
        }

        actionButtonList.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach(BaseAction baseAction in selectedUnit.GetBaseActions())
        {
            Transform actionButton = Instantiate(actionButtonPrefab, actionButtonContainer);
            ActionButtonUI buttonUI = actionButton.GetComponent<ActionButtonUI>();
            buttonUI.SetBaseAction(baseAction);

            actionButtonList.Add(buttonUI);
        }
    }

    void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButton in actionButtonList)
        {
            actionButton.ShowSelectedVisual();
        }
    }

    void UpdateActionPoints()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        actionPointsText.text = "Action Points: " + selectedUnit.GetActionPoints();
    }
}
