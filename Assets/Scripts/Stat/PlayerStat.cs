using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{

    [SerializeField] 
    public int _money;
    [SerializeField] 
    public float _playTime;
    
    #region Stat GetSet
    

    public int Money { get { return _money; } set { _money = value; } }
    public float PlayTime { get { return _playTime; } set { _playTime = value; } }
    
    #endregion
    
    private void Start()
    {
        _hp = 40;
        _maxHp = _hp;
        _attack = 10;
        _name = "playerName";
        _playTime = 0.0f;
    }
}
