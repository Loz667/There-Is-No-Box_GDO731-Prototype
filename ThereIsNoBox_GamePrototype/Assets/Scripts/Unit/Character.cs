using System;
using UnityEngine;
using UnityEngine.AI;

public class Character: MonoBehaviour
{
    //[SerializeField] private Transform prefabParent;
    public enum CharacterState
    {
        Alive,
        Dead    
    }
    public CharacterDef Data { get; private set; }
    
    private NavMeshAgent agent; //TODO manage this in a 'mover' class?
    
    private CharacterState state = CharacterState.Alive;
    
    private void OnDisable() => UnsubscribeEvents();


    public void Initialize(CharacterDef data)
    {
        Data = data;
        //Health, Morale, ActionPoints

        if (data.CharacterPrefab == null) return;
        GameObject cp = Instantiate(data.CharacterPrefab, transform);
        cp.transform.localPosition = Vector3.zero;
        cp.transform.localRotation = Quaternion.identity;
        
    }
    
    
    
    
    private void SubscribeEvents()
    {
       
    }

    private void UnsubscribeEvents()
    {
        //EventBroker<SelectActionEvent>.OnEvent -= OnActionSelect;
        
    }
}
