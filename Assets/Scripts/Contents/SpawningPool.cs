using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    int _reserveCount = 0;
    [SerializeField]
    int _keepMonsterCount = 0;
    [SerializeField]
    float _spawnTime = 0.5f;
    public void AddMonsterCount(int value) { _monsterCount += value; }
    
    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        if (_reserveCount + _monsterCount < _keepMonsterCount)
            StartCoroutine("ReserveSpawn");
    }
    
    IEnumerator ReserveSpawn()
    {
        string monsterName = "";
        _reserveCount++;
        
        yield return new WaitForSeconds(_spawnTime); 
        
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, monsterName);
        
        // obj.transform.position = randPos;
        _reserveCount--;
    }
}
