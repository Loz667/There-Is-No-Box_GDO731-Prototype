using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{

    public enum GameState
    {
        
    }

    public enum GameOverState
    {
        
    }
    
    private GameState _currentState;
    
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private List<CharacterDef> characters;
    private List<Character> _playerTeam;
    
    public Character ActiveCharacter {get; private set;}
    private int activeCharIndex = 0;
    public RoomManager ActiveRoom; //TODO manage getter and setter


    private void StartGame()
    {
        //Initialize Facility and grid
        SetupFacility();
        //Initialize characters
        
        //Set first character
        SetupTeam();
        ActiveCharacter = _playerTeam[activeCharIndex];
        
    }


    private void NextCharacter()
    {
            activeCharIndex++;
            if(activeCharIndex > characters.Count) activeCharIndex = 0;
            ActiveCharacter = _playerTeam[activeCharIndex]; //TODO Check for status. 
            //TODO switch camera to focus on where character is
    }

    private void SetupFacility()
    {
        RoomManager startRoom = Game.Facility.Initialize();
        ActiveRoom = startRoom;
    }
    
    
    
    private void SetupTeam()
    {
        foreach (CharacterDef character in characters)
        {
            
        }
            
    }
    
    
}
