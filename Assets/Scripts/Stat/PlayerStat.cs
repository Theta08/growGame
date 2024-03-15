using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{

    // [SerializeField] 
    // private int _money;
    [SerializeField] 
    private float _playTime;
    
    #region Stat GetSet
    
    // public int Money { get { return _money; } set { _money = value; } }
    public float PlayTime { get { return _playTime; } set { _playTime = value; } }
    
    #endregion
    
    private void Start()
    {
        _hp = 30;
        _maxHp = _hp;
        _attack = 3;
        // _name = "";
        _playTime = 0.0f;
        Money = Managers.Game.Money;
    }
}
