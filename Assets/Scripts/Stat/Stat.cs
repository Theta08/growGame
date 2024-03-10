using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat: MonoBehaviour
{
    protected int _hp;
    protected int _attack;
    protected int _maxHp;
    [SerializeField] 
    public string _name;
    
    public string Name { get { return _name; } set { _name = value; } }
    #region 스텟
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    #endregion
    
    private void Start()
    {
        _hp = 10;
        _maxHp = _hp;
        _attack = 3;
    }
}
