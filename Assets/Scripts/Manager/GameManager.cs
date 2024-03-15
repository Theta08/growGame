using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int _money;
    public float _playTime;
}

public class GameManager
{
    GameData _gameData = new GameData();
    
    private string _playerName;
    private HeroKnightController _playerData;
   
    public Action<int> OnSpawnEvent;
    public HeroKnightController PlayerInfo { get { return _playerData; } set { _playerData = value; } }
    public String PlayerName { get { return _playerName; } set { _playerName = value; } }

    #region 스텟
    public int Money { get { return _gameData._money; } set { _gameData._money = value; } }

    

    #endregion
   
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
