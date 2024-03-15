using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MoneyText : UI_Base
{
    enum Texts
    {
        MoneyText,
    }
    
    [SerializeField]
    private float _moveSpeed = 1.0f;
    [SerializeField]
    private float _alphaSpeed = 2.0f;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private Color _color = Color.red;
    private Color alpha;
    private float postionY = 0.0f; 
    
    private Stat _stat;
    public int SetDamage { set { _damage = value; } }
    public Color SetColor { set { _color = value; } }
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        _stat = transform.parent.GetComponent<Stat>();
        
        BindText(typeof(Texts));
        GetText((int)Texts.MoneyText).text = "";
        alpha = GetText((int)Texts.MoneyText).color = _color;

        postionY = 1.5f;        
        Transform parent = transform.parent;
        if(_stat.Type == Define.ObjectType.Player)
            transform.position = parent.position + new Vector3(0,1.2f,0) * parent.GetComponent<Collider2D>().bounds.size.y * postionY;
        else
            transform.position = parent.position + new Vector3(0,0.5f,0)* parent.GetComponent<Collider2D>().bounds.size.y * postionY;

        return true;
    }
    
    void Update()
    {
        transform.Translate(new Vector3(0, _moveSpeed * Time.deltaTime, 0));
        transform.rotation = Camera.main.transform.rotation;
        
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * _alphaSpeed);

        GetText((int)Texts.MoneyText).text = $"{_damage}";
        GetText((int)Texts.MoneyText).color = alpha;
        
        if(alpha.a <= 0.4f)
            Destroy(gameObject);
    }
}
