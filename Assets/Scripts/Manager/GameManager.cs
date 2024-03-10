using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private string _playerName;
    
    private HeroKnightController _gameData;
    
    public HeroKnightController PlayerInfo { get { return _gameData; } set { _gameData = value; } }
    public String PlayerName { get { return _playerName; } set { _playerName = value; } }
    
    public void Init()
    {
        
    }
    
    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                // _monsters.Add(go);
                // if (OnSpawnEvent != null)
                //     OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                // go.GetComponent<PlayerController>().MyPlay = false;
                break;
            default:
                // _monsters.Add(go);
                // if (OnSpawnEvent != null)
                    // OnSpawnEvent.Invoke(1);
                break;
        }

        return go;
    }
}
