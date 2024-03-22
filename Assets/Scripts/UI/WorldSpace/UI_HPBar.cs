using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar
    }

    private Stat _stat;
    
    public override bool Init()
    { 
        if (base.Init() == false)
            return false;
        
        BindObject(typeof(GameObjects));

        // _stat = transform.parent.GetComponent<Stat>();
        
        return true;
    }

    private void OnEnable()
    {
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        
        if(_stat.Type == Define.ObjectType.Player)
            transform.position = parent.position + new Vector3(0,1.2f,0) * parent.GetComponent<Collider2D>().bounds.size.y;
        else
            transform.position = parent.position + new Vector3(0,0.5f,0)* parent.GetComponent<Collider2D>().bounds.size.y;
        
        transform.rotation = Camera.main.transform.rotation;

        float hpBar = _stat.Hp / (float)_stat.MaxHp;

        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = hpBar;
    }

    public void RefeshUI()
    {
        _stat = transform.parent.GetComponent<Stat>();
    }
}
