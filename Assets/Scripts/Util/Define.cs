using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
    }
    
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
    }
    
    public enum Sound
    {
        Bgm,
        Effect,
        Speech,
        Max,
    }
    
    public enum Scene
    {
        Unknown,
        Dev,
        Game,
    }

    public enum ObjectType
    {
        Unknown,
        Player,
        Monster,
    }
    
    public enum State
    {
        Idle,
        Moving,
        Die,
        Damage,
        Attack1,
        Attack2,
        Attack3,
    }
}
