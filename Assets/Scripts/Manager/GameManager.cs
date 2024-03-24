using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Timeline;




public class GameManager
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }
    
    GameObject _player;
    HashSet<GameObject> _monsters = new HashSet<GameObject>();
    private string _playerName;
    private HeroKnightController _playerData;
   
    public Action<int> OnSpawnEvent;
    public GameObject GetPlayer{ get {return _player;} set { _player = value; } }
    
    public HeroKnightController PlayerInfo { get { return _playerData; } set { _playerData = value; } }

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

    public void RefreshPlayerData()
    {
        GetPlayer.GetComponent<HeroKnightController>().RefreshStat();
    }
    
    #region Save & Load
    public string _path = Application.persistentDataPath + "/SaveData.json";

    public void SaveGame()
    {
        string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
        // string jsonStr = JsonConvert.SerializeObject(Managers.Game.SaveData);
        File.WriteAllText(_path, jsonStr);
        Debug.Log($"Save Game Completed {_path}");
    }

    public bool LoadGame()
    {
        if (File.Exists(_path) == false)
            return false;

        string fileStr = File.ReadAllText(_path);
        GameData data = JsonUtility.FromJson<GameData>(fileStr);

        if (data != null)
            Managers.Game.SaveData = data;
        
        Debug.Log($"Save Game Load {_path}");

        return true;
    }
    #endregion
}
