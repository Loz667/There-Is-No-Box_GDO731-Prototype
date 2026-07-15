using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    public readonly int maxActions = 3;
    
    public enum GameState
    {
        Loading,
        Setup,
        StartRound,
        EndRound,
        StartTurn,
        PlayerTurn,
        AITurn,
        EndTurn,
        GameOver
        
    }

    public enum GameOverState
    {
        
    }
    
    private GameState _currentState;
    
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private List<CharacterDef> characters;
    private List<Character> _playerTeam = new List<Character>();

    private int _doomTrackLength = 10;
    private int _failureCount = 0;
    
    public int FailureCount => _failureCount;
    public int FailureTrack => _doomTrackLength;
    
    public Character ActiveCharacter {get; private set;}
    public bool IsPlayerTurn { get; private set; } 
    public int RoundNumber { get; private set; }
    
    private int activeCharIndex = 0;
    public RoomManager activeRoom; //TODO manage getter and setter
    
    public int CharacterIndex => activeCharIndex; 
    
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
        SetupContainmentTrack();
       
        //TODO Wrap this in a function
        ActiveCharacter = _playerTeam[activeCharIndex];
        ActiveCharacter.IsActive(true);
        
        //_currentState = GameState.PlayerTurn;
        Debug.Log("Starting level");
        StartNewRound();
    }


    private void StartNewRound()
    {
        _currentState = GameState.StartRound;
        RoundNumber++;
        Debug.Log("Starting Round " + RoundNumber);
        IsPlayerTurn = true;
        activeCharIndex = 0;
        
        StartNewTurn();
    }

    private void EndRound()
    {
        //Do stuff at the end of the round
        _currentState = GameState.EndRound;
        Debug.Log("Do End of Round type stuff");
        _failureCount++;
        EventBroker<FailureTrackAdvanceEvent>.Broadcast(new FailureTrackAdvanceEvent());
        StartNewRound();
    }


    private async void StartNewTurn()
    {
        _currentState = GameState.StartTurn;
        EventBroker<TurnStartEvent>.Broadcast(new TurnStartEvent());
        if (IsPlayerTurn)
        {
            GetNextActiveCharacter();
            ActiveCharacter.IsActive(true);
            Game.UI.playerHUD.UpdateCharacterView(ActiveCharacter);
            FocusOnCharacter();
           
        }
        else
        {
            Debug.Log("### Starting AI turn ###");
            await Task.Delay(3000);
            EndTurn();
        }

        
       
    }

    public void TriggerTurnEnd()
    {
        Debug.Log("Trigger turn end");
        EndTurn();
    }
    
    private void EndTurn()
    {
        _currentState = GameState.EndTurn;
        if (IsPlayerTurn)
        {
            if (ActiveCharacter != null)
            {
                ActiveCharacter.IsActive(false);
            }
        }
        else
        {
            Debug.Log("### Ending AI turn ###");
        }
        
        IsPlayerTurn = !IsPlayerTurn;

        if (IsPlayerTurn)
        {
            activeCharIndex++;
            Debug.Log("AC Index: " + activeCharIndex + " Team count: " + _playerTeam.Count);
            if (activeCharIndex >= _playerTeam.Count)
            {
                Debug.Log("EndTurn:: Ending Round...");
                EndRound();
            }
        }
        
        StartNewTurn();
    }
    
    
    
    private void GetNextActiveCharacter()
    {
        /*  
        activeCharIndex++;
        if (activeCharIndex > characters.Count)
        {
           
        }
        */
        bool anyAliveChars = false;
        for (int i = 0; i < _playerTeam.Count; i++)
        {
            if (_playerTeam[(activeCharIndex + i) % _playerTeam.Count].Status == Character.CharacterState.Alive)
            {
                activeCharIndex = (activeCharIndex + i) % _playerTeam.Count;
                ActiveCharacter = _playerTeam[activeCharIndex];
                anyAliveChars = true;
                break;
            }
        }

        if (!anyAliveChars)
        {
            _currentState = GameState.GameOver;
            //TODO Game over man, Game over!
        }
        
        //TODO switch camera to focus on where character is
    }

    private async Task FocusOnCharacter()
    {
        RoomManager nextRoom = ActiveCharacter.CurrentRoom;
        if (activeRoom != null && activeRoom == nextRoom) return;
        await ScreenFader.Instance.FadeOut();
        RoomManager oldRoom = activeRoom;
        oldRoom.SetActiveRoomCamera(false);
        activeRoom = nextRoom;
        activeRoom.SetActiveRoomCamera(true);
       
        await Task.Delay(500);
        await ScreenFader.Instance.FadeIn();
        
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
            character.EnterRoom(activeRoom);
            _playerTeam.Add(character);
            
        }

    }

    private void SetupContainmentTrack()
    {
        Game.UI.playerHUD.Initialize();
    }
    
    
}
