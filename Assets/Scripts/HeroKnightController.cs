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
        
        _stat =  gameObject.GetOrAddComponent<PlayerStat>();
        _stat.Name = Managers.Game.PlayerName;
        
        State = Define.State.Idle;
        
        targetLayer = LayerMask.GetMask("Monster");
        Managers.Game.PlayerInfo = gameObject.GetComponent<HeroKnightController>();
        return true;
    }
    
    protected override void UpdateIdle()
    {
        // 스켄 후 플레이어 objcet 가져오기
        GetNearest();
        
        // 거리 계산 후 State값 공격 표시
        
        // int randomAttack = Random.Range(4, 7);
        // State = (Define.State)randomAttack;
    }

    protected override void UpdateAttck()
    {
        // State = Define.State.Idle;
    }

    void Attack1()
    {
        State = Define.State.Idle;
        // Debug.Log("attack1");
    }

    void GetNearest()
    {

        Collider2D targets = Physics2D.OverlapBox(transform.position, new Vector2(2,2), 0, targetLayer);

        if (targets == null)
            return;
        
        int randomAttack = Random.Range(4, 7);
        State = (Define.State)randomAttack;

    }
}


//Attack
// if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
// {
//     m_currentAttack++;
//
//     // Loop back to one after third attack
//     if (m_currentAttack > 3)
//         m_currentAttack = 1;
//
//     // Reset Attack combo if time since last attack is too large
//     if (m_timeSinceAttack > 1.0f)
//         m_currentAttack = 1;
//
//     // Call one of three attack animations "Attack1", "Attack2", "Attack3"
//     m_animator.SetTrigger("Attack" + m_currentAttack);
//
//     // Reset timer
//     m_timeSinceAttack = 0.0f;
// }
//
// // Block
// else if (Input.GetMouseButtonDown(1) && !m_rolling)
// {
//     m_animator.SetTrigger("Block");
//     m_animator.SetBool("IdleBlock", true);
// }
//
// else if (Input.GetMouseButtonUp(1))
//     m_animator.SetBool("IdleBlock", false);