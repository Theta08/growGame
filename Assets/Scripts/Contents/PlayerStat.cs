using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{

    [SerializeField] 
    private float _playTime;
    
    public Upgrade AttackUpgrade;
    public Upgrade DefUpgrade;
    public Upgrade MaxHpUpgrade;
    
    public float PlayTime { get { return _playTime; } set { _playTime = value; } }
    
    private void Start()
    {
        #region 초기 설정
        Upgrade upgrade = new Upgrade();
        upgrade.count = 1;
        upgrade.rank = 1;
        
        AttackUpgrade = upgrade;
        DefUpgrade = upgrade;
        MaxHpUpgrade = upgrade;
        
        MaxHp = 100;
        Hp = MaxHp;
        Attack = 4;
        PlayTime = 0.0f;
        Money = 0;
        #endregion

        GetPlayerStat(Managers.Game.SaveData);
    }

    public override void GetPlayerStat(GameData gameData)
    {
        // 세이브데이터X 경우
        if (gameData.Attack == 0 || gameData.Reset)
        {
            gameData.MaxHp = MaxHp;
            gameData.Hp = Hp;
            gameData.Attack = Attack;
            gameData.Def = Def;
            gameData.Money = 0;
            gameData.PlayTime = 0;
            
            Upgrade upgrade = new Upgrade{ count = 1, rank = 1 };

            gameData.AttackUpgrade = upgrade;
            gameData.DefUpgrade = upgrade;
            gameData.MaxHpUpgrade = upgrade;
        }
        else
        {
            MaxHp = gameData.MaxHp;
            Hp = gameData.Hp;
            Attack = gameData.Attack;
            Def = gameData.Def;
        
            Name = gameData.Name;
            Money = gameData.Money;
            PlayTime = gameData.PlayTime;

            AttackUpgrade = gameData.AttackUpgrade;
            DefUpgrade = gameData.DefUpgrade;;
            MaxHpUpgrade = gameData.MaxHpUpgrade;;
        }

        Managers.Game.SaveGame();
    }
}
