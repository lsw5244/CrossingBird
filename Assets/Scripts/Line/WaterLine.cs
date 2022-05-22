using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLine : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _boards;

    public int boardActivePercentage = 3;

    [SerializeField]
    private LayerMask _blockingObjectLayer;

    private GameObject _randomSelectBoard;

    private void Awake()
    {
        for (int i = 0; i < _boards.Length; ++i)
        {
            _boards[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        _randomSelectBoard = _boards[Random.Range(0, _boards.Length)];
        _randomSelectBoard.SetActive(true);
        CheckBlockingObejct(_randomSelectBoard.transform.position);

        for (int i = 0; i < _boards.Length; ++i)
        {
            if (Random.Range(1, 11) < boardActivePercentage)
            {
                _boards[i].SetActive(true);
                CheckBlockingObejct(_boards[i].transform.position);
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

    void CheckBlockingObejct(Vector3 BoardPosition)
    {
        RaycastHit hit;

        //Debug.DrawRay(BoardPosition + (Vector3.up * 2f), Vector3.back * 1f, Color.blue, 10);
        if (Physics.Raycast(BoardPosition + (Vector3.up * 2f), Vector3.back, out hit, 1f, _blockingObjectLayer))
        {
            hit.collider.gameObject.SetActive(false);                        
        }
    }
}
