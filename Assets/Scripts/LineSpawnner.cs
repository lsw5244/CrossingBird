﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawnner : MonoBehaviour
{
    enum LineType
    {
        FlowingWater, Forest, Road, Train, Water, End
    }

    private LineType _prevLineType = LineType.End;

    [SerializeField]
    private GameObject[] _lines;
    private GameObject[,] _linePool;
    [SerializeField]
    private int _PoolSize = 30;

    public Transform _cameraTransfom;

    [SerializeField]
    private float _startSpawnZPos = 0f;
    private float _spawnZPos;
    [SerializeField]
    private float _spawnBlockDistance = 20f;

    private int _randomIndex;

    void Start()
    {
        _spawnZPos = _startSpawnZPos;
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

    void Update()
    {
        // 4부터 시작 마지막 블럭은 24임 -> 카메라와 spawnZPos랑 20(새 블럭이 생성 될 기준) 이상 차이나면 만들기
        // 카메라 기준에서 일정거리 
        if (_spawnZPos - _cameraTransfom.position.z < _spawnBlockDistance)
        {
            SpawnLine();
        }
    }

    void SpawnLine()
    {
        while(true)
        {
            _randomIndex = Random.Range(0, (int)LineType.End);
            if(_prevLineType == LineType.Water && (LineType)_randomIndex == LineType.Water)
            {
                continue;
            }
            _prevLineType = (LineType)_randomIndex;
            break;
        }

        GameObject line = GetLine((LineType)_randomIndex);
        line.transform.position = Vector3.forward * _spawnZPos;
        {
            line.SetActive(true);
        }
        _spawnZPos++;
    }

    GameObject GetLine(LineType type)
    {
        for (int i = 0; i < _PoolSize; ++i)
        {
            if (_linePool[(int)type, i].activeSelf == false)
            {
                return _linePool[(int)type, i];
            }
        }

        Debug.LogError("라인POOL에 있는 라인이 부족함 !");
        return null;
    }
}
