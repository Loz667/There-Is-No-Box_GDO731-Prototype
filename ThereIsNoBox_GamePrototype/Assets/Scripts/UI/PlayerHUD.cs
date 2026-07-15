using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to update main player view
/// </summary>
public class PlayerHUD: UIPanel
{

    [SerializeField] private ActiveCharacterUI characterView;
    [SerializeField] private FailureTrackUI containmentView;
    
    private void OnEnable() => SubscribeEvents();
    private void OnDisable() => UnsubscribeEvents();
    
    
    public void UpdateCharacterView(Character character)
    {
        characterView.SetCharacter(character);
    }

    public void Initialize()
    {
        containmentView.Initialize(Game.Director.FailureTrack); //TODO pass this in from 
    }

    private void UpdateFailureTrack(FailureTrackAdvanceEvent e)
    {
        containmentView.Redraw(Game.Director.FailureCount);
    }

    private void OnTurnStart(TurnStartEvent e)
    {
        if (Game.Director.IsPlayerTurn)
        {
            characterView.Show();
        }
        else
        {
            characterView.Hide();
        }
    }
    
    
    private void SubscribeEvents()
    {
        EventBroker<TurnStartEvent>.OnEvent += OnTurnStart;
        EventBroker<FailureTrackAdvanceEvent>.OnEvent += UpdateFailureTrack;
    }

    private void UnsubscribeEvents()
    {
        EventBroker<TurnStartEvent>.OnEvent -= OnTurnStart;
        EventBroker<FailureTrackAdvanceEvent>.OnEvent -= UpdateFailureTrack;
    }
    
}