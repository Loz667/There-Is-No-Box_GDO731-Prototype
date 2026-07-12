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
    
    
    private List<CharacterDef> _characters;
    
    private Character activeCharacter;
    
    
}
