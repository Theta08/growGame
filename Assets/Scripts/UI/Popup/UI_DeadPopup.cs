using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DeadPopup : UI_Popup
{
    enum Buttons
    {
        TitleButton,
        ExitButton,
    }
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.TitleButton).gameObject.BindEvent(OnTitleButton);
        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(OnExitButton);
        
        Managers.Game.Stop();
        Managers.Game.SaveData.Reset = true;
        
        return true;
    }

    void OnTitleButton()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Sound_Select");
        Managers.UI.ClosePopupUI(this);
        
        GameObject player = GameObject.Find("HeroKnight");
        GameObject enemie = GameObject.Find("Goblin");
        
        SpawningPool pool = GameObject.Find("SpawningPool").GetComponent<SpawningPool>();
        
        pool.SetKeepMonsterCount(0);
        
        Managers.Game.Despawn(player);
        Managers.Game.Despawn(enemie);

        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_TitlePopup>();
    }
    
    void OnExitButton()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Sound_Select");
        Managers.Game.SaveGame();
        
        Managers.UI.CloseAllPopupUI();
        
        Application.Quit();
    }
}
