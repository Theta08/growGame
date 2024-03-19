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
        // DiffText,
        // UpgradeText,
        // MoneyText
    }
    
    enum Buttons
    {
        UpgradeButton
    }

    Define.StatType _statType;
    
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

        // gameObject.GetOrAddComponent<UI_AbilityItem>();
        // int id = GetStatUpradId(_statType);
         
        RefreshUI();
    }
    
    public void RefreshUI()
    {
        if (_init == false)
            return;

        //_statType

        GetText((int)Texts.TitleText).text = $"{_statType.ToString()} 증가";
        GetText((int)Texts.ChangeText).text = $"Test 5 > 10";
        GetText((int)Texts.UpgradeMoneyText).text = $"Test 100";
        
        
        // data setting...

    }

    int GetStatUpradId(Define.StatType statType)
    {
        switch (_statType)
        {
            case Define.StatType.MaxHp:
                return 1;
            
        }

        return 0;
    }

    void OnUpgradeButton()
    {
        Debug.Log("OnUpgradeButton");

        if (Managers.Game.Money < 10)
            return;

        Managers.Game.Money -= 10;
    }
}
