using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _disableBoards;

    public float _moveSpeed = 5f;
    public int boardActivePercentage = 6;

    private void Awake()
    {
        for(int i = 0; i < _disableBoards.Length; ++i)
        {
            _disableBoards[i].SetActive(false);
        }
    }

    void Update()
    {
        transform.position += transform.right * _moveSpeed * Time.deltaTime;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _disableBoards.Length; ++i)
        {
            if (Random.Range(1, 11) < boardActivePercentage)
            {
                _disableBoards[i].SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _disableBoards.Length; ++i)
        {
            _disableBoards[i].SetActive(false);
        }
    }
}
