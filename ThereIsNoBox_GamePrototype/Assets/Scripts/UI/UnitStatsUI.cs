using System;
using TMPro;
using UnityEngine;

public class UnitStatsUI : MonoBehaviour
{
    [SerializeField] Unit unit;

    [Header("Stats")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text moraleText;
    [SerializeField] TMP_Text actionPointsText;

    void Start()
    {
        Unit.OnAnyHealthPointsChanged += OnHealthPointsChanged;
        UpdateHealthText();

        Unit.OnAnyMoralePointsChanged += OnMoralePointsChanged;
        UpdateMoraleText();

        Unit.OnAnyActionPointsChanged += OnActionPointsChanged;
        UpdatePointsText();
    }

    void UpdateHealthText()
    {
        healthText.text = unit.GetHealthPoints().ToString();
    }

    void OnHealthPointsChanged(object sender, EventArgs e)
    {
        UpdateHealthText();
    }

    void UpdateMoraleText()
    {
        if (unit.IsEnemy())
            moraleText.gameObject.SetActive(false);
        else
            moraleText.text = unit.GetMoralePoints().ToString();
    }

    void OnMoralePointsChanged(object sender, EventArgs e)
    {
        UpdateMoraleText();
    }

    void UpdatePointsText()
    {
        actionPointsText.text = unit.GetActionPoints().ToString();
    }

    void OnActionPointsChanged(object sender, EventArgs e)
    {
        UpdatePointsText();
    }
}
