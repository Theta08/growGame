using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class WeightedItem
{
    public int rank;
    public float weight;
}

public class WeightedRandomUtility
{
    public static int GetWeightedRandom(List<WeightedItem> weightedItems)
    {
        // 만약 항목이 없으면 기본값 반환
        if (weightedItems.Count == 0)
            return 0;

        // 모든 가중치를 더하여 총합을 구함
        float totalWeight = 0f;
        
        foreach (var weightedItem in weightedItems)
        {
            totalWeight += weightedItem.weight;
        }

        // 0부터 총합 사이의 랜덤 값을 생성
        float randomValue = Random.value * totalWeight;

        // 랜덤 값이 어느 범위에 속하는지 확인하여 항목 선택
        foreach (var weightedItem in weightedItems)
        {
            randomValue -= weightedItem.weight;
            
            if (randomValue <= 0)
                return weightedItem.rank;
        }

        //기본값 반환
        return 0;
    }

}
