using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DrawAbilityItem : UI_Base
{
    enum Texts
    {
        TitleText,
        ChangeText,
    }
    enum Buttons { }
    enum Images
    {
        UI_AbilityItem,
        Icon,
    }

    Define.StatType _statType;
    private int _rank = 1;
    private Sprite _icon;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        Debug.Log("UI_DrawAbilityItem");

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        
        gameObject.BindEvent(OnUpgradeButton);
        
        // RefreshUI();
        
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
        
        // GetChangeRank();

        GetText((int)Texts.TitleText).text = $"{_statType.ToString()} 증가";
        GetText((int)Texts.ChangeText).text =$"{GetChangeRank()}" ;

        OnImgSetting();
        GetImage((int)Images.Icon).sprite = _icon;

    }

    void OnImgSetting()
    {
        string name = "";
        string ColorStr = "";
        Color color;
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/UI/UI");
        
        switch (_statType)
        {
            case Define.StatType.MaxHp:
                sprites = Resources.LoadAll<Sprite>("Sprites/UI/Props");
                name = "Health";
                break;
            case Define.StatType.Attack:
                name = "Select 0";
                break;
            case Define.StatType.Def:
                name = "Select 6";
                break;
        }
        
        foreach (var sprite in sprites)
        {
            if (sprite.name == name)
                _icon = sprite;
        }
        
        if (_rank == 1)
            // 하얀색에 가까운 주황색
            ColorStr = "#F2BD85";
        else if( _rank == 2)
            // 주황색
            ColorStr = "#FF9600";
        else if( _rank == 3)
            // 금색
            ColorStr = "#FFBC00";
        
        ColorUtility.TryParseHtmlString(ColorStr, out color);
        gameObject.GetComponent<Image>().color = color;
    }
    
    string GetChangeRank()
    {
        _rank = Random.Range(1, 4);
        string currenRank = "";
        
        switch (_statType)
        {
            case Define.StatType.MaxHp:
                currenRank = Managers.Game.SaveData.MaxHpUpgrade.rank.ToString();
                break;
            case Define.StatType.Attack:
                currenRank = Managers.Game.SaveData.AttackUpgrade.rank.ToString();
                break;
            case Define.StatType.Def:
                currenRank = Managers.Game.SaveData.DefUpgrade.rank.ToString();
                break;
        }
        
       return $"{currenRank} > {_rank}";
    }

    public void OnUpgradeButton()
    {
        Debug.Log("OnUpgradeButton");
        // 스텟 랭크 적용 
        Managers.Game.SaveData.ChangRank(_statType, _rank);
        // 종료
        Managers.Game.RefreshPlayerData();
    }
}
