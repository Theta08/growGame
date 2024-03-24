using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DrawPopup : UI_Popup
{
    enum GameObjects
    {
        UI_DrawAbilityItem1,
        UI_DrawAbilityItem2,
    }
    enum Texts
    {
        TileText,
        DrawText,
        CancelText,
    }
    enum Buttons
    {
        UI_DrawAbilityItem1,
        UI_DrawAbilityItem2,
        CancelButton,
    }

    private Define.StatType[] _type;
    private int _rank;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        Bind<UI_DrawAbilityItem>(typeof(GameObjects));
        
        GetButton((int)Buttons.CancelButton).gameObject.BindEvent(
            () => Managers.UI.ClosePopupUI(this));
        GetButton((int)Buttons.UI_DrawAbilityItem1).gameObject.BindEvent(OnDrawPopupClose);
        GetButton((int)Buttons.UI_DrawAbilityItem2).gameObject.BindEvent(OnDrawPopupClose);
        
        Setting();
        Get<UI_DrawAbilityItem>((int)GameObjects.UI_DrawAbilityItem1).SetInfo(_type[0]);
        Get<UI_DrawAbilityItem>((int)GameObjects.UI_DrawAbilityItem2).SetInfo(_type[1]);
        
        return true;
    }

    void Setting()
    {

        _type = new Define.StatType[2];
        
        // 랜덤 가챠 설정 
        for (int i = 0; i < _type.Length; i++)
        {
            int type = Random.Range(0, 3);
            switch (type)
            {
                case 0: _type[i] = Define.StatType.MaxHp;
                    break;
                case 1: _type[i] = Define.StatType.Attack;
                    break;
                case 2: _type[i] = Define.StatType.Def;
                    break;
            }
        }

    }

    void OnDrawPopupClose()
    {
        Debug.Log("OnDrawAbilityItem");
        
        GameObject go = GameObject.Find("UI_PlayPopup");
        go.GetComponent<UI_PlayPopup>().RefreshUI();
        
        Managers.UI.ClosePopupUI(this);
    }
}
