using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Timeline;


public class GameManager
{
    private string _playerName;
    [SerializeField]
    private bool _isLive = true;
    private HeroKnightController _playerData;
    
    GameObject _player;
    GameData _gameData = new GameData();
    HashSet<GameObject> _monsters = new HashSet<GameObject>();

    public Action<int> OnSpawnEvent;
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }
    public GameObject GetPlayer{ get {return _player;} set { _player = value; } }
    public HeroKnightController PlayerInfo { get { return _playerData; } set { _playerData = value; } }
    public bool IsLive { get { return _isLive;} set { _isLive = value; } }
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
    
    public string GetPlayerTimer(float playTime)
    {
        float reMainTimer = playTime;
        int min = Mathf.FloorToInt(reMainTimer / 60);
        int sec = Mathf.FloorToInt(reMainTimer % 60);

        return $"{min.ToString("D2")} : {sec.ToString("D2")}";
    }
    
    public void Stop()
    {
        _isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        _isLive = true;
        Time.timeScale = 1;
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
