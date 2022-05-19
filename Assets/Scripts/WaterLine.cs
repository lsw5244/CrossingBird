using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLine : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _boards;

    public int boardActivePercentage = 3;

    private void Awake()
    {
        for (int i = 0; i < _boards.Length; ++i)
        {
            _boards[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        _boards[Random.Range(0, _boards.Length)].SetActive(true);

        for(int i = 0; i < _boards.Length; ++i)
        {
            if (Random.Range(1, 11) < boardActivePercentage)
            {
                _boards[i].SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _boards.Length; ++i)
        {
            _boards[i].SetActive(false);
        }
    }
}
