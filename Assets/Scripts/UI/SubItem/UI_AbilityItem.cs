using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AbilityItem : UI_Base
{
    enum Texts
    {
        TitleText,
        ChangeText,
        UpgradeMoneyText,
    }
    
    enum Buttons
    {
        UpgradeButton
    }

    Define.StatType _statType;
    private int _pay = 10;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        Debug.Log("UI_AbilityItem");

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.UpgradeButton).gameObject.BindEvent(OnUpgradeButton);
        
        RefreshUI();
        
        return true;
    }
    
    public void SetInfo(Define.StatType statType)
    {
        _statType = statType;
        RefreshUI();
    }
    
    public void RefreshUI()
    {
        if (_init == false)
            return;

        _pay += Managers.Game.SaveData.SelectUpgradeCount(_statType);
        
        GetUpradeSetting();

        GetText((int)Texts.TitleText).text = $"{_statType.ToString()} 증가";
        GetText((int)Texts.ChangeText).text =$"{GetUpradeSetting()}" ;
        GetText((int)Texts.UpgradeMoneyText).text = $"{_pay}";

    }

    string GetUpradeSetting()
    {
        string currentStat = "";
        switch (_statType)
        {
            case Define.StatType.MaxHp:
                currentStat = Managers.Game.SaveData.MaxHp.ToString();
                break;
            case Define.StatType.Attack:
                currentStat = Managers.Game.SaveData.Attack.ToString();
                break;
            case Define.StatType.Def:
                currentStat = Managers.Game.SaveData.Def.ToString();
                break;
        }
        
       return $"{currentStat} > " +
              $"{Managers.Game.SaveData.NextUpgradeInt(_statType).ToString()}";
    }

    void OnUpgradeButton()
    {
        Debug.Log("OnUpgradeButton");

        if (Managers.Game.SaveData.Money < _pay)
        {
            Debug.Log($"돈 {_pay - Managers.Game.SaveData.Money} 부족합니다.");
            return;
        }
        
        Managers.Game.SaveData.Money -= _pay;
        Managers.Game.SaveData.NextUpgrade(_statType);
        Managers.Game.RefreshPlayerData();
        
        _pay += Managers.Game.SaveData.SelectUpgradeCount(_statType);
        
        RefreshUI();
    }
}
