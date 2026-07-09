using System;
using UnityEngine;

public class InteractAction : BaseAction
{
    int maxInteractDistance = 1;

    void Update()
    {
        if (!isActive) return;

        ActionCompleted();
    }

    public override string GetActionName()
    {
        return "Interact";
    }

    public override void TakeAction(Action onActionComplete)
    {
        Debug.Log("Interacting");
        ActionStarted(onActionComplete);
    }
}
