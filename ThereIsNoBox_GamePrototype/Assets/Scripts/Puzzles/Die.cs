using UnityEngine;
public class Die
{
    private DieDefinition _def;

    private DieFace _rolledFace;
    private DiceEnums.DieResult _curResult;
    private DiceEnums.RollType _curType;
    private DiceEnums.DieState _curState;
    
    //Type?

    public DiceEnums.DieResult RollResult
    {
        get
        {if(_rolledFace != null) return _rolledFace.rollResult;
            return DiceEnums.DieResult.EMPTY;
        }
    }
    
    public DiceEnums.RollType RollType
    {
        get
        {if(_rolledFace != null) return _rolledFace.rollType;
            return DiceEnums.RollType.NONE;
        }
    }

    public Sprite rollIcon
    {
        get
        {
            if (_rolledFace != null)
            {
                return _rolledFace.image;
            }
            return null;
        }
    }
    
    
    /*
    {
        get => _rolledFace.resultType; //_curResult;
        set
        {
            //if (_curResult != value)
            if (_rolledFace != value)
            {
                _curResult = value;
                ResultChanged();
            }
        }
       
    }
    */

    public DiceEnums.DieState State
    {
        get => _curState;
        set
        {
            if (_curState != value)
            {
                _curState = value;
                StateChanged();
            }
        }
    }

    public Die(DieDefinition _newDef)
    {
        _def = _newDef;
        _curState = DiceEnums.DieState.Available;
        _curResult = DiceEnums.DieResult.EMPTY;
        _curType = DiceEnums.RollType.NONE;
        _rolledFace = null;
    }

    public void Roll()
    {
        Debug.Log("Rolling Die");
        _rolledFace = _def.GetRoll();
        Debug.Log("RolledFace = " + _rolledFace.rollResult);
        _curState = DiceEnums.DieState.Rolling;
    }

    public void ModifyResult(DieFace newResult)
    {
        if (_rolledFace != newResult)
        {
            _rolledFace = newResult;
            ResultChanged();
        }
    }
    
    public Color GetColor()
    {
        return _def.dieColor;
    }
    //Roll to trigger an event here?
    
    private void ResultChanged()
    {
        // Update UI icon to reflect change
        EventBroker<DiceResultChangeEvent>.Broadcast(new DiceResultChangeEvent(_curResult));
    }

    private void StateChanged()
    {
        //Fire DieStateChanged Event
        EventBroker<DiceStateChangedEvent>.Broadcast(new DiceStateChangedEvent(_curState));
    }
    
}
