using UnityEngine;
using UnityEngine.AI;

public class LocationNode : BaseNode
{
    public override void HandleRaycast()
    {
        Arrive();
    }

    void Arrive()
    {
        GameManager.instance.currentNode = this;

        agent.SetDestination(transform.position);

        if (col != null)
        {
            col.enabled = false;
        }
    }
}
