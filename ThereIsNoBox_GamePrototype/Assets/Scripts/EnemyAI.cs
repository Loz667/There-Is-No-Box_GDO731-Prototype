using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    float timer;

    void Start()
    {
        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
    }

    void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn()) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            TurnSystem.Instance.NextTurn();
        }
    }

    void OnTurnChanged(object sender, EventArgs e)
    {
        timer = 2f;
    }
}
