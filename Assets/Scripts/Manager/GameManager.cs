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
    GameObject _player;
    HashSet<GameObject> _monsters = new HashSet<GameObject>();
    
    private string _playerName;
    private HeroKnightController _playerData;
   
    public Action<int> OnSpawnEvent;
    public GameObject GetPlayer() { return _player; }
    
    public HeroKnightController PlayerInfo { get { return _playerData; } set { _playerData = value; } }
    public String PlayerName { get { return _playerName; } set { _playerName = value; } }

    #region 스텟
    public int Money { get { return _gameData._money; } set { _gameData._money = value; } }
    #endregion
   
    public void Init()
    {
    }
    
    public GameObject Spawn(Define.ObjectType type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.ObjectType.Monster:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.ObjectType.Player:
                _player = go;
                break;
            default:
                break;
        }

        return go;
    }

    public Define.ObjectType GetObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.ObjectType.Unknown;

        return bc.ObjectType;
    }
    public void Despawn(GameObject go)
    {
        Define.ObjectType type = GetObjectType(go);

        switch (type)
        {
            case Define.ObjectType.Monster:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if(OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.ObjectType.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                    break;
        }
        
        Managers.Resource.Destroy(go);
    }
}
