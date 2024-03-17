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
                case Define.State.Attack:
                    anim.CrossFade("Attack", 0.1f, -1, 0);
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
                case Define.State.Hit:
                    anim.CrossFade("Hit", 0.1f);
                    break;
            }
        }
    }

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        State = Define.State.Idle;
    }

    
    private void Update()
    {
        switch (State)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Attack:
            case Define.State.Attack1:
            case Define.State.Attack2:
            case Define.State.Attack3:
                UpdateAttck();
                break;
            case Define.State.Hit:
                break;
            case Define.State.Die:
                Invoke("UpdateDie", 0.6f);
                break;
        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateAttck() { }
    protected virtual void UpdateDie() { }
    protected virtual void UpdateHit() { }

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
        Collider2D collider = Physics2D.OverlapBox(transform.position, new Vector2(3,3), 0, targetLayer);
        
        if (collider == null)
            return;
        
        _lockTarget = collider.gameObject;

        if (_lockTarget.GetComponent<Stat>().Hp <= 0)
            return;
        
        if (ObjectType == Define.ObjectType.Player)
        {
            int randomAttack = Random.Range(5, 8);
            State = (Define.State)randomAttack;
        }
        else
            State = Define.State.Attack;
    }
    
    // 공격
    void Attack()
    {
        Stat targetStat = _lockTarget.GetComponent<Stat>();
        targetStat.OnAttacked(_stat);
        
        State = Define.State.Idle;
    }

    void Hit()
    {
        // GetNearest();
        // State = Define.State.Idle;
    }
}
