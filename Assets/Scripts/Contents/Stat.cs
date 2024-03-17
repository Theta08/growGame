using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Stat: MonoBehaviour
{
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _attack;
    protected int _maxHp;
    
    [SerializeField]
    protected Define.ObjectType _type;
    [SerializeField] 
    public string _name;
    [SerializeField] 
    private int _money;
    
    private BaseController myObject;
    
    public string Name { get { return _name; } set { _name = value; } }
    public int Money { get { return _money; } set { _money = value; } }
    public Define.ObjectType Type { get { return _type; } set { _type = value; } }
    
    #region 스텟
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    #endregion
    
    private void Start()
    {
        _hp = 10;
        _maxHp = _hp;
        _attack = 3;
        Name = gameObject.GetComponent<BaseController>().gameObject.name;
    }

    private void OnEnable()
    {
        _hp = _maxHp;
    }

    public void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack);
        Hp -= damage;
     
        string attack = "Sound_Attack";
        Managers.Sound.Play(Define.Sound.Effect, attack);
        
        // 데미지 표시
        Managers.UI.MakeWorldSpace<UI_DamageText>(transform).SetDamage = damage;

        // if (Type == Define.ObjectType.Monster)
        //     myObject.State = Define.State.Hit;
        
        if (Hp <= 0)
        {
            Hp = 0;
            
            // 플레이어 돈 증가
            if (attacker.Type == Define.ObjectType.Player)
            {
                Managers.Game.Money += Money;
                attacker.Money = Managers.Game.Money;
            }
            OnDead();
        }
    }

    public virtual void OnDead()
    {
        // Debug.Log("Dead");
        myObject = gameObject.GetComponent<BaseController>();
        myObject.State = Define.State.Die;
    }
}
