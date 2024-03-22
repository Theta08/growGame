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
        GameDrawText
    }

    enum Buttons
    {
        StatButton,
        GameDraw,
    }
    
    enum AbilityItems
    {
        UI_AbilityItem_MaxHp,
        UI_AbilityItem_Attack, 
        UI_AbilityItem_Def,
    }
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        Bind<UI_AbilityItem>(typeof(AbilityItems));
        
        Setting();
        GetText((int)Texts.NameText).text = Managers.Game.SaveData.Name;
        
        GetButton((int)Buttons.StatButton).gameObject.BindEvent( () => Debug.Log($"스텟 버튼"));
        GetButton((int)Buttons.GameDraw).gameObject.BindEvent( () => Debug.Log($"뽑기 버튼"));
        
        Get<UI_AbilityItem>((int)AbilityItems.UI_AbilityItem_MaxHp).SetInfo(Define.StatType.MaxHp);
        Get<UI_AbilityItem>((int)AbilityItems.UI_AbilityItem_Attack).SetInfo(Define.StatType.Attack);
        Get<UI_AbilityItem>((int)AbilityItems.UI_AbilityItem_Def).SetInfo(Define.StatType.Def);
        
        RefreshUI();

        StartCoroutine(CoSave(3.0f));
        
        return true;
    }

    public void Update()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        // GetText((int)Texts.Timer).text = Managers.Game.PlayerName;
        GetText((int)Texts.Money).text = Managers.Game.SaveData.Money.ToString("D");
    }

    void OnTestClick()
    {
        Debug.Log("OnTestClick");
    }
    void Setting()
    {
        GameObject player = Managers.Game.Spawn(Define.ObjectType.Player, "Player/HeroKnight");
        
        Canvas canvas = gameObject.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        
        pool.SetKeepMonsterCount(1);
        
        player.transform.position = new Vector2(-1, 0);

        if(Managers.Game.SaveData.Reset)
            Managers.Game.SaveData.Reset = false;

        Managers.Game.LoadGame();
    }

    IEnumerator CoSave(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Managers.Game.SaveGame();
        }
    }
}
