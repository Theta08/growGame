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
        _stat.Name = Managers.Game.PlayerName;
        _stat.Type = ObjectType;
        
        State = Define.State.Idle;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        
        Managers.Game.PlayerInfo = gameObject.GetComponent<HeroKnightController>();
        
        return true;
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
