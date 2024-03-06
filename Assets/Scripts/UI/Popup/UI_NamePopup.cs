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
        Debug.Log("ConfirmButton 실행!");

        Managers.Game.Name = _inputField.text;
        
        // Managers.UI.ClosePopupUI(this);
        // Managers.UI.ShowPopupUI<UI_NamePopup>();
    }
}
