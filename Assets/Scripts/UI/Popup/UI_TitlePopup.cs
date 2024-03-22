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
        GetButton((int)Buttons.StartButton2).gameObject.BindEvent(OnClickLoadButton);
        
        GetText((int)Texts.StartButtonText).text = "새로하기";
        
        Managers.Sound.Clear();
        Managers.Sound.Play(Define.Sound.Bgm, "Sound_MainTitle", 0.25f);

        if (Managers.Game.LoadGame())
        {
            GetButton((int)Buttons.StartButton2).gameObject.SetActive(true);
            GetText((int)Texts.StartButtonText2).text = "불러오기";
        }
        else
            GetButton((int)Buttons.StartButton2).gameObject.SetActive(false);

        return true;
    }
 
    void OnClickStartButton()
    {
        Debug.Log("OnClickStartButton");

        Managers.Sound.Play(Define.Sound.Effect, "Sound_MainButton");
        
        Managers.Game.Init();
        Managers.Game.SaveData.Reset = true;
        
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_NamePopup>();
    }
    
    void OnClickLoadButton()
    {
        Debug.Log("OnClickLoadButton");
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
    }
}
