using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class HeroKnightController : BaseController 
{
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        ObjectType = Define.ObjectType.Player;
        targetLayer = LayerMask.GetMask("Monster");
        
        _stat =  gameObject.GetOrAddComponent<PlayerStat>();
        _stat.Name = Managers.Game.SaveData.Name;
        
        // TODO save시 값 받아와야함
        _stat.Type = ObjectType;

        State = Define.State.Idle;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpace<UI_HPBar>(transform);
        
        Managers.Game.GetPlayer = gameObject;
        
        return true;
    }

    public void RefreshStat()
    {
        _stat.GetPlayerStat(Managers.Game.SaveData);
        gameObject.GetComponentInChildren<UI_HPBar>().RefeshUI();
    }
    
    protected override void UpdateIdle()
    {
        // 스켄 후 objcet 가져오기
        GetNearest();
    }

    protected override void UpdateAttck()
    {
        // Debug.Log("UpdateAttack");
        // State = Define.State.Idle;
    }


    
}
