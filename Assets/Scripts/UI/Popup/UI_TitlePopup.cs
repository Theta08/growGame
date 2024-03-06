using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TitlePopup : UI_Popup
{
    enum Texts
    {
        StartButtonText,
        StartButtonText2,
    }

    enum Buttons
    {
        StartButton,
        StartButton2,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        Debug.Log("UI_TitlePopup");
        
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickStartButton);
        GetButton((int)Buttons.StartButton2).gameObject.BindEvent(OnClickStartButton2);

        GetText((int)Texts.StartButtonText).text = "시작하기";
        GetText((int)Texts.StartButtonText2).text = "시작하기2";

        return true;
    }
 
    void OnClickStartButton()
    {
        Debug.Log("OnClickStartButton");
        
        // TODO 게임저장 시스템 구현 해야함
        // if(Managers.Game.LoadGame())
        
        Managers.Game.Init();
        
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_NamePopup>();
    }
    
    void OnClickStartButton2()
    {
        Debug.Log("OnClickStartButton2");
    }
}
