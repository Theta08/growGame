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
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindText(typeof(Texts));
        
        GameObject go = Managers.Resource.Instantiate("Player/HeroKnight");
        go.transform.position = new Vector2(-1, 0);
        
        RefreshUI();
        
        return true;
    }
    
    void RefreshUI()
    {
        GetText((int)Texts.NameText).text = Managers.Game.PlayerName;
    }
}
