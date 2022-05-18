using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLine : MonoBehaviour
{
    enum CarMoveDirection
    {
        Left, Right, End
    }
    private CarMoveDirection _carMoveDirection;

    [SerializeField]
    private GameObject[] _cars;
    private GameObject[] _carPool;
    [SerializeField]
    private int _spawnCarCount = 2;
    
    private Vector3 _carSpawnPosition;
    private int _selectCarIdx;
    private GameObject _selectCar;

    [SerializeField]
    private float _minSpawnDelay = 1.0f;
    [SerializeField]
    private float _maxSpawnDelay = 3.0f;

    void Awake()
    {
        _carPool = new GameObject[_cars.Length * _spawnCarCount];
        for (int i = 0; i < _cars.Length; ++i)
        {
            for (int j = 0; j < _spawnCarCount; j++)
            {
                _carPool[i * _spawnCarCount + j] = Instantiate(_cars[i], _cars[i].transform.position, Quaternion.identity);
                _carPool[i * _spawnCarCount + j].transform.parent = transform;
                _carPool[i * _spawnCarCount + j].SetActive(false);
            }
        }
    }

    // 첫 시작은 -14 or 14
    private void OnEnable()
    {
        _carMoveDirection = (CarMoveDirection)Random.Range(0, (int)CarMoveDirection.End);

        StartCoroutine(SpawnCar());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnCar());
        for(int i = 0; i < _carPool.Length; ++i)
        {
            _carPool[i].SetActive(false);
        }
    }

    IEnumerator SpawnCar()
    {
        while (gameObject.activeSelf == true)
        {
            _selectCarIdx = Random.Range(0, _carPool.Length);
            _selectCar = _carPool[_selectCarIdx];

            if (_selectCar.activeSelf == false)
            {
                SetSpawnCarPosition();

                // Y회전값이 0이면 오른쪽으로 향하기, 180이면 왼쪽으로 향하기
                if (_carMoveDirection == CarMoveDirection.Right)
                {
                    _selectCar.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
                else
                {
                    _selectCar.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }

                _selectCar.SetActive(true);
            }
            yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));
        }
    }

    void SetSpawnCarPosition()
    {
        _carSpawnPosition.x = _carMoveDirection == CarMoveDirection.Right ? -14f : 14f;
        _carSpawnPosition.y = _selectCar.transform.position.y;
        _carSpawnPosition.z = transform.position.z;
        _selectCar.transform.position = _carSpawnPosition;
    }

}
