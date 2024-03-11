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
    
    public string Name { get { return _name; } set { _name = value; } }
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

    public virtual void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack);
        Hp -= damage;
        
        // 데미지 표시

        if (Hp <= 0)
        {
            Hp = 0;
            OnDead();
        }
    }

    public virtual void OnDead()
    {
        BaseController myObject = gameObject.GetComponent<BaseController>();
        myObject.State = Define.State.Die;
    }
}
