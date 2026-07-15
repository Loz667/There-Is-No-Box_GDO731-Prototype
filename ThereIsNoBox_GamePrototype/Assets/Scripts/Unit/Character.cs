using System;
using UnityEngine;
using UnityEngine.AI;

public class Character: MonoBehaviour
{
    //[SerializeField] private Transform prefabParent;
    public enum CharacterState
    {
        Alive,
        Dead,
        Helpless
    }
    
    private bool isActive = false;
    
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }
    public int CurrentMorale { get; private set; }
    public int MaxMorale { get; private set; }
    public string Name { get; private set; }
    public int ActionPoints { get; private set; }
    
    public CharacterDef Data { get; private set; }
    
    private NavMeshAgent agent; //TODO manage this in a 'mover' class?

    public CharacterState Status { get; private set; } = CharacterState.Alive;

    private void OnDisable() => UnsubscribeEvents();

    public RoomManager CurrentRoom { get; private set; }

    

    public void Initialize(CharacterDef data)
    {
        Data = data;
        //Health, Morale, ActionPoints
        MaxHealth = data.MaxHealth;
        MaxMorale = data.MaxMorale;
        Name = data.name;
        ActionPoints = 0; //Will be set when Character becomes active character

        if (data.CharacterPrefab == null) return;
        GameObject cp = Instantiate(data.CharacterPrefab, transform);
        cp.transform.localPosition = Vector3.zero;
        cp.transform.localRotation = Quaternion.identity;
        
        //Set CurrentHealth
        CurrentHealth = MaxHealth;
        CurrentMorale = MaxMorale;
        
    }

    public void IsActive(bool setActive)
    {
        isActive = setActive;
        if (setActive)
        {
            ActionPoints = Game.Director.maxActions;
            SubscribeEvents();
        }
        else
        {
            ActionPoints = 0;
            UnsubscribeEvents();
        }
    }
    
    public void EnterRoom(RoomManager newRoom)
    {
        Debug.Log("EnterRoom " + newRoom);
        if (newRoom == CurrentRoom) return;
        if( CurrentRoom != null) CurrentRoom.RemoveCharacter(this);
        CurrentRoom = newRoom;
        CurrentRoom.AddCharacter(this);
    }
    
    public void AdjustHealth(int amt)
    {
        if (amt == 0 || Status != CharacterState.Alive) return;
        
        CurrentHealth += amt;
        if(CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        //Check for change in health by caching amount?
        if (CurrentHealth <= 0)
        {
            Status = CharacterState.Dead;
            CurrentHealth = 0;
            ActionPoints = 0;
            //TODO Send unit death notification
        }
    }
    
    public void AdjustMorale(int amt)
    {
        if (amt == 0 || Status != CharacterState.Alive) return;
        
        CurrentMorale += amt;
        if(CurrentMorale > MaxMorale) CurrentMorale = MaxMorale;
        //Check for change in health by caching amount?
        if (CurrentMorale <= 0)
        {
            Status = CharacterState.Helpless;
            CurrentMorale = 0;
            ActionPoints = 0;
            //TODO Send unit helpless notification
        }
    }
    
    private void SubscribeEvents()
    {
       
    }

    private void UnsubscribeEvents()
    {
        //EventBroker<SelectActionEvent>.OnEvent -= OnActionSelect;
        
    }
}
