using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Upgrade
{
    public int count = 1;
    public int rank = 1;
}

[Serializable]
public class GameData 
{
    public int _maxHp = 300;
    public int _hp = 300;
    public int _attack = 4;
    public int _def = 0;
    
    public int _money;
    public string _name;
    public float _playTime;
    private bool _reset = false;
    
    private Define.ObjectType _type;

    public Upgrade AttackUpgrade;
    public Upgrade DefUpgrade;
    public Upgrade MaxHpUpgrade;
    
    public Define.ObjectType Type { get { return _type; } set { _type = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public int Money { get { return _money; } set { _money = value; } }
    public float PlayTime { get { return _playTime; } set { _playTime = value; } }
    
    // 초기화 판별용
    public bool Reset { get { return _reset; } set { _reset = value; } }
    
    #region 스텟
    public int MaxHp
    {
        get { return _maxHp; }
        set
        {
            if (MaxHpUpgrade.count * MaxHpUpgrade.rank == 1)
                _maxHp = value;
            else
                _maxHp = value + MaxHpUpgrade.count * MaxHpUpgrade.rank;
        }
    }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int Attack 
    {
        get { return _attack; }

        set
        {
            if (AttackUpgrade.count * AttackUpgrade.rank == 1)
                _attack = value;
            else
                _attack = value + AttackUpgrade.count * AttackUpgrade.rank;
        }
    }
    public int Def
    {
        get
        { return _def; }
        set
        {
            if (DefUpgrade.count * DefUpgrade.rank == 1)
                _def = value;
            else
                _def = value + DefUpgrade.count * DefUpgrade.rank;
        }
    }
    
    #endregion
    /// <summary>
    /// 업그레이드 돈 설정
    /// </summary>
    /// <param name="statType"></param>
    /// <returns></returns>
    public int SelectUpgradeCount(Define.StatType statType)
    {
        int result = 0;
        switch (statType)
        {
            case Define.StatType.MaxHp:
                if (MaxHpUpgrade.count != 1)
                    result = (int)Mathf.Ceil(MaxHpUpgrade.count * 1.1f);
                break;
            case Define.StatType.Attack:
                if (AttackUpgrade.count != 1)
                    result = (int)Mathf.Ceil(AttackUpgrade.count * 1.1f);
                break;
            case Define.StatType.Def:
                if (DefUpgrade.count != 1)
                    result = (int)Mathf.Ceil(DefUpgrade.count * 1.1f);
                break;
        }
        return result;
    }
    
    public int NextUpgradeInt(Define.StatType statType)
    {
        int result = 0;
        switch (statType)
        {
            case Define.StatType.MaxHp:
                result = _maxHp + (MaxHpUpgrade.count + 1) * MaxHpUpgrade.rank;
                break;
            case Define.StatType.Attack:
                result = _attack + (AttackUpgrade.count + 1) * AttackUpgrade.rank;
                break;
            case Define.StatType.Def:
                result = _def + (DefUpgrade.count + 1) * DefUpgrade.rank;
                break;
        }
        return result;
    }
    public void NextUpgrade(Define.StatType statType)
    {
        switch (statType)
        {
            case Define.StatType.MaxHp:
                MaxHpUpgrade.count++;
                MaxHp = _maxHp;
                break;
            case Define.StatType.Attack:
                AttackUpgrade.count++;
                Attack = _attack;
                break;
            case Define.StatType.Def:
                DefUpgrade.count++;
                Def = _def;
                break;
        }
    }
    public void ChangRank(Define.StatType statType, int rank)
    {
        switch (statType)
        {
            case Define.StatType.MaxHp:
                MaxHpUpgrade.rank = rank;
                break;
            case Define.StatType.Attack:
                AttackUpgrade.rank = rank;
                break;
            case Define.StatType.Def:
                DefUpgrade.rank = rank;
                break;
        }
    }
}
