using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayPopup : UI_Popup
{

    enum GameObjects
    {
        
    }
    
    enum Texts
    {
        NameText,
        Timer,
        Money,
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindText(typeof(Texts));

        GameObject go = Managers.Resource.Instantiate("Player/HeroKnight");
        GameObject go1 = Managers.Resource.Instantiate("Enemies/Goblin");
        go.transform.position = new Vector2(-1, 0);
        
        RefreshUI();
        
        return true;
    }

    public void Update()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        GetText((int)Texts.NameText).text = Managers.Game.PlayerName;
        // GetText((int)Texts.Timer).text = Managers.Game.PlayerName;
        GetText((int)Texts.Money).text = Managers.Game.Money.ToString("D");

    }
}
