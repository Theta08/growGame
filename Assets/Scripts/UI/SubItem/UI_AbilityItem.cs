using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    enum Images
    {
        UI_AbilityItem,
        Icon,
    }

    Define.StatType _statType;
    private Sprite _icon;
    private int _pay = 10;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        Debug.Log("UI_AbilityItem");

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        
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
        OnImgSetting();

        GetText((int)Texts.TitleText).text = $"{_statType.ToString()} 증가";
        GetText((int)Texts.ChangeText).text =$"{GetUpradeSetting()}" ;
        GetText((int)Texts.UpgradeMoneyText).text = $"{_pay}";

        GetImage((int)Images.Icon).sprite = _icon;

    }

    void OnImgSetting()
    {
        string name = "";
        string ColorStr = "";
        int rank = 1;
        Color color;
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/UI/UI");
        
        switch (_statType)
        {
            case Define.StatType.MaxHp:
                sprites = Resources.LoadAll<Sprite>("Sprites/UI/Props");
                rank = Managers.Game.SaveData.MaxHpUpgrade.rank;
                name = "Health";
                break;
            case Define.StatType.Attack:
                rank = Managers.Game.SaveData.AttackUpgrade.rank;
                name = "Select 0";
                break;
            case Define.StatType.Def:
                rank = Managers.Game.SaveData.DefUpgrade.rank;
                name = "Select 6";
                break;
        }
        
        foreach (var sprite in sprites)
        {
            if (sprite.name == name)
                _icon = sprite;
        }
        
        if (rank == 1)
            // 하얀색에 가까운 주황색
            ColorStr = "#F2BD85";
        else if( rank == 2)
            // 주황색
            ColorStr = "#FF9600";
        else if( rank == 3)
            // 금색
            ColorStr = "#FFBC00";
        
        ColorUtility.TryParseHtmlString(ColorStr, out color);
        gameObject.GetComponent<Image>().color = color;
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
