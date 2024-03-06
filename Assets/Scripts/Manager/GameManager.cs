using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public string Name;

    public int Hp;
    public int MaxHp;
    public int Stress;

    public int Money;
    public float PlayTime;


 }

public class GameManager 
{
    GameData _gameData = new GameData();

    #region 스텟
    public string Name
    {
        get { return _gameData.Name; }
        set { _gameData.Name = value; }
    }
    
    public int Hp
    {
        get { return _gameData.Hp; }
        set { _gameData.Hp = value; }
    }
    
    public int MaxHp
    {
        get { return _gameData.MaxHp; }
        set { _gameData.MaxHp = value; }
    }
    
    public int Stress
    {
        get { return _gameData.Stress; }
        set { _gameData.Stress = value; }
    }
    #endregion

    #region 돈
    public int Money
    {
        get { return _gameData.Money; }
        set { _gameData.Money = value; }
    }
    #endregion

    #region 시간
    public float PlayTime
    {
        get { return _gameData.PlayTime; }
        set { _gameData.PlayTime = value; }
    }
    #endregion
    
    public void Init()
    {
        // 스텟 설정

        Name = "NoName";
        PlayTime = 0.0f;

        Hp = MaxHp;
    }
}
