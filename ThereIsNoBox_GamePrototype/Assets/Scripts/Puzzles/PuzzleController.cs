using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    
    void OnEnable() => SubscribeEvents();
    void OnDisable() => UnsubscribeEvents();
    

    public void RollDice()
    {
        
    }


    private void OnDiceStateChanged(DiceStateChangedEvent ev)
    {
        //Do something
    }

    private void OnDiceResultChanged(DiceResultChangeEvent ev)
    {
        DiceEnums.DieResult result = ev.NewResult;
    }


    private void SubscribeEvents()
    {
        EventBroker<DiceStateChangedEvent>.OnEvent += OnDiceStateChanged;
        EventBroker<DiceResultChangeEvent>.OnEvent += OnDiceResultChanged;
    }
    
    private void UnsubscribeEvents()
    {
        EventBroker<DiceStateChangedEvent>.OnEvent -= OnDiceStateChanged;
        EventBroker<DiceResultChangeEvent>.OnEvent -= OnDiceResultChanged;
    }

}
