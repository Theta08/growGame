using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public abstract class BaseController : MonoBehaviour
{
    // [SerializeField]
    // protected Vector3 _destPos;
    [SerializeField]
    protected GameObject _lockTarget;
    [SerializeField]
    protected LayerMask targetLayer;
    [SerializeField]
    protected Define.State _state = Define.State.Idle;
    [SerializeField]
    protected Stat _stat;
 
    protected bool _init = false;
    public Define.ObjectType ObjectType { get; protected set; } = Define.ObjectType.Unknown;

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            
            Animator anim = GetComponent<Animator>();

            switch (_state)
            {
                case Define.State.Idle:
                    anim.CrossFade("Idle",0.1f);
                    break;
                case Define.State.Attack1:
                    anim.CrossFade("Attack1", 0.1f, -1, 0);
                    break;
                case Define.State.Attack2:
                    anim.CrossFade("Attack2", 0.1f, -1, 0);
                    break;
                case Define.State.Attack3:
                    anim.CrossFade("Attack3", 0.1f, -1, 0);
                    break;
                case Define.State.Die:
                    anim.CrossFade("Die", 0.1f);
                    break;
            }
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (State)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Attack1:
            case Define.State.Attack2:
            case Define.State.Attack3:
                UpdateAttck();
                break;
            case Define.State.Die:
                UpdateDie();
                break;
        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateAttck() { }
    protected virtual void UpdateDie() { }

    public virtual bool Init()
    {
        if (_init)
            return false;

        return _init = true;
    }

    public Stat Stat
    {
        get { return _stat; }
        set { _stat = value; }
    }
    
    protected void GetNearest()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, new Vector2(2,2), 0, targetLayer);
        
        if (collider == null)
            return;
        
        _lockTarget = collider.gameObject;

        if (ObjectType == Define.ObjectType.Player)
        {
            int randomAttack = Random.Range(4, 7);
            State = (Define.State)randomAttack;
        }
        else
            State = Define.State.Attack1;
    }
    
    // 공격
    void Attack()
    {
        Stat targetStat = _lockTarget.GetComponent<Stat>();
        targetStat.OnAttacked(_stat);
        
        string attack = "Sound_Attack";
        Managers.Sound.Play(Define.Sound.Effect, attack);
     
        State = Define.State.Idle;
    }
}
