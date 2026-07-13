using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    public enum GameState
    {
        Loading,
        Setup,
        PlayerTurn,
        GameOver
    }

    public enum GameOverState
    {
        
    }
    
    private GameState _currentState;
    
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private List<CharacterDef> characters;
    private List<Character> _playerTeam = new List<Character>();
    
    public Character ActiveCharacter {get; private set;}
    private int activeCharIndex = 0;
    public RoomManager activeRoom; //TODO manage getter and setter
    
    private void Awake()
    {
        _currentState = GameState.Loading;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ensure we are in the correct state before starting
        if (_currentState == GameState.Loading)
        {
            _currentState = GameState.Setup;
            StartLevel();
        }
    }

    private void StartLevel()
    {
        //Initialize Facility and grid
        SetupFacility();
        //Initialize characters
        SetupTeam();
        //Set first character
       
        ActiveCharacter = _playerTeam[activeCharIndex];
        _currentState = GameState.PlayerTurn;
        Debug.Log("Starting level");
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
        Debug.Log("Setup Facility");
        RoomManager startRoom = Game.Facility.Initialize();
        activeRoom = startRoom;
        startRoom.SetActiveRoomCamera(true);
    }
    
    
    
    private void SetupTeam()
    {
        Debug.Log("Setup Characters");
        if (activeRoom == null)
        {
            Debug.Log("No start room assigned");
            return;
        }
        
        for (int i = 0; i < characters.Count; i++)
        {
            Transform spawnPoint = activeRoom.PlayerPoint[i];
            CharacterDef charData = characters[i];
            
            GameObject characterObj = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
            Character character = characterObj.GetComponent<Character>();
            character.Initialize(charData);
            _playerTeam.Add(character);
        }

    }
    
    
}
