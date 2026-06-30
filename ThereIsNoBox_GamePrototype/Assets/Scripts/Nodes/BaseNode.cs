using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseNode : MonoBehaviour, IRaycastable
{
    [SerializeField] List<BaseNode> nearbyNodes = new List<BaseNode>();
    
    protected NavMeshAgent agent;

    protected Collider col;

    protected virtual void Awake()
    {
        agent = FindFirstObjectByType<NavMeshAgent>();
        col = GetComponent<Collider>();
    }

    public abstract void HandleRaycast();    
}
