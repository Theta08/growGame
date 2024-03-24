using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitlePopup : UI_Popup
{
    enum Texts
    {
        StartButtonText,
        StartButtonText2,
        ExitButtonText,
    }

    enum Buttons
    {
        StartButton,
        StartButton2,
    }

    private GameObject knight;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        Debug.Log("UI_TitlePopup");
        
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickStartButton);
        GetButton((int)Buttons.StartButton2).gameObject.BindEvent(OnClickLoadButton);
        GetButton((int)Buttons.StartButton2).gameObject.BindEvent(OnExitButton);
        
        GetText((int)Texts.StartButtonText).text = "새로하기";
        
        Managers.Sound.Clear();
        Managers.Sound.Play(Define.Sound.Bgm, "Sound_MainTitle", 0.25f);

        if (Managers.Game.LoadGame() && Managers.Game.SaveData.Reset == false)
        {
            GetButton((int)Buttons.StartButton2).gameObject.SetActive(true);
            GetText((int)Texts.StartButtonText2).text = "불러오기";
        }
        else
            GetButton((int)Buttons.StartButton2).gameObject.SetActive(false);

        // 켄버스 변경 : GameObject 가려서 변경함
        Canvas canvas = gameObject.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;

        knight = Managers.Game.Spawn(Define.ObjectType.Player, "Player/Knight");
        knight.transform.position = new Vector3(0, 0.75f, 0);

        return true;
    }
 
    void OnClickStartButton()
    {
        Managers.Game.Despawn(knight);
        Managers.Sound.Play(Define.Sound.Effect, "Sound_MainButton");
        
        Managers.Game.Init();
        Managers.Game.SaveData.Reset = true;
        
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_NamePopup>();
    }
    
    void OnClickLoadButton()
    {
        Managers.Game.Despawn(knight);
        Managers.Sound.Play(Define.Sound.Effect, "Sound_MainButton");
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
    }
    
    void OnExitButton()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Sound_Select");
        Application.Quit();
    }
}
