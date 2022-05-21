using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawnner : MonoBehaviour
{
    enum LineType
    {
        FlowingWater, Forest, Road, Train, Water, End
    }

    [SerializeField]
    private GameObject[] _lines;
    private GameObject[,] _linePool;
    [SerializeField]
    private int _PoolSize = 30;

    void Start()
    {
        //_spawnZPos = _startSpawnZPos;
        _linePool = new GameObject[(int)LineType.End, _PoolSize];

        for (int i = 0; i < _lines.Length; ++i)
        {
            for (int j = 0; j < _PoolSize; j++)
            {
                _linePool[i, j] = Instantiate(_lines[i], Vector3.zero, Quaternion.identity);
                _linePool[i, j].SetActive(false);
            }
        }
    }

}
