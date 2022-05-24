using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _leafs;

    [Range(0, 10)]
    public int leapAddPercentage = 5;

    private void OnEnable()
    {
        _leafs[0].SetActive(true);

        for (int i = 1; i < _leafs.Length; ++i)
        {
            if(Random.Range(1, 11) <= leapAddPercentage)
            {
                _leafs[i].SetActive(true);
                continue;
            }
            break;
        }
    }

    private void OnDisable()
    {
        for (int i = 1; i < _leafs.Length; ++i)
        {
            _leafs[i].SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
