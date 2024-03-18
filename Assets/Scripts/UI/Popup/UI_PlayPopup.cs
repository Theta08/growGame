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

        GameObject player = Managers.Game.Spawn(Define.ObjectType.Player, "Player/HeroKnight");
        
        // GameObject go1 = Managers.Resource.Instantiate("Enemies/Goblin");

        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        
        pool.SetKeepMonsterCount(1);
        
        player.transform.position = new Vector2(-1, 0);
        
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
