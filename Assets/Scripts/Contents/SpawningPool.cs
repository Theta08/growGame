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
    float _spawnTime = 0.7f;
    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }
    
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
        // 임시 
        string monsterName = "Enemies/Goblin";
        _reserveCount++;
        
        yield return new WaitForSeconds(_spawnTime); 
        
        GameObject obj = Managers.Game.Spawn(Define.ObjectType.Monster, monsterName);
        
        // obj.transform.position = randPos;
        _reserveCount--;
    }
}
