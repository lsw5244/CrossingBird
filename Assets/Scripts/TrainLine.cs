using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLine : MonoBehaviour
{
    [SerializeField]
    private GameObject _train;
    private Vector3 _trainSpawnPosition;

    [SerializeField]
    private GameObject[] _greenTrafficLight;
    [SerializeField]
    private GameObject[] _redTrafficLight;

    [SerializeField]
    private float _warningDelay = 0.5f;

    [SerializeField]
    private float _minSpawnDelay = 1.0f;
    [SerializeField]
    private float _maxSpawnDelay = 3.0f;
    

    private void Awake()
    {
        _trainSpawnPosition = _train.transform.position;
        TurnOnGreenTrafficLight();

        _train.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(TrainSpawnCycle());
    }

    private void OnDisable()
    {
        StopCoroutine(TrainSpawnCycle());
        _train.SetActive(false);
    }

    IEnumerator TrainSpawnCycle()
    {
        while (gameObject.activeSelf == true)
        {

            if(_train.activeSelf == false)
            {
                // 경고 딜레이(0.5) + 빨간 불 키기
                // 기차 스폰시키기
                // 파란 불 키기 (스폰 끝) ( 기차가 메시지 보냄 )
                TurnOnRedTrafficLight();

                yield return new WaitForSeconds(_warningDelay);

                SpawnTrain();
            }

            yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));
        }
    }

    void SpawnTrain()
    {
        _train.transform.position = _trainSpawnPosition;
        _train.SetActive(true);
    }

    public void TurnOnGreenTrafficLight()
    {
        for(int i = 0; i < _greenTrafficLight.Length; ++i)
        {
            _greenTrafficLight[i].SetActive(true);
        }

        for (int i = 0; i < _greenTrafficLight.Length; ++i)
        {
            _redTrafficLight[i].SetActive(false);
        }
    }

    void TurnOnRedTrafficLight()
    {
        for (int i = 0; i < _greenTrafficLight.Length; ++i)
        {
            _greenTrafficLight[i].SetActive(false);
        }

        for (int i = 0; i < _greenTrafficLight.Length; ++i)
        {
            _redTrafficLight[i].SetActive(true);
        }
    }

}
