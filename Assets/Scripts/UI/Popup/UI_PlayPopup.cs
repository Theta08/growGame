using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayPopup : UI_Popup
{

    enum GameObjects { }
    enum Texts
    {
        NameText,
        Timer,
        Money,
        MoneyDrawText
    }
    enum Buttons
    {
        GameDraw,
    }
    enum AbilityItems
    {
        UI_AbilityItem_MaxHp,
        UI_AbilityItem_Attack, 
        UI_AbilityItem_Def,
    }

    private int _draw;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        Bind<UI_AbilityItem>(typeof(AbilityItems));
        
        Setting();
        GetText((int)Texts.NameText).text = Managers.Game.SaveData.Name;
        GetText((int)Texts.MoneyDrawText).text = _draw.ToString();
        
        GetButton((int)Buttons.GameDraw).gameObject.BindEvent(OnDrawButtion);
        
        RefreshUI();
        Managers.Game.Resume();
        
        StartCoroutine(CoSave(3.0f));
        
        return true;
    }

    public void Update()
    {
        if (!Managers.Game.IsLive)
            return;
        
        float remainTime = Managers.Game.SaveData.PlayTime += Time.deltaTime;
        
        GetText((int)Texts.Money).text = Managers.Game.SaveData.Money.ToString("D");
        GetText((int)Texts.Timer).text = Managers.Game.GetPlayerTimer(remainTime);
    }

    public void RefreshUI()
    {
        Get<UI_AbilityItem>((int)AbilityItems.UI_AbilityItem_MaxHp).SetInfo(Define.StatType.MaxHp);
        Get<UI_AbilityItem>((int)AbilityItems.UI_AbilityItem_Attack).SetInfo(Define.StatType.Attack);
        Get<UI_AbilityItem>((int)AbilityItems.UI_AbilityItem_Def).SetInfo(Define.StatType.Def);
        
        GetText((int)Texts.Money).text = Managers.Game.SaveData.Money.ToString("D");
    }

    void OnDrawButtion()
    {
        if (Managers.Game.SaveData.Money < _draw)
            return;
        
        Managers.Game.SaveData.Money -= _draw;
        Managers.Sound.Play(Define.Sound.Effect, "Sound_Draw");
        Managers.UI.ShowPopupUI<UI_DrawPopup>();
    }
    void Setting()
    {
        
        // 켄버스 변경 : GameObject 가려서 변경함
        Canvas canvas = gameObject.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        
        GameObject player = Managers.Game.Spawn(Define.ObjectType.Player, "Player/HeroKnight");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(1);
        
        player.transform.position = new Vector2(-1, -0.4f);
        _draw = 20;
        
        Managers.Game.LoadGame();

        if(Managers.Game.SaveData.Reset)
            Managers.Game.SaveData.Reset = false;
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
