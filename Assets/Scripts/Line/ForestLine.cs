using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestLine : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _innerWoods;
    [SerializeField]
    private GameObject[] _outterWoods;

    [Range(0, 10)]
    public int innerWoodSpawnPercentage;
    [Range(0, 10)]
    public int outterWoodSpawnPercentage;

    private void OnEnable()
    {
        SpawnBlockingObject();
    }

    private void OnDisable()
    {
        
    }

    void SpawnBlockingObject()
    {
        for (int i = 0; i < _innerWoods.Length; ++i)
        {
            if (Random.Range(1, 11) <= innerWoodSpawnPercentage)
            {
                _innerWoods[i].SetActive(true);
            }
        }

        for (int i = 0; i < _outterWoods.Length; ++i)
        {
            if (Random.Range(1, 11) <= outterWoodSpawnPercentage)
            {
                _outterWoods[i].SetActive(true);
            }
        }
    }
}
