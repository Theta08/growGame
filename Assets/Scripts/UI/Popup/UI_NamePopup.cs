using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_NamePopup : UI_Popup
{
    enum GameObjects
    {
        InputField,
    }

    enum Texts
    {
        NameText,
        HintText,
    }

    enum Buttons
    {
        ConfirmButton
    }

    private TMP_InputField _inputField;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.ConfirmButton).gameObject.BindEvent(OnClickConfirmButton);

        _inputField = GetObject((int)GameObjects.InputField).gameObject.GetOrAddComponent<TMP_InputField>();
        _inputField.text = "";

        RefreshUI();
        
        return true;
    }

    void RefreshUI()
    {
        GetText((int)Texts.NameText).text = "";
        GetText((int)Texts.HintText).text = "이름입력";
    }
    
    void OnClickConfirmButton()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Sound_MainButton");
        
        Debug.Log("ConfirmButton 실행!");
        Managers.Game.SaveData.Name = _inputField.text;
        
        // 초기화
        Managers.Game.SaveData.AttackUpgrade = new Upgrade { rank = 1, count = 1 };
        Managers.Game.SaveData.DefUpgrade = new Upgrade { rank = 1, count = 1 };
        Managers.Game.SaveData.MaxHpUpgrade = new Upgrade { rank = 1, count = 1 };
        Managers.Game.SaveData.Money = 0;
        Managers.Game.SaveData.Attack = 4;
        Managers.Game.SaveData.MaxHp = 300;
        Managers.Game.SaveData.Hp = 300;
        Managers.Game.SaveData.Def = 0;
        Managers.Game.SaveData.PlayTime = 0.0f;
            
        Managers.Game.SaveGame();
        
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
    }
}
