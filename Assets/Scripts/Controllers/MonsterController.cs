using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        ObjectType = Define.ObjectType.Monster;
        targetLayer = LayerMask.GetMask("Player");
        
        _stat =  gameObject.GetOrAddComponent<Stat>();
        _stat.Type = ObjectType;
        
        State = Define.State.Idle;
        
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpace<UI_HPBar>(transform);
        
        return true;
    }
}
